using System;

namespace Kimicu.Achievements
{
	[Serializable]
	public readonly partial struct ProgressAchievementItem<T> : IAchievementProgress<T>
	{
		public string Id { get; }
		public string Title { get; }
		public string Description { get; }

		public T TargetProgress { get; }

		public ProgressAchievementItem(string id, string title, string description, T targetProgress)
		{
			Id = id;
			Title = title;
			Description = description;
			TargetProgress = targetProgress;
		}
	}
}