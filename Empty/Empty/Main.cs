using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoFlash.Engine;

namespace Empty
{
	internal class Main : Sprite
	{
		/// <inheritdoc />
		public Main()
		{
			IslandGenerator ig = new IslandGenerator(25, 25);
			AddChild(ig);
		}

		/// <inheritdoc />
		public override void Draw(SpriteBatch sb, GameTime gameTime = null)
		{
			sb.Begin();
			base.Draw(sb, gameTime);
			sb.End();
		}
	}
}