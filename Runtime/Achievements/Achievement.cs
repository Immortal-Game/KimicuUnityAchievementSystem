using UnityEngine.Events;

namespace Kimicu.Achievements
{
	public class Achievement<T>
	{
		public readonly IAchievementItem Item;

		public readonly UnityEvent<IAchievementItem> OnComplete = new();

		public bool IsComplete { get; protected set; }

		public Achievement(IAchievementItem item, bool isComplete = false)
		{
			Item = item;
			IsComplete = isComplete;
		}

		public virtual void Complete()
		{
			IsComplete = true;
			OnComplete.Invoke(Item);
		}
	}
}