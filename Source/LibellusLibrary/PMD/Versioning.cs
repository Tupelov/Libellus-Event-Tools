using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibellusLibrary.PMD
{

	public enum FormatVersion : int
	{
		v3_Nocturne = 3,
		v4_Nocturne = 4,
		v12_Persona3_4=12,
		v13_Persona3_4 = 13,
	}
	// Classes that implement this will have FormatVersion at the end of all of their constructors
	interface IVersioning
	{
		public FormatVersion[] GetSupportedVersions();
	}

	interface IVersioningHandler
	{
		public Type GetTypeFromVersion(FormatVersion version);
	}
}
