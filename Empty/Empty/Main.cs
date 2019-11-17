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
			BaseHuman bh = new BaseHuman(Assets.textures["Human"]);
			AddChild(bh);
			bh.x = 720;
			bh.y = 300;
			island = new Island();
			AddChild(island);
			AddChild(new UI.Property());
		}

		/// <inheritdoc />
		public override void Draw(SpriteBatch sb, GameTime gameTime = null)
		{
			sb.Begin(samplerState: SamplerState.PointClamp);
			base.Draw(sb, gameTime);
			sb.End();
		}
	}
}