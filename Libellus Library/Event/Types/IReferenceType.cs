using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibellusLibrary.Event.Types
{
	internal interface IReferenceType
	{
		public void SetReferences(PmdBuilder pmdBuilder);
	}
}
