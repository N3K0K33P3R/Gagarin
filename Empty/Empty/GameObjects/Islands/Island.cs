using Empty.Building;
using Empty.GameObjects.Humans;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoFlash.Engine;
using System.Collections.Generic;

namespace Empty.GameObjects
{
	public class Island : Sprite
	{
		public const     int             IslandSize = 16;
		private readonly IslandGenerator islandGenerator;

		private readonly int             wight;
		private readonly int             height;
		protected        List<BaseHuman> humans;
		public           TileType[,]     Cells;
		public           int             Offset;
		public           Vector2         node;
		public           Color           re = Color.White;

		public List<Structure> Structures = new List<Structure>();

		internal TileType GetCellByMouse => GetCellByPose(Main.instance.MousePosition);

		/// <inheritdoc />
		public Island(int w = IslandSize, int h = IslandSize)
		{
			wight           = w;
			height          = h;
			islandGenerator = new IslandGenerator(wight, height);
			Cells           = islandGenerator.island;

			PlaceHumans();
		}

		public void Posing(Vector2 node)
		{
			this.node = node;
		}

		public TileType[,] GetMap() => Cells;

		public void DrawIsland(SpriteBatch sb)
		{
			for (var i = 0; i < wight; i++)
			{
				for (var j = 0; j < height; j++)
				{
					if (Cells[i, j].Equals(TileType.Grass))
					{
						sb.Draw(Assets.textures["Grass"], pos(i, j, Offset), null, Color.White);
					}

					if (Cells[i, j].Equals(TileType.Sand))
					{
						sb.Draw(Assets.textures["Sand"], pos(i, j, Offset), null, Color.White);
					}

					if (Cells[i, j].Equals(TileType.Stone))
					{
						sb.Draw(Assets.textures["Stone"], pos(i, j, Offset), null, Color.White);
					}
				}
			}

			Structures.ForEach(item => item.Draw(sb));
		}

		internal TileType GetCellByPose(Vector2 vector)
		{
			if (vector.X > 0 &&
				vector.Y > 0 &&
				vector.X < wight &&
				vector.Y < height)
			{
				return Cells[(int)vector.X, (int)vector.Y];
			}

			return TileType.Empty;
		}


		private Vector2 pos(int i, int j, int offset = 0) =>
			Vector2.UnitX * 16 * i + Vector2.UnitY * 16 * j - Vector2.UnitX * offset + new Vector2((float)globalX, (float)globalY);

		private void PlaceHumans()
		{
			var humanCount = 5;
			humans = new List<BaseHuman>();

			for (var i = 0; i < islandGenerator.island.GetLength(0); i++)
			{
				var shouldBreak = false;

				for (var j = 0; j < islandGenerator.island.GetLength(1); j++)
				{
					if (islandGenerator.island[i, j] == TileType.Empty)
					{
						continue;
					}

					var bh = new BaseHuman(Assets.textures["Human"], i, j);
					AddChild(bh);
					humans.Add(bh);
					humanCount--;

					if (humanCount != 0)
					{
						continue;
					}

					shouldBreak = true;
					break;
				}

				if (shouldBreak)
				{
					break;
				}
			}
		}
	}
}