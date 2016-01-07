using Saver;
using System;
using System.IO;

namespace MGUI
{
    public class Saver
    {
        public static string SavePath
        {
            get
            {
                string home = (  Environment.OSVersion.Platform == PlatformID.Unix ||
                                 Environment.OSVersion.Platform == PlatformID.MacOSX)
                                 ? Environment.GetEnvironmentVariable("HOME")
                                 : Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%\\Saved Games");
                return home + Path.DirectorySeparatorChar + "SmallWorld";
            }
        }

        static void SafeCreateDirectory()
        {
            if (!Directory.Exists(SavePath))
            {
                Directory.CreateDirectory(SavePath);
            }
        }

        public static string SaveGame(Core.Game g)
        {
            SafeCreateDirectory();

            try
            {
                string fileName = string.Format("{0:yyyy-MM-dd_HH-mm-ss}.sw", DateTime.Now);
                string filePath = SavePath + Path.DirectorySeparatorChar + fileName;
                System.Console.WriteLine(filePath);
                Stream output = new FileStream(filePath, FileMode.Create);
                Xml.GameToXml(g, output);
                output.Close();
                output.Dispose();

                return fileName;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
