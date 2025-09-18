using System;

namespace Kimicu.Achievements
{
	public class Achievement<T> : IDisposable
	{
		public readonly IAchievementItem Item;

		protected Action<IAchievementItem> CompleteAction;
		public event Action<IAchievementItem> OnCompleteEvent;

		public bool IsComplete { get; protected set; }

		public Achievement(IAchievementItem item, bool isComplete = false)
		{
			Item = item;
			IsComplete = isComplete;
			CompleteAction += OnCompleteCallback;
		}

		public void Dispose()
		{
			CompleteAction -= OnCompleteCallback;
		}

		public virtual void Complete()
		{
			IsComplete = true;
			CompleteAction.Invoke(Item);
		}

		private void OnCompleteCallback(IAchievementItem item)
		{
			OnCompleteEvent?.Invoke(Item);
		}
	}
}