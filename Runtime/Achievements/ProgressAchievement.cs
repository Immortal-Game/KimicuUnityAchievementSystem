using System;

namespace Kimicu.Achievements
{
	public class ProgressAchievement<T> : Achievement<T>
	{
		public readonly IAchievementProgress<T> ProgressItem;

		private T _progress;

		/// <summary> 1 - achievement instance, 2 - old value, 3 - new value </summary>
		public event Action<ProgressAchievement<T>, T, T> OnStep;

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
					CompleteAction?.Invoke(ProgressItem);
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
			Progress = ProgressItem.TargetProgress;
			base.Complete();
		}
	}
}