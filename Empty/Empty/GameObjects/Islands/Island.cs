using Empty.GameObjects.Humans;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoFlash.Engine;
using System.Collections.Generic;
using System.Linq;

namespace Empty.GameObjects
{
	public class Island : Sprite
	{
		public const     int             IslandSize = 16;
		private readonly IslandGenerator islandGenerator;
		private readonly TileType[,]     cells;

		private readonly int             wight;
		private readonly int             height;
		private          List<BaseHuman> humans;
		private          BaseHuman       selectedHuman;
		public           int             Offset;
		public           Vector2         node;
		public           Color           re = Color.White;

		internal TileType GetCellByMouse
		{
			get
			{
				Vector2 vc = node /= 16;

				if (vc.X > 0 &&
					vc.Y > 0 &&
					vc.X < wight / 16f &&
					vc.Y < height / 16f)
				{
					return cells[(int)(vc.X * 16), (int)(vc.Y * 16)];
				}

				return TileType.Empty;
			}
		}

		/// <inheritdoc />
		public Island(int w = IslandSize, int h = IslandSize)
		{
			wight           = w;
			height          = h;
			islandGenerator = new IslandGenerator(wight, height);
			cells           = islandGenerator.island;

			PlaceHumans();
		}

		public override void Draw(SpriteBatch sb, GameTime gameTime = null)
		{
			for (var i = 0; i < wight; i++)
			{
				for (var j = 0; j < height; j++)
				{
					if (cells[i, j].Equals(TileType.Grass))
					{
						sb.Draw(Assets.textures["Grass"], pos(i, j, Offset), null, Color.White);
					}

					if (cells[i, j].Equals(TileType.Sand))
					{
						sb.Draw(Assets.textures["Sand"], pos(i, j, Offset), null, Color.White);
					}

					if (cells[i, j].Equals(TileType.Stone))
					{
						sb.Draw(Assets.textures["Stone"], pos(i, j, Offset), null, Color.White);
					}
				}
			}

			sb.DrawRectangle(node * Values.TILE_SIZE, Vector2.One * Values.TILE_SIZE, re, 4, 0);
			base.Draw(sb, gameTime);
		}

		public void Posing(Vector2 node)
		{
			this.node = node;
		}

		public TileType[,] GetMap() => cells;

		public void OnClick(Point tile)
		{
			if (selectedHuman == null)
			{
				BaseHuman human = humans.FirstOrDefault(h => h.tilePos == (tile));

				if (human != null)
				{
					selectedHuman = human;
				}
			}
			else
			{
				(int x1, int x2) = (tile);
				selectedHuman.SetTilePos(x1, x2);
				selectedHuman = null;
			}
		}


		internal TileType GetCellByPose(Vector2 vector)
		{
			if (vector.X > 0 &&
				vector.Y > 0 &&
				vector.X < wight &&
				vector.Y < height)
			{
				return cells[(int)vector.X, (int)vector.Y];
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