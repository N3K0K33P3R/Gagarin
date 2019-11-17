using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Empty.Building
{
    public static class StructureFabric
    {

        public static Structure GetStoneWall => new Wall(Assets.textures["Wall"], 25, 10, 3, 0, 10);


    }
}
