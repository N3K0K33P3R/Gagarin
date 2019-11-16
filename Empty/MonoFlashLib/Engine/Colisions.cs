using Microsoft.Xna.Framework;
using System;

namespace MonoFlashLib.Engine
{
	public class Colisions
	{
		public static Vector2 ResolveCollision(Rectangle movingObject, Rectangle staticObject)
		{
			double itemX = staticObject.Center.X;
			double itemY = staticObject.Center.Y;

			if (movingObject.Intersects(staticObject))
			{
				return Bouncing(movingObject, staticObject);
			}

			return new Vector2(float.NaN, float.NaN);
		}

		private static Vector2 Bouncing(Rectangle movingObject, Rectangle staticObject)
		{
			double itemX = staticObject.X + staticObject.Width / 2;
			double itemY = staticObject.Y + staticObject.Height / 2;

			double posX = movingObject.X + movingObject.Width / 2;
			double posY = movingObject.Y + movingObject.Height / 2;


			double tileSizeHalf   = movingObject.Width / 2f;
			double halfSizeWidth  = movingObject.Width / 2f;
			double halfSizeHeight = movingObject.Height / 2f;


			double  difX   = tileSizeHalf + halfSizeWidth - Math.Abs(posX - itemX);
			double  difY   = tileSizeHalf + halfSizeHeight - Math.Abs(posY - itemY);
			Vector2 result = movingObject.Location.ToVector2();

			if (difX < difY)
			{
				if (posX < itemX)
				{
					result.X = (float)(itemX - staticObject.Width / 2 - movingObject.Width);
				}
				else
				{
					result.X = (float)(itemX + staticObject.Width / 2);
				}
			}
			else
			{
				if (posY < itemY)
				{
					result.Y = (float)(itemY - staticObject.Height / 2 - movingObject.Height);
				}
				else
				{
					result.Y = (float)(itemY + staticObject.Height / 2);
				}
			}

			return result;
		}
	}
}