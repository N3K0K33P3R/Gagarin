using Empty.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoFlash.Engine;

namespace Empty
{
	internal class Main : Sprite
	{
		private Island island;
		/// <inheritdoc />
		public Main()
		{
			island = new Island();
			AddChild(island);
			AddChild(new UI.Property());
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