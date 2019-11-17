using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

namespace Empty.Building
{
    
    abstract class Structure
    {
        public int StoneCost = 0;
        public int TimberCost = 0;
        public int IronCost = 0;

        public int WorkCost;
    }
}
