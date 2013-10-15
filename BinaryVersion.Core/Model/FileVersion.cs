using System.Diagnostics;

namespace BinaryVersion.Core.Model
{
    /// <summary>
    /// FileVersion represents a ligher type similar to <see cref="System.Diagnostics.FileVersionInfo"/> used for serializing only some of the properties.
    /// A new type was needed because the FileVersionInfo is selaed and doesn't implement any interfaces.
    /// </summary>
    public class FileVersion : IResponseType
    {
        #region [ Propreties ]

        public string OriginalFilename { get; set; }
        public string CompanyName { get; set; }
        public string ProductName { get; set; }
        public string ProductVersion { get; set; }
        public string Comments { get; set; }
        public string LegalCopyright { get; set; }
        public string LegalTrademarks { get; set; }

        /// <summary>
        /// Full path of the binary file.
        /// </summary>
        /// <remarks>
        /// The only safe way to target multiple .NET framework versions was to make this private and access the value via a method.
        /// In .NET 4.5 I could use [XmlIgnore] and [ScriptIgnore] attributes, but [ScriptIgnore] is not available in .NET 2.0.
        /// </remarks>
        private string FileName { get; set; }

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
            this.OriginalFilename = fileVersionInfo.OriginalFilename;
            this.CompanyName = fileVersionInfo.CompanyName;
            this.ProductName = fileVersionInfo.ProductName;
            this.ProductVersion = fileVersionInfo.ProductVersion;
            this.FileName = fileVersionInfo.FileName;
            this.Comments = fileVersionInfo.Comments;
            this.LegalCopyright = fileVersionInfo.LegalCopyright;
            this.LegalTrademarks = fileVersionInfo.LegalTrademarks;
        }

        #endregion

        #region [ Methods ]

        /// <summary>
        /// Returns full path of the binary file.
        /// </summary>
        /// <returns></returns>
        public string GetFileName()
        {
            return this.FileName;
        }

        #endregion
    }
}