using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibellusLibrary.Utils
{
	public sealed class DisposableCollection<T> : Collection<T>, IDisposable
	where T : IDisposable
	{
		public DisposableCollection() 
			: base()
		{

		}

		public DisposableCollection(IList<T> items)
			: base(items)
		{
		}

		public void Dispose()
		{
			foreach (var item in this)
			{
				try
				{
					item.Dispose();
				}
				catch
				{
					// swallow
				}
			}
		}
	}

	public sealed class DisposableDictionaryAsync<TKey,TValue> : Dictionary<TKey,TValue>, IAsyncDisposable
	where TValue : IAsyncDisposable
	{
		public DisposableDictionaryAsync() 
			: base()
		{
		}

		public DisposableDictionaryAsync(IDictionary<TKey,TValue> items)
			: base(items)
		{
		}


		public async ValueTask DisposeAsync()
		{
			List<Task> tasks= new();
			foreach (var item in this)
			{
				tasks.Add( item.Value.DisposeAsync().AsTask());
			}
			await Task.WhenAll(tasks);
			return;

		}
	}
}
