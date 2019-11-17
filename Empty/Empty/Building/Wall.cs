using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace Empty.Building
{
    class Wall:Structure
    {
        int Healty;



        public Wall(Texture2D texture, int healty, int stoneCost, int timberCost, int ironCost, int workCost) : base(texture, stoneCost, timberCost, ironCost, workCost)
        {
            this.Healty = healty;
        }
    }
}
