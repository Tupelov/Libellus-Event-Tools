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
            
            return 0;
        }

        
    }
}
