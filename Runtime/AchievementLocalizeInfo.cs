using System;

namespace Kimicu.Achievements
{
	[Serializable]
	public partial class AchievementLocalizeInfo
	{
		public readonly string Title;
		public readonly string Description;

		public AchievementLocalizeInfo(string title, string description)
		{
			Title = title;
			Description = description;
		}
	}
}