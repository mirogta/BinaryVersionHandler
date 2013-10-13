using System.Web;
using System.Xml.Serialization;
using BinaryVersion.Handler.Model;

namespace BinaryVersion.Handler.Response
{
    /// <summary>
    /// Response type for XML.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class XmlResponseHandler<T>: IResponseHandler
        where T : IResponseType
    {
        #region [ Private Fields ]

        private XmlSerializer Serializer { get; set; }
        private System.IO.Stream ResponseOutputStream { get; set; }

        #endregion

        #region [ Constructors ]

        /// <summary>
        /// Initialising constructor for "text/xml" response.
        /// </summary>
        /// <param name="response"></param>
        public XmlResponseHandler(HttpResponse response)
        {
            response.ContentType = "text/xml";
            this.ResponseOutputStream = response.OutputStream;
            this.Serializer = new XmlSerializer(typeof(T));
        }

        #endregion

        #region IResponseHandler Implementation

        public void Write(IResponseType obj)
        {
            this.Serializer.Serialize(ResponseOutputStream, obj);
        }

        #endregion

    }
}
