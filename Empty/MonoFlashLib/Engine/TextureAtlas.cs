using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MonoFlashLib.Engine
{
	public class TextureAtlas
	{
		private readonly int       offset;
		private          int       tilesetTileSize;
		public           Texture2D atlas;
		public           int       atlasTileHeight;
		public           int       atlasTileWidth;

		public int tileWidth,
				   tileHeight;

		/// <summary>
		/// Создать из текстуры
		/// </summary>
		/// <param name="atlas">Атлас</param>
		/// <param name="offset">Смещение</param>
		/// <param name="tilesetTileSize">Размер одного тайла</param>
		/// <param name="tileWidth">Количество тайлов в ширину</param>
		/// <param name="tileHeight">И в высоту</param>
		public TextureAtlas(
			Texture2D atlas,
			int offset = 0,
			int tilesetTileSize = 32,
			int tileWidth = 32,
			int tileHeight = 32)
		{
			this.offset          = offset;
			this.atlas           = atlas;
			atlasTileWidth       = atlas.Width / tileWidth;
			atlasTileHeight      = atlas.Height / tileHeight;
			this.tilesetTileSize = tilesetTileSize;
			this.tileWidth       = tileWidth;
			this.tileHeight      = tileHeight;
		}

		/// <summary>
		/// Получить определённый прямоугольник с атласа
		/// </summary>
		/// <param name="gid">ID прямоугольника</param>
		/// <returns></returns>
		public Rectangle GetTextureRect(int gid)
		{
			int column = gid % atlasTileWidth;
			var row    = (int)Math.Floor((double)gid / atlasTileWidth);


			var tilesetRec = new Rectangle(tileWidth * column, tileHeight * row, tileWidth, tileHeight);


			return tilesetRec;
		}

		/// <summary>
		/// Получить четыре текстуры, начиная с определённой
		/// </summary>
		/// <param name="gid"></param>
		/// <returns></returns>
		public Rectangle[] Get4SideTextureRect(int gid)
		{
			var rects = new Rectangle[4];

			for (var i = 0; i < 4; i++)
			{
				rects[i] = GetTextureRect(gid * 4 + i + offset);
			}

			return rects;
		}

		/// <summary>
		/// Получить фреймы для анимации
		/// </summary>
		/// <param name="gid">ID начальной</param>
		/// <param name="rows">Количество строк</param>
		/// <param name="columns">Количество столбцов</param>
		/// <returns></returns>
		public Rectangle[][] GetAnimTextureRect(int gid, int rows, int columns)
		{
			var rects = new Rectangle[rows][];

			for (var i = 0; i < rows; i++)
			{
				rects[i] = new Rectangle[columns];
			}

			for (var i = 0; i < rows; i++)
			{
				for (var j = 0; j < columns; j++)
				{
					rects[i][j] = GetTextureRect(gid + j + i * atlasTileWidth);
				}
			}

			return rects;
		}

		/// <summary>
		/// Получить текстуру по её координатам
		/// </summary>
		/// <param name="i">x</param>
		/// <param name="j">y</param>
		/// <returns></returns>
		public Rectangle GetTextureByCoordinates(int i, int j)
		{
			var res = new Rectangle(i * tileWidth, j * tileHeight, tileWidth, tileHeight);
			return res;
		}

		public Rectangle[] GetFrames(int gid, int count)
		{
			var res = new Rectangle[count];

			for (int i = 0; i < count; i++)
			{
				res[i] = new Rectangle((gid + i) * tilesetTileSize, 0, tilesetTileSize, tilesetTileSize);
			}

			return res;
		}
	}
}