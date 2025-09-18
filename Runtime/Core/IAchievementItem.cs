using System.Collections.Generic;

namespace Kimicu.Achievements
{
	public interface IAchievementItem
	{
		public string Id { get; }
		public Dictionary<string, AchievementLocalizeInfo> LocalizeViewData { get; }
	}
}