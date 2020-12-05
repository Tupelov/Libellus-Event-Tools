using LibellusCommon;
using System;
namespace Extract_BF
{
    class Program
    {
        
        static int Main(string[] args)
        {
            Console.WriteLine("Input directory");
            String input = Console.ReadLine();
            string[] directory = Utils.GetFilesinDirectory(input);
            if (directory == null)
            {
                Console.WriteLine("Invalid Directory Set!");
                return -1;
            }
            for(int i = 0; i < directory.Length; i++)
            {
                Console.WriteLine(directory[i]);
            }

            string[] pakExtensions = new string[] { ".PAK",
                                                    ".PAC",
                                                    ".BIN",
                                                    ".ARC" 
                                                    };
            Console.WriteLine("Unpacking!");

            string[] pakList = Utils.FilterExtensions(directory, pakExtensions);

            for (int i = 0; i < pakList.Length; i++)
            {
                Console.WriteLine(pakList[i]);
            }

            for (int i =0; i < pakList.Length; i++)
            {
                Utils.UnpackPAK(pakList[i]);
            }
            return 0;
        }

        
    }
}
