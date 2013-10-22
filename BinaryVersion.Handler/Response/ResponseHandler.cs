using System;
using System.IO;
using BinaryVersion.Handler.Model;

namespace BinaryVersion.Handler.Response
{
    /// <summary>
    /// Generic response handler
    /// </summary>
    public class ResponseHandler : IResponseHandler
    {
        #region [ Properties ]

        public string ContentType { get; set; }

        public SerializeResponse Serialize { get; set; } 
        
        #endregion
    }
}
