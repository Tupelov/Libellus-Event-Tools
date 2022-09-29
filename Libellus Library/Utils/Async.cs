using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibellusLibrary.Utils
{
	public static class Async
	{

		/// <summary>
		/// Allows you to await tasks as they complete
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="tasks"></param>
		/// <returns></returns>
		public static Task<Task<T>>[] Interleaved<T>(IEnumerable<Task<T>> tasks)
		{   // No clue how this works just copied it from microsoft lol
			var inputTasks = tasks.ToList();

			var buckets = new TaskCompletionSource<Task<T>>[inputTasks.Count];
			var results = new Task<Task<T>>[buckets.Length];
			for (int i = 0; i < buckets.Length; i++)
			{
				buckets[i] = new TaskCompletionSource<Task<T>>();
				results[i] = buckets[i].Task;
			}

			int nextTaskIndex = -1;
			Action<Task<T>> continuation = completed =>
			{
				var bucket = buckets[Interlocked.Increment(ref nextTaskIndex)];
				bucket.TrySetResult(completed);
			};

			foreach (var inputTask in inputTasks)
				inputTask.ContinueWith(continuation, CancellationToken.None, TaskContinuationOptions.ExecuteSynchronously, TaskScheduler.Default);

			return results;
		}
		// For when a task doesnt return anything
		public static Task<Task>[] Interleaved(IEnumerable<Task> tasks)
		{   // No clue how this works just copied it from microsoft lol
			var inputTasks = tasks.ToList();

			var buckets = new TaskCompletionSource<Task>[inputTasks.Count];
			var results = new Task<Task>[buckets.Length];
			for (int i = 0; i < buckets.Length; i++)
			{
				buckets[i] = new TaskCompletionSource<Task>();
				results[i] = buckets[i].Task;
			}

			int nextTaskIndex = -1;
			Action<Task> continuation = completed =>
			{
				var bucket = buckets[Interlocked.Increment(ref nextTaskIndex)];
				bucket.TrySetResult(completed);
			};

			foreach (var inputTask in inputTasks)
				inputTask.ContinueWith(continuation, CancellationToken.None, TaskContinuationOptions.ExecuteSynchronously, TaskScheduler.Default);

			return results;
		}
	}
}
