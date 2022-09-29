using LibellusLibrary.Event.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibellusLibrary.Utils;
using System.Collections.Concurrent;
using LibellusLibrary.Utils.IO;

namespace LibellusLibrary.Event
{
	internal class PmdBuilder
	{
		internal PolyMovieData Pmd;

		public List<PmdTypeID> ExistingTypes = new();

		internal Dictionary<PmdTypeID, List<byte[]>> ReferenceTables = new();

		internal Dictionary<IReferenceType, int> nameTableReferenceIndices = new();

		internal ConcurrentDictionary<PmdTypeID, List<PmdDataType>> typeTable = new();

		internal PmdBuilder(PolyMovieData Pmd)
		{
			this.Pmd = Pmd;
		}

		// I sincerely apologize for the absolute hell that is the writing code
		//I dont know what the fuck I was smoking but I know if I touch it, the whole thing will explode
		internal async Task<MemoryStream> CreatePmd(string path)
		{
			
			MemoryStream pmdFile = new MemoryStream();
			using var writer = new BinaryWriter(pmdFile);

			//await using DisposableDictionaryAsync<PmdDataType, long> dataTypes = new();
			Dictionary<PmdDataType,long> dataTypes = new();
			// Type, offset
			foreach (PmdDataType pmdData in Pmd.PmdDataTypes)
			{
				if(pmdData is IReferenceType reference)
				{
					reference.SetReferences(this);
				}
			}
			writer.FSeek(0x20 + 0x10 * Pmd.PmdDataTypes.Count + 0x40);
			foreach (var referenceType in ReferenceTables)
			{
				var dataType = new PmdData_RawData();
				dataType.Type = referenceType.Key;
				dataType.Data = referenceType.Value;
				var start = writer.FTell();
				dataType.SaveData(this, writer);
				dataTypes.Add(dataType, start);
				//writer.Write(dataTypes[dataType].Item1.ToArray());
			}

			List<Task<MemoryStream>> writeDataTasks = new();
			foreach (PmdDataType pmdData in Pmd.PmdDataTypes)
			{
				var start = writer.FTell();
				pmdData.SaveData(this, writer);
				dataTypes.Add(pmdData, start);
				//writer.Write(dataTypes[pmdData].Item1.ToArray());
			}
			
			
			/*
			// I know this is incredibly cursed and I have no idea why this was neccessary
			var reversed = dataStreams.ToDictionary(x => x.Value, x => x.Key);
			//reversed.Reverse();
			
			// Gives us plenty of space to write
			pmdFile.Seek(0x20 + 0x10 * Pmd.PmdDataTypes.Count + 0x40, SeekOrigin.Begin);
			List<Task> writeFileTasks = new();
			foreach (var bucket in Async.Interleaved<MemoryStream>(writeDataTasks))
			{   // Process tasks as they finish ie. write to file as soon as memory buffer is done
				var t = await bucket;
				var result = await t;
				long offset = pmdFile.Position;
				offsets.Add(reversed[result], offset);
				writeFileTasks.Add(pmdFile.WriteAsync(result.GetBuffer()).AsTask());
			}
			
			await Task.WhenAll(writeFileTasks);
			*/
			// Write Header


			writer.Seek(0, SeekOrigin.Begin);
			writer.Write((int)0); // Filetype/format/userid
			writer.Write((int)pmdFile.Length);
			writer.Write(Pmd.MagicCode.ToCharArray());
			writer.Write((int)0); // Expand Size
			writer.Write(dataTypes.Count);
			writer.Write(Pmd.Version);
			writer.Write((int)0); //Reserve
			writer.Write((int)0);


			// Create Type table
			writer.FSeek(0x20);
			// Write the type table in the correct order
			//IEnumerable<KeyValuePair<PmdDataType, long>> dataTypes = offsets.Reverse();
			foreach (KeyValuePair<PmdDataType, long> dataType in dataTypes)
			{
				writer.Write((int)dataType.Key.Type);
				writer.Write((int)dataType.Key.GetSize());// Size
				writer.Write((int)dataType.Key.GetCount());
				writer.Write((int)dataType.Value); // Offset
			}


			return pmdFile;
		}


		/// <summary>
		/// Creates another datatype and returns it's index
		/// </summary>
		/// <param name="id"></param>
		/// <param name="data"></param>
		/// <returns></returns>
		internal int AddReference(PmdTypeID id, byte[] data)
		{
			if (ReferenceTables.ContainsKey(id))
			{
				ReferenceTables[id].Add(data);
				return ReferenceTables.Count-1;
			}
			ReferenceTables.Add(id, new List<byte[]>());
			ReferenceTables[id].Add(data);
			return ReferenceTables.Count - 1;
		}

	}
}
