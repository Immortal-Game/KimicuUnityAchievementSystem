using System;

namespace Kimicu.Achievements
{
	[Serializable]
	public readonly partial struct AchievementProgressItem<T> : IAchievementProgress<T>
	{
		public string Id { get; }
		public string Title { get; }
		public string Description { get; }

		public T TargetProgress { get; }

		public AchievementProgressItem(string id, string title, string description, T targetProgress)
		{
			Id = id;
			Title = title;
			Description = description;
			TargetProgress = targetProgress;
		}
	}
}