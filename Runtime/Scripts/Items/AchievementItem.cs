using System;

namespace Kimicu.Achievements
{
	[Serializable]
	public partial class AchievementItem : IAchievementItem
	{
		public string Id { get; }
		public string Title { get; }
		public string Description { get; }

		public AchievementItem(string id, string title, string description)
		{
			Id = id;
			Title = title;
			Description = description;
		}
	}
}