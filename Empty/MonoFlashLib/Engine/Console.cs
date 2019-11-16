using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoFlash.Engine;

namespace MonoFlashLib.Engine
{
    internal class Console : Sprite
    {
        public bool isActive = false;
        private readonly SpriteFont spriteFont;
        private readonly StringBuilder text;

        public Console(SpriteFont spriteFont)
        {
            this.spriteFont = spriteFont;
            text = new StringBuilder();
        }

        public override void Draw(SpriteBatch sb, GameTime gameTime)
        {
            base.Draw(sb);
            if (isActive) sb.DrawString(spriteFont, text.ToString(), new Vector2(0, 0), Color.White);
        }
    }
}