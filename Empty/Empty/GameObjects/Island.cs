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
		private readonly TileType[,]     cells;

		private readonly int     wight;
		private readonly int     height;
		public           int     Offset;
		public           Vector2 node;
		public           Color   re = Color.White;

        public List<Building.Structure> Structures= new List<Building.Structure>();

        internal TileType GetCellByMouse => GetCellByPose(Main.instance.MousePosition);
        
		/// <inheritdoc />
		public Island(int w = IslandSize, int h = IslandSize)
		{
			wight           = w;
			height          = h;
			islandGenerator = new IslandGenerator(wight, height);
			cells           = islandGenerator.island;
		}

		public override void Draw(SpriteBatch sb, GameTime gameTime = null)
		{
			for (var i = 0; i < wight; i++)
			{
				for (var j = 0; j < height; j++)
				{
					if (cells[i, j].Equals(TileType.Grass))
					{
						sb.Draw(Assets.textures["Grass"], pos(i, j, Offset), null, color: Color.White);
					}

					if (cells[i, j].Equals(TileType.Sand))
					{
						sb.Draw(Assets.textures["Sand"], pos(i, j, Offset), null, color: Color.White);
					}

					if (cells[i, j].Equals(TileType.Stone))
					{
						sb.Draw(Assets.textures["Stone"], pos(i, j, Offset), null, color: Color.White);
					}
				}
			}

            Structures.ForEach((item) => item.Draw(sb));
            sb.DrawRectangle(node*Values.TILE_SIZE, Vector2.One * Values.TILE_SIZE, re, 1, 0);
			base.Draw(sb, gameTime);
		}            
        
		public void Posing(Vector2 node)
		{
			this.node = node;
		}

		public TileType[,] GetMap() => cells;


		internal TileType GetCellByPose(Vector2 vector)
		{
			if (vector.X > 0 &&
				vector.Y > 0 &&
				vector.X < wight &&
				vector.Y < height)
			{
				return cells[(int)(vector.X), (int)(vector.Y)];
			}

			return TileType.Empty;
		}


		private Vector2 pos(int i, int j, int offset = 0) => Vector2.UnitX * 16 * i + Vector2.UnitY * 16 * j - Vector2.UnitX * offset;
	}
}