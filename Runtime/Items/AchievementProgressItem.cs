using System;
using System.Collections.Generic;

namespace Kimicu.Achievements
{
	[Serializable]
	public readonly struct AchievementProgressItem<T> : IAchievementProgress<T>
	{
		/// <summary> Unique key achievement </summary>
		public string Id { get; }
		
		/// <summary> example: key["ru"] = {"title", "description"} </summary>
		public Dictionary<string, AchievementLocalizeInfo> LocalizeViewData { get; }

		/// <summary> T - is progress type (int, float, List, ...) </summary>
		public T TargetProgress { get; }

		public AchievementProgressItem(string id, Dictionary<string, AchievementLocalizeInfo> localizeViewData, T targetProgress)
		{
			Id = id;
			LocalizeViewData = localizeViewData;
			TargetProgress = targetProgress;
		}
	}
}