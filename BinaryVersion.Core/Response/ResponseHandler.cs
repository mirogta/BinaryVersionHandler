using System;
using System.IO;
using BinaryVersion.Core.Model;

namespace BinaryVersion.Core.Response
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
