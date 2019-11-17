using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Empty.GameObjects;

namespace Empty.Building
{
    public static class BuildProcessing
    {
        private static Structure curStructure;
        private static Island island;

        public static void CallBuilding(this Island island, Structure structure)
        {
            Game1.UpdateEvent += Bulding;
            island.Structures.Add(structure);
            BuildProcessing.island = island;
            curStructure = structure;

        }

        public static void Bulding()
        {
            if (curStructure == null) return;
            var node = Main.instance.MousePosition * 16f;
            curStructure.position = (node / 16f).ToPoint().ToVector2() * 16;
            if (curStructure.IsCanPut(island))
            {
                curStructure.StateColor = Color.Green;
                if (Mouse.GetState().LeftButton.Equals(ButtonState.Pressed))
                    SetBuild();

            }
            else curStructure.StateColor = Color.Red;
        }


        public static void SetBuild()
        {
            var vector = curStructure.position;
            Game1.UpdateEvent -= Bulding;
            curStructure.OnAddOnGrid(ref island.Cells,(int)vector.X,(int)vector.Y);
            curStructure.StateColor = Color.White;
            curStructure = null;
        }

    }
}
