using System.Collections.Generic;

namespace BinaryVersion.Core.Model
{
    /// <summary>
    /// ApplicationInformation contains basic information about the running application (that is currently in my interest).
    /// </summary>
    public class ApplicationInformation : IResponseType
    {
        #region [ Properties ]

        /// <summary>
        /// Physical computer name
        /// </summary>
        public string ComputerName { get; set; }

        /// <summary>
        /// Server name as returned by a browser.
        /// </summary>
        public string ServerName { get; set; }

        /// <summary>
        /// List of versions of binaries with related information.
        /// </summary>
        public List<FileVersion> Versions { get; set; }

        #endregion

        #region [ Constructors ]

        /// <summary>
        /// Default constructor for serialization.
        /// </summary>
        public ApplicationInformation() {}

        #endregion
    }
}