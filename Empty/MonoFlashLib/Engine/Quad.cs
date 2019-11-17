using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoFlash.Engine
{
	public class Quad : Sprite
	{
		public           uint  color;
		private readonly int   depth;
		public           Color XNAColor;

		public Quad(float width, float height, uint color, float alpha = 1, int depth = 0)
		{
			this.width  = width;
			this.height = height;
			this.color  = color;
			this.depth  = depth;
			this.alpha  = alpha;
		}

		public override void Draw(SpriteBatch sb, GameTime gameTime)
		{
			XNAColor   = Colors.hexToRGB(color);
			XNAColor.A = (byte)(alpha * 255);
			sb.FillRectangle((float)globalX, (float)globalY, (float)width, (float)height, XNAColor, depth);
			base.Draw(sb);
		}
	}
}