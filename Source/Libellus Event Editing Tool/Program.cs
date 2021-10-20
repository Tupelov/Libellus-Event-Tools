using System;
using System.IO;
using LibellusLibrary.PMD;


namespace LibellusEventEditingTool
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Welcome to LEET!\nLibellus Event Editing Tool");
			if (args.Length < 1)
			{
				Console.WriteLine("Not Enough args!");
				return;
			}
			foreach (string file in args)
			{
				string ext = Path.GetExtension(file).ToLower();
				if (ext == ".pm1" || ext == ".pm2" || ext == ".pm3")
				{
					Console.WriteLine("Coverting to Json: ", file);
					PmdFile pmdFile = new(file);
					pmdFile.ExtractPmd(new FileInfo(file).Directory.FullName + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(file));
					continue;
				}

				if (ext == ".json")
				{
					Console.WriteLine("Coverting to PMD: ", file);
					PmdFile pmdFile = PmdFile.FromJson(file);
					string pmdext = "PM" + pmdFile.MagicCode[3];
					pmdFile.Save(file + "." + pmdext);
					//pmdFile2.Save("./output/" + name + ".lib.json" + Path.GetExtension(filePath));
				}
			}
			Console.WriteLine("Press Any Button To Exit.");
			Console.ReadKey();
			return;
		}
	}
}
