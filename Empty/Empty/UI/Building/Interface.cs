using Microsoft.Xna.Framework.Graphics;
using MonoFlash.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Empty.UI.Building
{
    class Interface : Sprite
    {
        public static List<BuildingCell> cells;
        public enum BuildType { Cannon, Wall, Bridge };

        public Interface(Action <BuildType> build)
        {
            cells = new List<BuildingCell>();
            AddCell(Game1.stoneTexture, 1, 0, 1, BuildType.Cannon, build);
            AddCell(Game1.woodTexture, 0, 0, 1, BuildType.Wall, build);
            AddCell(Game1.ironTexture, 1, 10, 100, BuildType.Bridge, build);
            DrawCells();
        }

        

        public void AddCell(Texture2D icon, int stoneCost, int timberCost, int ironCost, BuildType buildtype, Action<BuildType> build)
        {
            cells.Add(new BuildingCell(icon, stoneCost, timberCost, ironCost, buildtype, build));
        }

        private void DrawCells()
        {
            for (int i = 0; i < cells.Count; i++)
            {
                cells[i].x = (int)Values.SCREEN.WIDTH / 2 - (cells.Count * cells[i].quadWidth / 2) + (i * cells[i].quadWidth);
                cells[i].y = (int)Values.SCREEN.HEIGHT - cells[i].quadWidth;
                AddChild(cells[i]);
            }
        }

        public static void UpdateInterface()
        {
            Property.mainProperty.UpdateMainProperties();
            foreach (BuildingCell cell in cells)
            {
                cell.costs.RemoveChildren();
                cell.costs.props.Clear();
                cell.DrawCosts();
            }
        }
    }
}
