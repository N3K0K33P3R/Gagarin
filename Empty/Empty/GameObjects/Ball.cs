using Empty.Building;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoFlash.Engine;
using System;

namespace Empty.GameObjects
{
	public class Ball : Sprite
	{
		public           bool   ShouldDelete { get; set; }
		private          Point  target;
		private readonly Island i1;
		private readonly Island i2;
		private readonly Cannon parent;
		private          float  speed = 8;

		private float tX,
					  tY;

		private int r = Values.TILE_SIZE / 2;

		/// <inheritdoc />
		public Ball(Point start, Point target, Island i1, Island i2, Cannon parent)
		{
			this.target = target;
			this.i1     = i1;
			this.i2     = i2;
			this.parent = parent;
			x           = start.X * Values.TILE_SIZE;
			y           = start.Y * Values.TILE_SIZE;

			float angle = GetAngle(start.X, start.Y, target.X, target.Y);
			tX = (float)Math.Cos(angle);
			tY = (float)Math.Sin(angle);
		}

		/// <inheritdoc />
		public override void Draw(SpriteBatch sb, GameTime gameTime = null)
		{
			sb.DrawCircle((float)globalX + r, (float)globalY + r, r, 100, Color.Black, r, 0);
			base.Draw(sb, gameTime);
		}

		/// <inheritdoc />
		public override void Update(float delta)
		{
			x += tX * speed;
			y += tY * speed;

			if (new Point((int)(x / Values.TILE_SIZE), (int)(y / Values.TILE_SIZE)) == target)
			{
				ShouldDelete = true;
			}

			foreach (Structure structure in i1.Structures)
			{
				if (structure == parent)
				{
					continue;
				}

				Point center = (structure.position + Vector2.One * Values.TILE_SIZE / 2).ToPoint();

				if (center.GetDist(new Point((int)x + r, (int)y + r)) < r)
				{
					ShouldDelete           = true;
					structure.ShouldDelete = true;
				}
			}

			if (i2 != null)
			{
				foreach (Structure structure in i2.Structures)
				{
					Point center = (new Vector2((float)structure.globalX + structure.position.X, (float)structure.globalY + structure.position.Y) + Vector2.One * Values.TILE_SIZE / 2).ToPoint();
						//Trace(center.GetDist(new Point((int)globalX + r, (int)globalY + r)));
					//Trace(globalX, globalY, structure.globalX + structure.position.X, structure.globalY + structure.position.Y);
					if (center.GetDist(new Point((int)x + r, (int)y + r)) < r)
					{
						ShouldDelete           = true;
						structure.ShouldDelete = true;
					}
				}
			}

			
			
			base.Update(delta);
		}


		public static float GetAngle(Vector2 v1, Vector2 v2) => GetAngle(v1.X, v1.Y, v2.X, v2.Y);

		public static float GetAngle(float x1, float y1, float x2, float y2)
		{
			float xDiff = x2 - x1;
			float yDiff = y2 - y1;
			return (float)(Math.Atan2(yDiff, xDiff));
		}
	}
}