using Microsoft.Xna.Framework;

namespace Empty.Helpers
{
	public static class RectangleExtensions
	{
		public static int MidX(this Rectangle rect) => rect.Left + rect.Width / 2;

		public static int MidY(this Rectangle rect) => rect.Top + rect.Height / 2;

		public static Point Center(this Rectangle rect) => new Point(rect.MidX(), rect.MidY());
	}
}