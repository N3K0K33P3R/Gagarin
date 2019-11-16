using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoFlash.Engine;

namespace Empty
{
    internal class Main : Sprite
    {
        public Main()
        {
            AddChild(new Inventory.Inventory());
        }

        public override void Draw(SpriteBatch sb, GameTime gameTime = null)
        {
            sb.Begin();
            base.Draw(sb, gameTime);
            sb.End();
        }
    }
}