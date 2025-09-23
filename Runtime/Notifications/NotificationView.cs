using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Kimicu.Achievements.View
{
	public class NotificationView : MonoBehaviour, IPointerUpHandler
	{
		protected string Id { get; private set; }
		
		[SerializeField] protected TMP_Text Title;
		[SerializeField] protected TMP_Text Description;

		public UnityEvent<string> OnClick = new();
		public UnityEvent OnShowComplete = new();

		public virtual void Setup(IAchievementItem achievementItem)
		{
			Id = achievementItem.Id;
			Title.text = achievementItem.Title;
			Description.text = achievementItem.Description;
			Show();
		}

		protected virtual void Show()
		{
			OnShowComplete?.Invoke();
		}

		public void OnPointerUp(PointerEventData eventData) => OnClick?.Invoke(Id);
	}
}