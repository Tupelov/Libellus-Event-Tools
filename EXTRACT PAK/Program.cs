using LibellusCommon;
using System;
using System.Collections.Generic;

namespace EXTRACT_PAK
{
    class Program
    {
        public static string[] directory;

        public static string[] pakExtensions = new string[] { ".PAK",
                                                    ".PAC",
                                                    ".BIN",
                                                    ".ARC"
                                                    };
        public static string input;

        static int Main(string[] args)
        {
            Console.WriteLine("Input directory");
            input = Console.ReadLine();
            directory = Utils.GetFilesinDirectory(input);
            if (directory == null)
            {
                Console.WriteLine("Invalid Directory Set!");
                return -1;
            }


            for (int i = 0; i < directory.Length; i++)
            {
                Console.WriteLine(directory[i]);
            }



            Console.WriteLine("Unpacking!");

            string[] pakList = Utils.FilterExtensions(directory, pakExtensions);

            for (int i = 0; i < pakList.Length; i++)
            {
                Console.WriteLine(pakList[i]);
            }

            UnPackAll(pakList);


            return 0;
        }


        public static void UnPackAll(string[] filelist)
        {
            if (filelist.Length == 0)
            {
                return;
            }
            else
            {

                //string[] pakList = Utils.FilterExtensions(directory, pakExtensions);

                List<string> unpacked = new List<string>();
                for (int i = 0; i < filelist.Length; i++)
                {
                    string folder = Utils.UnpackPAK(filelist[i]);
                    unpacked.Add(folder);
                }

                directory = (string[])unpacked.ToArray();


                for (int i = 0; i < directory.Length; i++)
                {
                    string[] moreFiles = new string[1];
                    moreFiles = Utils.FilterExtensions(Utils.GetFilesinDirectory(directory[i]), pakExtensions);
                    UnPackAll(moreFiles);
                }
            }
        }

    }
}
