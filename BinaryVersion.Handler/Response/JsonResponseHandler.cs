using System.Web;
using System.Web.Script.Serialization;
using BinaryVersion.Handler.Model;

namespace BinaryVersion.Handler.Response
{
    /// <summary>
    /// Response handler for JSON.
    /// </summary>
    public class JsonResponseHandler: IResponseHandler
    {
        #region [ Private Fields ]

        private JavaScriptSerializer Serializer { get; set; }
        private HttpResponse Response { get; set; }

        #endregion

        #region [ Constructors ]

        /// <summary>
        /// Initialising constructor for "application/json" response.
        /// </summary>
        /// <param name="response"></param>
        public JsonResponseHandler(HttpResponse response)
        {
            response.ContentType = "application/json";
            this.Response = response;
            this.Serializer = new JavaScriptSerializer();
        }

        #endregion

        #region IResponseHandler Implementation

        public void Write(IResponseType obj)
        {
            this.Response.Write(Serializer.Serialize(obj));
        }

        #endregion
    }
}
