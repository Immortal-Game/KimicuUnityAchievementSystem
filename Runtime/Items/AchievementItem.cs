using System;
using System.Collections.Generic;

namespace Kimicu.Achievements
{
	[Serializable]
	public readonly struct AchievementItem : IAchievementItem
	{
		/// <summary> Unique key achievement </summary>
		public string Id { get; }
		
		/// <summary> example: key["ru"] = {"title", "description"} </summary>
		public Dictionary<string, AchievementLocalizeInfo> LocalizeViewData { get; }

		public AchievementItem(string id, Dictionary<string, AchievementLocalizeInfo> localizeViewData)
		{
			Id = id;
			LocalizeViewData = localizeViewData;
		}
	}
}