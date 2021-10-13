using System;
using System.IO;
using LibellusLibrary.PmdFile;

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
			foreach(string file in args)
			{
				string ext = Path.GetExtension(file).ToLower();
				if (ext == ".pm1" || ext == ".pm2" || ext == ".pm3") 
				{
					Console.WriteLine("Coverting to Json: ", file);
					PmdFile pmdFile = new(file);
					string json = pmdFile.ToJson();

					using (BinaryWriter writer = new BinaryWriter(File.Create(file + ".json")))
					{
						writer.Write(json);
					}
					continue;
				}
				
				if(ext == ".json")
				{
					Console.WriteLine("Coverting to PMD: ", file);
					string json = File.ReadAllText(file);
					PmdFile pmdFile = PmdFile.FromJson(json);
					string pmdext = new(pmdFile.MagicCode);
					pmdFile.Save(file + "." + pmdext);
					//pmdFile2.Save("./output/" + name + ".lib.json" + Path.GetExtension(filePath));
				}
			}
			Console.WriteLine("Exiting...");
			return;
		}
	}
}
