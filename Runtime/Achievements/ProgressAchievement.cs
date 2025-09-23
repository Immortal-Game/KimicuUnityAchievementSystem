using System;
using UnityEngine.Events;

namespace Kimicu.Achievements
{
	public class ProgressAchievement<T> : Achievement<T>
	{
		public readonly IAchievementProgress<T> ProgressItem;

		private T _progress;

		/// <summary> 1 - achievement instance, 2 - old value, 3 - new value </summary>
		public UnityEvent<ProgressAchievement<T>, T, T> OnStep = new();

		public Func<string> ProgressToString;

		public T Progress
		{
			get => _progress;
			set
			{
				if (IsComplete) return;
				OnStep?.Invoke(this, _progress, value);
				_progress = value;
				if (_progress.Equals(ProgressItem.TargetProgress))
				{
					OnComplete?.Invoke(ProgressItem);
				}
			}
		}

		public ProgressAchievement(IAchievementProgress<T> progressItem, T startProgress = default(T), bool isComplete = false)
			: base(progressItem, isComplete)
		{
			ProgressItem = progressItem;
			IsComplete = isComplete;
			Progress = startProgress;
		}

		public override void Complete()
		{
			base.Complete();
			Progress = ProgressItem.TargetProgress;
		}
	}
}