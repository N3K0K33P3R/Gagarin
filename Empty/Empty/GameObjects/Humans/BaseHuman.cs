using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoFlash.Engine;
using MonoFlashLib.Engine;
using System.Linq;

namespace Empty.GameObjects.Humans
{
	public class BaseHuman : Sprite
	{
		private Point        tilePos;
		private TextureAtlas atlas;
		private Rectangle[]  walkAnim;
		private Rectangle[]  deathAnim;
		private bool         isMoving;
		AnimatedSprite       animated;

		/// <inheritdoc />
		internal BaseHuman(Texture2D atlasTexture)
		{
			TextureAtlas ta = new TextureAtlas(atlasTexture, 0, 16, 16, 16);
			animated = new AnimatedSprite(atlasTexture, 3, 0.1f);
			animated.AddFrames(ta.GetFrames(0, 5));
			AddChild(animated);
			animated.isStarted = true;
		}
	}
}