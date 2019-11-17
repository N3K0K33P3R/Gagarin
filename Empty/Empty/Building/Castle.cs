using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoFlash.Engine;
using Empty.GameObjects;
using System;
namespace Empty.Building
{
    public class Castle
    {
        public int Healty = 1000;
        private Rectangle rect;
        private readonly Texture2D texture;
        private readonly Color startColor;

        private Island island;

        public Point position;

        public Castle(Rectangle rect, Texture2D texture, Color startColor, Island island)
        {
            this.rect = rect;
            this.texture = texture;
            this.startColor = startColor;
            this.island = island;

            var pt = GetCastlePoint;



            island.Cells[pt.X - 1, pt.Y] = TileType.Castle;
            island.Cells[pt.X, pt.Y] = TileType.Castle;
            island.Cells[pt.X + 1, pt.Y] = TileType.Castle;

            island.Cells[pt.X - 1, pt.Y + 1] = TileType.Castle;
            island.Cells[pt.X, pt.Y + 1] = TileType.Castle;
            island.Cells[pt.X + 1, pt.Y + 1] = TileType.Castle;

            position = new Point((pt.X - 1) * 16, (pt.Y - 1) * 16);

        }

        public void Draw(SpriteBatch spriteBatch)
        {

            rect.Location = new Point((int)island.globalX, (int)island.globalY) + position;

            spriteBatch.Draw(texture, rect, startColor);
        }

        Random rd = new Random();
        Point GetCastlePoint
        {
            get
            {

                var pt = new Point(rd.Next(5, 8), rd.Next(5, 8));
                if (island.Cells[pt.X, pt.Y].Equals(TileType.Empty))
                    pt = GetCastlePoint;
                return pt;
            }
        }

    }
}
