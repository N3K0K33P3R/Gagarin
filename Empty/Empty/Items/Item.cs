using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Empty.Items
{
    class Item
    {
        public double weight;
        public double energy;
        public bool equipped = false;

        public Item(double weight = 0, double energy = 0)
        {
            this.weight = weight;
            this.energy = energy;
        }
    }
}
