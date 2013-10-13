using System.Diagnostics;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace BinaryVersion.Handler.Model
{
    /// <summary>
    /// FileVersion represents a ligher type similar to <see cref="System.Diagnostics.FileVersionInfo"/> used for serializing only some of the properties.
    /// A new type was needed because the FileVersionInfo is selaed and doesn't implement any interfaces.
    /// </summary>
    public class FileVersion : IResponseType
    {
        #region [ Propreties ]

        public string CompanyName { get; set; }
        public string ProductName { get; set; }
        public string ProductVersion { get; set; }
        public string Comments { get; set; }
        public string LegalCopyright { get; set; }
        public string LegalTrademarks { get; set; }

        [ScriptIgnore]
        [XmlIgnore]
        [SoapIgnore]
        public string FileName { get; set; }

        #endregion

        #region [ Constructors ]

        /// <summary>
        /// Default constructor for serialization.
        /// </summary>
        public FileVersion() {}

        /// <summary>
        /// Initialising constructor.
        /// </summary>
        public FileVersion(FileVersionInfo fileVersionInfo)
        {
            // I didn't want to add dependency on e.g. AutoMapper, keep it simple for now
            this.CompanyName = fileVersionInfo.CompanyName;
            this.ProductName = fileVersionInfo.ProductName;
            this.ProductVersion = fileVersionInfo.ProductVersion;
            this.FileName = fileVersionInfo.FileName;
            this.Comments = fileVersionInfo.Comments;
            this.LegalCopyright = fileVersionInfo.LegalCopyright;
            this.LegalTrademarks = fileVersionInfo.LegalTrademarks;
        }

        #endregion
    }
}