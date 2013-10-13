using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using BinaryVersion.Handler.Model;
using BinaryVersion.Handler.Response;

namespace BinaryVersion.Handler
{
    /// <summary>
    /// VersionHandler returns list of binaries used by currently running web application, their versions and other information, together with some basic information about the application environment. 
    /// </summary>
    public class VersionHandler : IHttpHandler
    {
        #region [ IHttpHandler Implementation ]

        public void ProcessRequest(HttpContext context)
        {
            // filter by request path info, e.g. /all returns all binaries
            var filter = new FilterFileVersionInfo(context.Request.PathInfo);

            // get information about application, assemblies and their versions
            var applicationInformation = GetApplicationInformation(filter);

            // create response handler based on the extension, e.g. .xml or .json
            var responseHandler = ResponseHandlerFactory.CreateResponseHandler(context.Request.CurrentExecutionFilePathExtension, context.Response);

            // write the information to the response using appropriate response handler
            responseHandler.Write(applicationInformation);

            // response output caching
            AddFileDependencyCache(context.Response, applicationInformation.Versions);
        }

        public bool IsReusable
        {
            get { return true; }
        }

        #endregion

        #region [ Private Methods ]

        private static void AddFileDependencyCache(HttpResponse response, IEnumerable<FileVersion> versions)
        {
            // Set additional properties to enable caching
            EnableCaching(response.Cache);

            // Add file dependency cache
            versions.AsParallel().Where(FilterFileVersionInfo.CanCache).ForAll(v => response.AddFileDependency(v.FileName));
        }

        private static void EnableCaching(HttpCachePolicy cache)
        {
            cache.SetExpires(DateTime.Now.AddYears(1));
            cache.SetCacheability(HttpCacheability.Public);
            cache.SetValidUntilExpires(true);
        }

        private static ApplicationInformation GetApplicationInformation(FilterFileVersionInfo filterFileVersionInfo)
        {
            return new ApplicationInformation
            {
                ComputerName = Environment.MachineName,
                ServerName = HttpContext.Current.Request.ServerVariables["SERVER_NAME"],
                Versions = GetVersions(filterFileVersionInfo).ToList()
            };
        }

        private static IEnumerable<FileVersion> GetVersions(FilterFileVersionInfo filterFileVersionInfo)
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                FileVersionInfo fileVersion = null;
                try
                {
                     fileVersion = FileVersionInfo.GetVersionInfo(assembly.Location);
                }
                catch (NotSupportedException)
                {
                    // System.NotSupportedException: The invoked member is not supported in a dynamic assembly.
                    continue;
                }

                if (filterFileVersionInfo.CanAdd(fileVersion))
                {
                    yield return new FileVersion(fileVersion);
                }
            }
        }

        #endregion
    }
}