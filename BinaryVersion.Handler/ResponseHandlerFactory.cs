using System;
using System.Web;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization.Json;
//using System.Web.Script.Serialization;
using BinaryVersion.Core.Model;
using BinaryVersion.Core.Response;

namespace BinaryVersion.Handler.Response
{
    /// <summary>
    /// ResponseHandlerFactory creates and returns IResponseHandler object based on the request extension.
    /// </summary>
    public static class ResponseHandlerFactory
    {
        public static IResponseHandler CreateResponseHandler<T>(string extension)
            where T : IResponseType
        {
            switch (extension)
            {
                case ".json":
                    return new ResponseHandler
                    {
                        ContentType = "application/json",
                        Serialize = (stream, obj) => new DataContractJsonSerializer(typeof(T)).WriteObject(stream, obj)
                        // .NET 3.5:
                        //Serialize = (stream, obj) => (new StreamWriter(stream)).Write(new JavaScriptSerializer().Serialize(obj))
                    };
                case ".xml":
                    return new ResponseHandler
                    {
                        ContentType = "text/xml",
                        Serialize = (stream, obj) => new XmlSerializer(typeof(T)).Serialize(stream, obj)
                    };
                default:
                    throw new NotSupportedException();
            }
        }
    }
}
