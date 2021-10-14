using Newtonsoft.Json;
using LibellusLibrary.IO;

namespace LibellusLibrary.PMD.Types
{

	public abstract class DataType : FileBase
	{
		[JsonIgnore] public DataTypeID TypeID;
	}

}
