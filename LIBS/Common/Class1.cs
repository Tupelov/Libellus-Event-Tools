using System;
using System.Collections.Generic;
using System.IO;
using PAKPack;

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
        public static string UnpackPAK(String filepath)
        {
            
            string[] args = new string[3];
            args[0] = "unpack";
            args[1] = Path.GetFullPath(filepath);
            //string unpacked = filepath.Substring(filepath.Length - 4);
            string unpacked = filepath + "_unpacked";
            args[2] = unpacked;
            try
            {
                Program.RunPak(args);
            }
            catch (Exception e)
            {
                
            }
            return unpacked;
            
        }
        
        /// <summary>
        /// Returns a list of all files in directory.
        /// </summary>
        /// <param name="directory"></param>
        /// <returns></returns>
        public static string[] GetFilesinDirectory(String directory) {

            try
            {
                string[] filePaths = Directory.GetFiles(directory, "",
                                             SearchOption.AllDirectories);

                return filePaths;
            }catch(Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }

        /// <summary>
        /// Takes an array of filepaths and returns the ones that end in extension.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="extension"></param>
        /// <returns></returns>
        public static string[] FilterExtensions(string[] list, string[] extension)
        {
            List<string> arr = new List<string>();
            for(int i = 0; i < list.Length; i++)
            {
                for (int j = 0; j < extension.Length; j++)
                {
                    
                    string ext = Path.GetExtension(list[i]).ToUpper();
                    if (ext.Equals(extension[j].ToUpper()))
                    {
                        arr.Add(list[i]);
                    }


                }
            }
            
            return (string[])arr.ToArray();
        }


    }


}
