using System;
using System.Collections;
using System.IO;

/// <summary>
/// A Library consisting of commonly used functions.
/// </summary>
namespace LibellusCommon
{

    /// <summary>
    /// Contains Generel Utilities
    /// </summary>
    public class Utils
    {
        /// <summary>
        /// Unpacks file using PACPAK
        /// </summary>
        /// <param name="filepath"></param>
        public static void UnpackPAK(String filepath)
        {

        }

        /// <summary>
        /// Returns a list of all files in directory.
        /// </summary>
        /// <param name="directory"></param>
        /// <returns></returns>
        public static string[] GetFilesinDirectory(String directory) {

            string[] filePaths = Directory.GetFiles(directory, "",
                                         SearchOption.AllDirectories);
            
            return filePaths;
        }

        /// <summary>
        /// Takes an array of filepaths and returns the ones that end in extension
        /// </summary>
        /// <param name="list"></param>
        /// <param name="extension"></param>
        /// <returns></returns>
        public static string[] FilterExtensions(string[] list, string[] extension)
        {
            ArrayList arr = new ArrayList();
            for(int i = 0; i < list.Length; i++)
            {
                for (int i = 0; i < extension.Length; i++)
                {
                    
                    string ext = Path.GetExtension(list[i]).ToUpper();
                    if (ext.Equals(extension.ToUpper()))
                    {
                        arr.Add(list[i]);
                    }


                }
            }
            return (string[])arr.ToArray(typeof(string));
        }


    }


}
