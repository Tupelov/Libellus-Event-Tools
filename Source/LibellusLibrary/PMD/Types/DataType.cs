using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using LibellusLibrary.IO;

namespace LibellusLibrary.PMD.Types
{
    
	public abstract class DataType: FileBase
	{
        [JsonIgnore] public DataTypeID TypeID;
	}
    
}
