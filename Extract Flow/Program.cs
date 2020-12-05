using System;
namespace Extract_BF
{
    class Program
    {
        
        static int Main(string[] args)
        {
            Console.WriteLine("Input directory");
            String input = Console.ReadLine();
            string[] directory = LibellusCommon.Utils.GetFilesinDirectory(input);
            for(int i = 0; i < directory.Length; i++)
            {
                Console.WriteLine(directory[i]);
            }
            
            return 0;
        }

        
    }
}
