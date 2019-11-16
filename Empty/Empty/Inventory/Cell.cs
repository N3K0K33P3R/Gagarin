using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoFlash.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Empty.Inventory
{
    class Cell : Sprite
    {
        public Items.Item item;
        public static int cellWidth = 50;
        public enum TypeCell { Generator, Weapon, Engine, Bonus, Inventory }
        public bool drawable = true;
        public TypeCell type;
        private int coordPosX = 0;
        private int coordPosY = 0;

        public Cell(int coordI, int coordJ, TypeCell type, Items.Item item = null)
        {
            this.coordPosX = coordJ;
            this.coordPosY = coordI;
            this.type = type;
            this.item = item;

            if (type != TypeCell.Inventory) DrawBackgroundImage();
        }

        public void DrawBackgroundImage()
        {
            Image image;
            switch (item.GetType().ToString())
            {
                case "Empty.Items.Generator":
                    image = new Image(Game1.generator);
                    break;
                case "Empty.Items.Weapon":
                    image = new Image(Game1.weapon);
                    break;
                case "Empty.Items.Engine":
                    image = new Image(Game1.engine);
                    break;
                case "Empty.Items.Bonus":
                    image = new Image(Game1.bonus);
                    break;
                default:
                    return;
                    break;
            }
            image.ScaleX = cellWidth / image.width;
            image.ScaleY = cellWidth / image.height;
            image.x = x + cellWidth / 2 + (coordPosX * (cellWidth + (cellWidth / 2))) + cellWidth / 2;
            image.y = y + cellWidth / 2 + (coordPosY * (cellWidth + (cellWidth / 2))) + cellWidth / 2;
            AddChild(image);
        }

        public override void Draw(SpriteBatch sb, GameTime gameTime = null)
        {
            if (drawable)
            {
                sb.FillRectangle(new Vector2((float)(x + cellWidth / 2 + (coordPosX * (cellWidth + (cellWidth / 2)))), (float)(y + cellWidth / 2 + (coordPosY * (cellWidth + (cellWidth / 2))))),
                new Vector2(cellWidth, cellWidth), Color.LightGray, 0);

                base.Draw(sb, gameTime);
            }      
            
            
        }
    }
}
