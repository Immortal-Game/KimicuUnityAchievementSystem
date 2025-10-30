namespace Kimicu.Achievements
{
	public interface IAchievementProgress<out T> : IAchievementItem
	{
		public T TargetProgress { get; }
	}
}