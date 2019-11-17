using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Empty.Building
{
    class Wall : Structure
    {


        public Wall(Texture2D texture, int stoneCost, int timberCost, int ironCost, int workCost, Vector2 offset = default) : base(texture, stoneCost, timberCost, ironCost, workCost, offset)
        {
        }

        public override bool IsCanPut(GameObjects.Island island) => island.GetCellByMouse != TileType.Empty;


        public override void OnAddOnGrid(ref TileType[,] types,int i, int j)
        {
           
        }
    }
}
