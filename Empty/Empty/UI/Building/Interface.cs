using Microsoft.Xna.Framework.Graphics;
using MonoFlash.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Empty.UI.Building
{
    public class Interface : Sprite
    {
        public static List<BuildingCell> cells;
        public enum BuildType { Cannon, Wall, Bridge };

        public Interface(Action<BuildType> build)
        {
            cells = new List<BuildingCell>();
            AddCell(Game1.stoneTexture, BuildType.Cannon, build);
            AddCell(Game1.woodTexture, BuildType.Wall, build);
            AddCell(Game1.ironTexture, BuildType.Bridge, build);
            DrawCells();
        }



        public void AddCell(Texture2D icon, BuildType buildtype, Action<BuildType> build)
        {

            var str = Empty.Building.StructureFabric.GetStructure(buildtype);

            cells.Add(new BuildingCell(icon, str.StoneCost, str.TimberCost, str.IronCost, buildtype, build));
        }

        private void DrawCells()
        {
            for (int i = 0; i < cells.Count; i++)
            {
                cells[i].x = (int)Values.SCREEN_WIDTH / 2 - (cells.Count * cells[i].quadWidth / 2) + (i * cells[i].quadWidth);
                cells[i].y = (int)Values.SCREEN_HEIGHT - cells[i].quadWidth;
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
