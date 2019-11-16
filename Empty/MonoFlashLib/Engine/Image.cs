using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoFlash.Engine
{
    public class Image : Sprite
    {
        protected SpriteEffects se;
        protected Texture2D texture;

        public Image(Texture2D texture)
        {
            this.texture = texture;
            width = texture.Width;
            height = texture.Height;
        }

        public override void Update(float delta)
        {
            base.Update(delta);
            width = texture.Width * ScaleX;
            height = texture.Height * ScaleY;
        }

        public override void Draw(SpriteBatch sb, GameTime gameTime)
        {
            sb.Draw(
                texture,
                scale: new Vector2((float) ScaleX, (float) ScaleY),
                rotation: (float) rotation,
                color: colorAlpha,
                position: new Vector2((int) globalX, (int) globalY),
                //position: new Vector2((int)globalX, (int)globalY),
                effects: se,
                origin: new Vector2(texture.Width / 2, texture.Height / 2)
            );
            base.Draw(sb);
        }
    }
}