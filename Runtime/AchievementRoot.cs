using System.Threading.Tasks;
using Kimicu.Achievements.Extensions;
using Kimicu.Achievements.View;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Kimicu.Achievements
{
	public partial class AchievementRoot
	{
		public AchievementRootView View;

		public static async Task<AchievementRoot> Initialize(string pathView = "AchievementRootView")
		{
			var instance = new AchievementRoot();
			await instance.Setup(pathView);
			return instance;
		}

		private AchievementRoot()
		{
		}

		private async Task Setup(string pathView)
		{
			if (string.IsNullOrEmpty(pathView)) pathView = "AchievementRootView";
			var request = Resources.LoadAsync<AchievementRootView>(pathView);
			var prefab = await request.WaitAsyncResource<AchievementRootView>();

			View = Object.Instantiate(prefab);
			Object.DontDestroyOnLoad(View);
		}
	}
}