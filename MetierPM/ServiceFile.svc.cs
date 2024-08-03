using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MetierPM
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ServiceFile" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ServiceFile.svc or ServiceFile.svc.cs at the Solution Explorer and start debugging.
    public class ServiceFile : IServiceFile
    {
        private readonly string fileDirectory = "PathToYourFileDirectory";

        public bool UploadFile(byte[] fileBytes, string fileName)
        {
            try
            {
                if (!Directory.Exists(fileDirectory))
                {
                    Directory.CreateDirectory(fileDirectory);
                }
                string filePath = Path.Combine(fileDirectory, fileName);
                File.WriteAllBytes(filePath, fileBytes);
                return true;
            }
            catch (Exception ex)
            {
                // Log the exception
                return false;
            }
        }

        public byte[] DownloadFile(string fileName)
        {
            try
            {
                string filePath = Path.Combine(fileDirectory, fileName);
                if (File.Exists(filePath))
                {
                    return File.ReadAllBytes(filePath);
                }
                return null;
            }
            catch (Exception ex)
            {
                // Log the exception
                return null;
            }
        }
    }
}

