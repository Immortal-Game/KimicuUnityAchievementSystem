using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Kimicu.Achievements.View
{
	public partial class NotificationRootView : MonoBehaviour
	{
		protected NotificationView NotificationViewPrefab;
		protected readonly Queue<IAchievementItem> DisplayQueue = new Queue<IAchievementItem>();

		public UnityEvent<string> OnClick = new();

		private bool _animating;
		private NotificationView _cachedNotificationView;

		public virtual void Setup(NotificationView prefab)
		{
			NotificationViewPrefab = prefab;
		}

		public virtual void Notify(IAchievementItem item)
		{
			DisplayQueue.Enqueue(item);
		}

		public virtual void Hide()
		{
			if (_cachedNotificationView == null) return;
			ClearQueue();
			_animating = false;
			Destroy(_cachedNotificationView.gameObject);
		}

		public virtual void ClearQueue()
		{
			DisplayQueue.Clear();
		}

		protected virtual void Update()
		{
			if (DisplayQueue.Count == 0 || _animating) return;
			_animating = true;
			var item = DisplayQueue.Dequeue();

			var notificationView = _cachedNotificationView = Instantiate(NotificationViewPrefab, transform);
			notificationView.OnClick.AddListener(OnClick.Invoke);
			notificationView.OnShowComplete.AddListener(() =>
			{
				Destroy(_cachedNotificationView.gameObject);
				_animating = false;
			});
			notificationView.Setup(item);
		}
	}
}