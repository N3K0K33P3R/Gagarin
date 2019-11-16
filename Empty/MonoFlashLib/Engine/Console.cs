using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoFlash.Engine;
using System.Text;

namespace MonoFlashLib.Engine
{
	internal class Console : Sprite
	{
		private readonly SpriteFont    spriteFont;
		private readonly StringBuilder text;
		public           bool          isActive = false;

		public Console(SpriteFont spriteFont)
		{
			this.spriteFont = spriteFont;
			text            = new StringBuilder();
		}

		public override void Draw(SpriteBatch sb, GameTime gameTime)
		{
			base.Draw(sb);

			if (isActive)
			{
				sb.DrawString(spriteFont, text.ToString(), new Vector2(0, 0), Color.White);
			}
		}
	}
}