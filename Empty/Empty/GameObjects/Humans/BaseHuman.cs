using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoFlashLib.Engine;
using System.Linq;

namespace Empty.GameObjects.Humans
{
	public class BaseHuman
	{
		private Point tilePos;
		private TextureAtlas atlas;
		private Rectangle[] walkAnim;
		private Rectangle[] deathAnim;
		private bool isMoving;

		/// <inheritdoc />
		internal BaseHuman(Texture2D atlasTexture)
		{
			atlas = new TextureAtlas(atlasTexture, 0, 64,64, 64 );
			walkAnim = atlas.GetAnimTextureRect(0, 1, 8)[0].Take(5).ToArray();
			deathAnim = atlas.GetAnimTextureRect(6, 1, 3)[0];
		}
	}
}