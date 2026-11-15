using System;
using System.Configuration;
using System.IO;

namespace DVLD_Business
{
    internal class clsSettings
    {
        public static readonly string PeopleImagesFolderPath = GetPeopleImagesFolderPath();

        private static string GetPeopleImagesFolderPath()
        {
            string folderPath = ConfigurationManager.AppSettings["ImagesFolderPath"];

            if (string.IsNullOrWhiteSpace(folderPath))
            {
                throw new ConfigurationErrorsException(
                    "Configuration error: 'ImagesFolderPath' key is missing or empty in App.config.");
            }

            return folderPath.Trim();
        }
    }
}
