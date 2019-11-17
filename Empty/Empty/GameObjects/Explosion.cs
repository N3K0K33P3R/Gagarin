using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoFlash.Engine;
using MonoFlashLib.Engine;

namespace Empty.GameObjects
{
	public class Explosion : Sprite
	{
		public bool ShouldDelete { get; set; }
		private readonly AnimatedSprite sprite;

		public Explosion(Texture2D texture2D, double x, double y)
		{
			sprite = new AnimatedSprite(texture2D, onEnd: () => ShouldDelete = true);
			var         textureAtlas = new TextureAtlas(texture2D, 0, 16, 16, 16);
			Rectangle[] frames       = textureAtlas.GetFrames(0, 8);
			sprite.AddFrames(frames);
			sprite.defaultSprite = frames[0];
			sprite.isStarted = true;
			AddChild(sprite);

			this.x = x;
			this.y = y;
		}
	}
}