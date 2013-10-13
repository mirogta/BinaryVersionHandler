using System;
using System.Web;
using BinaryVersion.Handler.Model;

namespace BinaryVersion.Handler.Response
{
    public static class ResponseHandlerFactory
    {
        public static IResponseHandler CreateResponseHandler(string extension, HttpResponse response)
        {
            switch (extension)
            {
                case ".json":
                    return new JsonResponseHandler(response);
                case ".xml":
                    return new XmlResponseHandler<ApplicationInformation>(response);
                default:
                    throw new NotSupportedException();
            }
        }
    }
}
