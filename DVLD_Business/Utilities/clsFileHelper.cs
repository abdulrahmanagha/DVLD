using System;
using System.IO;
using DVLD_Shared.Utility;

namespace DVLD.People
{
    public class clsFileHelper
    {
        public static bool CopyFileToFolder(string sourceFilePath, string destinationFolderPath, out string result)
        {
            result = null;

            try
            {
                if (!Directory.Exists(destinationFolderPath))
                {
                    Directory.CreateDirectory(destinationFolderPath);
                }

                string fileExtension = Path.GetExtension(sourceFilePath);
                string fileName = Guid.NewGuid().ToString() + fileExtension;
                string destinationFilePath = Path.Combine(destinationFolderPath, fileName);

                File.Copy(sourceFilePath, destinationFilePath, true);

                result = destinationFilePath;
                return true;
            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(
                    ex,
                    $"SourceFile: {sourceFilePath}, DestinationFolder: {destinationFolderPath}"
                );
                result = null;
                return false;
            }
        }

        public static bool DeleteFile(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath) || !File.Exists(filePath))
                return false;

            try
            {
                File.Delete(filePath);
                return true;
            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(ex, $"FilePath: {filePath}");
                return false;
            }
        }
    }
}