using TMPro;
using UnityEngine;

namespace Kimicu.Achievements.View
{
	public partial class ProgressAchievementView : AchievementView
	{
		[SerializeField] protected TMP_Text ProgressTMP;

		public override void Setup<T>(Achievement<T> achievement, string localizeKey)
		{
			base.Setup<T>(achievement, localizeKey);
			var progressAchievement = (ProgressAchievement<T>)achievement;

			progressAchievement.OnStep += OnStep;

			Dispose.AddListener(() => progressAchievement.OnStep -= OnStep);
		}

		protected virtual void OnStep<T>(ProgressAchievement<T> achievement, T oldProgress, T newProgress)
		{
			ProgressTMP.text = $"{achievement.ProgressToString}";
		}

		protected override void OnComplete(IAchievementItem item)
		{
			base.OnComplete(item);
			ProgressTMP.text = "100 %";
		}

		public override void UpdateView<T>(Achievement<T> achievement, string localizeKey)
		{
			base.UpdateView(achievement, localizeKey);
			var progressAchievement = (ProgressAchievement<T>)achievement;
			ProgressTMP.text = $"{progressAchievement.Progress.ToString()} / {progressAchievement.ProgressItem.TargetProgress.ToString()}";
		}
	}
}