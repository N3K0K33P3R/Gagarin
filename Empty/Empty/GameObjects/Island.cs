using MonoFlash.Engine;

namespace Empty.GameObjects
{
	public class Island : Sprite
	{
		private IslandGenerator islandGenerator;

		/// <inheritdoc />
		public Island()
		{
			islandGenerator = new IslandGenerator(50, 50);
		}
	}
}