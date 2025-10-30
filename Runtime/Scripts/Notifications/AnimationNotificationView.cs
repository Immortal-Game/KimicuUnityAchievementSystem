using System;
using System.Collections;
using UnityEngine;

namespace Kimicu.Achievements.View
{
	[RequireComponent(typeof(Animation), typeof(Animator))]
	public class AnimationNotificationView : NotificationView
	{
		[SerializeField] private float _durationShowing;

		private Animator _animator;

		public override void Setup(IAchievementItem achievementItem)
		{
			_animator = GetComponent<Animator>();
			base.Setup(achievementItem);
		}

		protected override void Show()
		{
			StartCoroutine(ShowRoutine(base.Show));
		}

		private IEnumerator ShowRoutine(Action onSuccess)
		{
			_animator.Play("Show");
			yield return new WaitWhile(() => _animator.IsInTransition(0));
			yield return new WaitWhile(() => _animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f);
			
			yield return new WaitForSeconds(_durationShowing);
			
			_animator.Play("Hide");
			yield return new WaitWhile(() => _animator.IsInTransition(0));
			yield return new WaitWhile(() => _animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f);
			
			onSuccess?.Invoke();
		}
	}
}