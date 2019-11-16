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
    class Inventory : Sprite
    {
        private Items.Generator equippedGenerator;
        private Items.Weapon equippedWeapon;
        private Items.Engine equippedEngine;
        private Items.Bonus equippedBonus;
        public static List<Items.Item> items;
        private Cell[,] itemCells;

        public Inventory()
        {
            items = new List<Items.Item>();
            items.Add(new Items.Generator() { equipped = true });
            items.Add(new Items.Weapon() { equipped = true });
            items.Add(new Items.Engine());
            items.Add(new Items.Bonus());

            itemCells = Get2DimArrayOfItems();
            for(int i = 0; i < itemCells.GetLength(0); i++)
            {
                for (int j = 0; j < itemCells.GetLength(1); j++)
                {
                    if (itemCells[i, j] != null && itemCells[i,j].item != null) Console.Write(itemCells[i, j].item.GetType().ToString() + " ");
                    else Console.Write("null ");
                }
                Console.WriteLine();
            }
        }

        public override void Draw(SpriteBatch sb, GameTime gameTime = null)
        {
            float width = (Cell.cellWidth * itemCells.GetLength(1)) + (Cell.cellWidth * itemCells.GetLength(1)) / 2 + Cell.cellWidth / 2;
            float height = (Cell.cellWidth * itemCells.GetLength(0)) + (Cell.cellWidth * itemCells.GetLength(0)) / 2 + Cell.cellWidth / 2;
            float startX = (float)globalX; // Поставить правильно, когда будет известен размер экрана
            float startY = (float)globalY; // И это тоже
            sb.FillRectangle(new Vector2(startX, startY), 
                new Vector2(width, height), 
                Color.Orange, 0);
            base.Draw(sb, gameTime);
        }

        private Cell[,] Get2DimArrayOfItems()
        {
            Cell[,] array = new Cell[4, 6];
            array[0, 0] = new Cell(0, 0, Cell.TypeCell.Generator);
            array[1, 0] = new Cell(1, 0, Cell.TypeCell.Weapon);
            array[2, 0] = new Cell(2, 0, Cell.TypeCell.Engine);
            array[3, 0] = new Cell(3, 0, Cell.TypeCell.Bonus);
            foreach (Items.Item item in items)
            {
                //Console.WriteLine(item.GetType().ToString());
                if (item.equipped)
                {
                    switch (item.GetType().ToString())
                    {
                        case "Empty.Items.Generator":
                            array[0, 0].item = item;
                            break;

                        case "Empty.Items.Weapon":
                            array[1, 0].item = item;
                            break;

                        case "Empty.Items.Engine":
                            array[2, 0].item = item;
                            break;

                        case "Empty.Items.Bonus":
                            array[3, 0].item = item;
                            break;

                        default:
                            Console.WriteLine(item.GetType().ToString()); // Ты поехавший?
                            break;
                    }
                }
                else
                {
                    for (int i = 0; i < array.GetLength(0); i++)
                    {
                        for (int j = 2; j < array.GetLength(1); j++) // Первая колонка зарезервирована для надетых вещей, вторая - для разделения.
                        {
                            if (array[i, j] == null)
                            {
                                array[i, j] = new Cell(i, j, Cell.TypeCell.Inventory, item);
                                i = array.GetLength(0);
                                j = array.GetLength(1);
                            }
                        }
                    }
                }
            }
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++) 
                {
                    if (array[i, j] != null)
                    {
                        AddChild(array[i, j]);
                    }
                }
            }
            return array;
        }

        private Cell[,] SetEmptyCells(Cell[,] array)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 1; j < array.GetLength(1); j++) // Первая колонка зарезервирована для надетых вещей и никогда не может быть не заполнена.
                {
                    if (array[i, j] == null)
                    {
                        array[i, j] = new Cell(i, j);
                        AddChild(array[i, j]);
                    }
                }
            }
            return array;
        }
    }
}
