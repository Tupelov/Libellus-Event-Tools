namespace LibellusLibrary.PMD.Types
{
	interface IExternalFile
	{
		public int NameIndex { get; set; }
		public byte[] Data { get; set; }
		public int DataSize { get; set; } // Only used during reading

		public void SaveFile(string dir, string file);
		public void LoadFile(string dir, string file);
	}
}
