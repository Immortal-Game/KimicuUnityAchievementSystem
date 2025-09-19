using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Kimicu.Achievements.View
{
	public partial class AchievementView : MonoBehaviour
	{
		[SerializeField] protected TMP_Text Title;
		[SerializeField] protected TMP_Text Description;
		[SerializeField] protected Button CollectButton;

		protected readonly UnityEvent Dispose = new();
		
		public virtual void Setup<T>(Achievement<T> achievement)
		{
			UpdateView(achievement);

			achievement.OnCompleteEvent += OnComplete;
			CollectButton.onClick.AddListener(Collected);
			
			Dispose.AddListener(() => achievement.OnCompleteEvent -= OnComplete);
		}

		protected virtual void OnDestroy()
		{
			Dispose.Invoke();
			Dispose.RemoveAllListeners();
		}

		protected virtual void Collected()
		{
			CollectButton.gameObject.SetActive(false);
		}

		protected virtual void OnComplete(IAchievementItem item)
		{
			CollectButton.interactable = true;
		}

		public virtual void UpdateView<T>(Achievement<T> achievement)
		{
			Title.text = achievement.Item.Title;
			Description.text = achievement.Item.Description;

			CollectButton.interactable = achievement.IsComplete;
		}
	}
}