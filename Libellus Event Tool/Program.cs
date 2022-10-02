using LibellusLibrary.Event;


namespace LibellusEventTool
{
	class Program
	{
		static async Task Main(string[] args)
		{

			System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
			System.Diagnostics.FileVersionInfo fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(assembly.Location);
			string version = fvi.FileVersion;
			Console.WriteLine("Welcome to LEET!\nLibellus Event Editing Tool: v" + version + " \nNow with better syntax!\n");

			if (args.Length < 1)
			{
				Console.WriteLine("Not Enough args!");
				Console.WriteLine("Press Any Button To Exit.");
				Console.ReadKey();
				return;
			}
			foreach (string file in args)
			{
				string ext = Path.GetExtension(file).ToLower();
				if (ext == ".pm1" || ext == ".pm2" || ext == ".pm3")
				{
					Console.WriteLine("Coverting to Json: "+ file);;
					PmdReader reader = new PmdReader();
					PolyMovieData pmd = await reader.ReadPmd(file);
					string folder = Path.Combine(Path.GetDirectoryName(file), Path.GetFileNameWithoutExtension(file));
					await pmd.ExtractPmd(folder, Path.GetFileName(file));
					continue;
				}

				if (ext == ".json")
				{
					Console.WriteLine("Coverting to PMD: "+ file);
					PolyMovieData pmd = await PolyMovieData.LoadPmd(Path.Combine(file));
					string pmdext = "PM" + pmd.MagicCode[3];
					pmd.SavePmd(file + "." + pmdext);
				}
			}
			Console.WriteLine("Press Any Button To Exit.");
			Console.ReadKey();
		}
	}
}