using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using BinaryVersion.Core.Model;
using BinaryVersion.Core.Response;
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
            // NOTE:
            // I have started with context.Request.CurrentExecutionFilePathExtension which returns just the extension, e.g. .xml or .json,
            // so that the filename would be controlled from the web.config, but because that property is only available in .NET 4.0 and higher
            // and I want to make the library work in .NET 3.5 and 2.0, I need to use another property which is available in older frameworks
            var requestPath = context.Request.CurrentExecutionFilePath;
            var responseHandler = ResponseHandlerFactory.CreateResponseHandler<ApplicationInformation>(requestPath.Substring(requestPath.LastIndexOf(".")));

            // write the information to the response using appropriate response handler
            context.Response.ContentType = responseHandler.ContentType;
            responseHandler.Serialize(context.Response.OutputStream, applicationInformation);

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
            foreach (var fileVersion in versions)
            {
                if (FilterFileVersionInfo.CanCache(fileVersion))
                {
                    response.AddFileDependency(fileVersion.GetFileName());
                }
            }
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