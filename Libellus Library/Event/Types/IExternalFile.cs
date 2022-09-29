using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibellusLibrary.Event.Types
{
	internal interface IExternalFile
	{
		public Task SaveExternalFile(string directory);
		public Task LoadExternalFile(string directory);
	}
}
