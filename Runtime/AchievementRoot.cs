using System.Threading.Tasks;
using Kimicu.Achievements.Extensions;
using Kimicu.Achievements.View;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Kimicu.Achievements
{
	public partial class AchievementRoot
	{
		public AchievementRootView RootView;
		public NotificationRootView NotificationRootView;

		private AchievementRoot() { }

		public static async Task<AchievementRoot> Initialize(Transform uiContainer, string pathView = "", string pathNotificationView = "")
		{
			var instance = new AchievementRoot();
			await instance.Setup(uiContainer, pathView, pathNotificationView);
			return instance;
		}

		public static AchievementRoot Initialize(Transform uiContainer, AchievementRootView rootViewPrefab, NotificationView notificationViewPrefab)
		{
			var instance = new AchievementRoot();
			instance.Setup(uiContainer, rootViewPrefab, notificationViewPrefab);
			return instance;
		}

		private async Task Setup(Transform uiContainer, string pathView, string pathNotificationView)
		{
			if (string.IsNullOrEmpty(pathView)) pathView = "AchievementRootView";
			var request = Resources.LoadAsync<AchievementRootView>(pathView);
			var prefab = await request.WaitAsyncResource<AchievementRootView>();

			RootView = Object.Instantiate(prefab, uiContainer);


			if (string.IsNullOrEmpty(pathNotificationView)) pathNotificationView = "AnimationNotificationView";
			request = Resources.LoadAsync<NotificationView>(pathNotificationView);
			var notificationPrefab = await request.WaitAsyncResource<NotificationView>();

			NotificationRootView = InstanceNotificationRootView(uiContainer);
			NotificationRootView.Setup(notificationPrefab);
		}

		private void Setup(Transform uiContainer, AchievementRootView rootViewPrefab, NotificationView notificationViewPrefab)
		{
			RootView = Object.Instantiate(rootViewPrefab, uiContainer);
			NotificationRootView = InstanceNotificationRootView(uiContainer);
			NotificationRootView.Setup(notificationViewPrefab);
		}

		private static NotificationRootView InstanceNotificationRootView(Transform uiContainer)
		{
			var container = new GameObject("NotificationRootView");
			container.transform.SetParent(uiContainer, false);
			var rectTransformContainer = container.AddComponent<RectTransform>();
			rectTransformContainer.anchorMin = Vector2.zero;
			rectTransformContainer.anchorMax = Vector2.one;
			rectTransformContainer.offsetMin = Vector2.zero;
			rectTransformContainer.offsetMax = Vector2.zero;
			return container.AddComponent<NotificationRootView>();
		}
	}
}