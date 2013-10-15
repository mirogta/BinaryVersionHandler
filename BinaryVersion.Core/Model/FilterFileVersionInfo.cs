using System;
using System.Diagnostics;

namespace BinaryVersion.Core.Model
{
    public class FilterFileVersionInfo
    {
        private const string PARAM_SHOW_ALL = "/all";
        private const string COMPANY_NAME_MICROSOFT = "Microsoft Corporation";

        #region [ Properties ]

        /// <summary>
        /// Show all file versions? Default is: false.
        /// Hide binaries from Microsoft Corporation by default.
        /// </summary>
        public bool ShowAll { get; set; }

        #endregion

        #region [ Properties ]

        /// <summary>
        /// Initialising constructor.
        /// </summary>
        /// <param name="filter">Filter parameter controls which file versions will be added to the output.</param>
        public FilterFileVersionInfo(string filter)
        {
            this.ShowAll = String.Equals(filter, PARAM_SHOW_ALL, StringComparison.InvariantCultureIgnoreCase);
        }

        #endregion

        #region [ Properties ]

        /// <summary>
        /// Determines whether a file version can be added to the output.
        /// </summary>
        /// <param name="fileVersion"></param>
        /// <returns>
        /// Returns true if the file version can be added to the output.
        /// Returns false if the file version cannot be added to the output.
        /// </returns>
        public bool CanAdd(FileVersionInfo fileVersion)
        {
            if (ShowAll)
            {
                return true;
            }

            return String.Equals(COMPANY_NAME_MICROSOFT, fileVersion.CompanyName, StringComparison.InvariantCultureIgnoreCase) == false;
        }

        /// <summary>
        /// Determines whether a file version can be cached in the output.
        /// </summary>
        /// <param name="fileVersion"></param>
        /// <returns>
        /// Returns true if the file version can be cached in the output.
        /// Returns false if the file version cannot be cached in the output.
        /// </returns>
        public static bool CanCache(FileVersion fileVersion)
        {
            return String.Equals(COMPANY_NAME_MICROSOFT, fileVersion.CompanyName, StringComparison.InvariantCultureIgnoreCase) == false;
        }

        #endregion
    }
}