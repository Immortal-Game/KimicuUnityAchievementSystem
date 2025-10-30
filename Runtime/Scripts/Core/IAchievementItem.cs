namespace Kimicu.Achievements
{
	public partial interface IAchievementItem
	{
		public string Id { get; }
		public string Title { get; }
		public string Description { get; }
	}
}