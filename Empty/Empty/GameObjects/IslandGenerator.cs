using Empty.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoFlash.Engine;
using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace Empty
{
	public class IslandGenerator : Sprite
	{
		private readonly  Point[]     polygon;
		internal readonly TileType[,] island;
		private           int         Width  { get; }
		private           int         Height { get; }

		/// <inheritdoc />
		public IslandGenerator(int width, int height)
		{
			Width  = width;
			Height = height;

			Vector2[] tempPolygon = MakeRandomPolygon(10, new Rectangle(0, 0, width, height));
			float     minX        = tempPolygon.Min(x => x.X);
			float     minY        = tempPolygon.Min(x => x.Y);
			polygon = tempPolygon.Select(x => new Point((int)(x.X - minX), (int)(x.Y - minY))).ToArray();

			island = new TileType[width, height];

			for (int i = 0; i < width; i++)
			{
				for (int j = 0; j < height; j++)
				{
					Point point = new Point(i, j);

					if (PolygonHelper.IsPointInPolygon(point, polygon))
					{
						var d = Values.RANDOM.NextDouble();

						if (d < 0.5)
						{
							island[i, j] = TileType.Grass;
						}
						else if (d < 0.8)
						{
							island[i, j] = TileType.Sand;
						}
						else
						{
							island[i, j] = TileType.Stone;
						}
					}
				}
			}
		}

		public void GenerateIsland() { }

		private Vector2[] MakeRandomPolygon(int numVertices, Rectangle bounds)
		{
			// Pick random radii.

			var          radii     = new double[numVertices];
			const double minRadius = 0.5;
			const double maxRadius = 1.0;

			for (var i = 0; i < numVertices; i++)
			{
				radii[i] = Values.RANDOM.NextDouble(minRadius, maxRadius);
			}

			// Pick random angle weights.
			var          angleWeights = new double[numVertices];
			const double minWeight    = 1.0;
			const double maxWeight    = 10.0;
			double       totalWeight  = 0;

			for (var i = 0; i < numVertices; i++)
			{
				angleWeights[i] =  Values.RANDOM.NextDouble(minWeight, maxWeight);
				totalWeight     += angleWeights[i];
			}

			// Convert the weights into fractions of 2 * Pi radians.
			var    angles  = new double[numVertices];
			double radians = 2 * Math.PI / totalWeight;

			for (var i = 0; i < numVertices; i++)
			{
				angles[i] = angleWeights[i] * radians;
			}

			// Calculate the points' locations.
			var    points = new Vector2[numVertices];
			float  rx     = bounds.Width / 2f;
			float  ry     = bounds.Height / 2f;
			float  cx     = bounds.MidX();
			float  cy     = bounds.MidY();
			double theta  = 0;

			for (var i = 0; i < numVertices; i++)
			{
				points[i] = new Vector2(
					cx + (int)(rx * radii[i] * Math.Cos(theta)),
					cy + (int)(ry * radii[i] * Math.Sin(theta)));

				theta += angles[i];
			}

			// Return the points.
			return points;
		}
	}
}