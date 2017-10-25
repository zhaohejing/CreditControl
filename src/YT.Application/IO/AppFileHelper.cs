using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Abp.IO;

namespace YT.IO
{/// <summary>
/// 
/// </summary>
    public static class AppFileHelper
    {/// <summary>
    /// 
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
        public static IEnumerable<string> ReadLines(string path)
        {
            using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite, 0x1000, FileOptions.SequentialScan))
            using (var sr = new StreamReader(fs, Encoding.UTF8))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    yield return line;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="folderPath"></param>
        /// <param name="fileNameWithoutExtension"></param>
        public static void DeleteFilesInFolderIfExists(string folderPath, string fileNameWithoutExtension)
        {
            var directory = new DirectoryInfo(folderPath);
            var tempUserProfileImages = directory.GetFiles(fileNameWithoutExtension + ".*", SearchOption.AllDirectories).ToList();
            foreach (var tempUserProfileImage in tempUserProfileImages)
            {
                FileHelper.DeleteIfExists(tempUserProfileImage.FullName);
            }
        }
    }
}
