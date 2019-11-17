using Empty.GameObjects;
using Empty.GameObjects.Humans;
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
			BaseHuman bh = new BaseHuman();
			
			island = new Island();
			AddChild(island);
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