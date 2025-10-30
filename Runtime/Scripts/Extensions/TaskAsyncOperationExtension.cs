using System.Threading.Tasks;
using UnityEngine;

namespace Kimicu.Achievements.Extensions
{
	public static class TaskAsyncOperationExtension
	{
		public static async Task<T> WaitAsyncResource<T>(this ResourceRequest request) where T : Object
		{
			var tcs = new TaskCompletionSource<T>();
        
			request.completed += operation => 
			{
				if (request.asset is T asset)
				{
					tcs.SetResult(asset);
				}
				else
				{
					tcs.SetException(new System.Exception($"Failed to load resource"));
				}
			};
        
			return await tcs.Task;
		}
	}
}