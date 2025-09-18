using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Kimicu.Achievements.View
{
	public partial class AchievementRootView : MonoBehaviour
	{
		[SerializeField] private Transform _container;

		private readonly Dictionary<string, AchievementView> _achievementViews = new();

		public virtual void Setup<T>(string localizeKey, Dictionary<AchievementView, Achievement<T>[]> achievements)
		{
			foreach (var achievementPair in achievements)
			{
				foreach (var achievement in achievementPair.Value)
				{
					var view = Instantiate(achievementPair.Key, _container);
					view.Setup<T>(achievement, localizeKey);
					_achievementViews.Add(achievement.Item.Id, view);
				}
			}
		}

		public virtual void Show() { }
		public virtual void Hide() { }

		public virtual void Redraw()
		{
			var childCount = _container.childCount;
			for (var i = 0; i < childCount; i++)
			{
				var child = _container.GetChild(i).gameObject;
				if (_achievementViews.Any(pair => pair.Value.gameObject == child)) continue;
				Destroy(child);
			}
			UpdateValues();
		}

		public virtual void UpdateValues()
		{
			foreach (var pair in _achievementViews)
			{
				//pair.Value.Setup();
			}
		}
	}
}