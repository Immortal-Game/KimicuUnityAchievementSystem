using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Kimicu.Achievements.View
{
	public sealed partial class AchievementRootView : MonoBehaviour
	{
		[SerializeField] private GameObject _view;
		[SerializeField] private Transform _container;

		private readonly Dictionary<string, AchievementView> _achievementViews = new();
		private readonly UnityEvent _achievementsUpdate = new();

		public void Setup<T>(Dictionary<AchievementView, Achievement<T>[]> achievements)
		{
			foreach (var achievementPair in achievements)
			{
				foreach (var achievement in achievementPair.Value)
				{
					var view = Instantiate(achievementPair.Key, _container);
					view.Setup(achievement);
					_achievementViews.Add(achievement.Item.Id, view);
					_achievementsUpdate.AddListener(() => view.UpdateView(achievement));
				}
			}
		}

		public void Show()
		{
			_view.SetActive(true);
			Redraw();
		}

		public void Hide() => _view.SetActive(false);

		public void Redraw()
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

		public void UpdateValues()
		{
			_achievementsUpdate?.Invoke();
		}
	}
}