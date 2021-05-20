using System;
using LibellusLibrary.PmdFile;

namespace LibellusLibraryTest
{
	class Program
	{
		static void Main(string[] args)
		{

			Console.WriteLine("Starting Test!");
			string testFilePath = "./Samples/E401_004.PM2";
			Console.WriteLine("Testing: {0}", testFilePath);
			PmdFile testFile = new PmdFile(testFilePath);
			testFile.Save("out.PM2");
			Console.WriteLine("Finished tests, exiting.");

		}
	}
}
