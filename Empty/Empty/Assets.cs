using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Empty
{
	internal class Assets
	{
		public static Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();
		public static List<Texture2D>               clouds   = new List<Texture2D>();
		public static SpriteFont                    font;
	}
}