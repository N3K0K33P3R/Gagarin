using Empty.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Empty.GameObjects;
using Empty.UI.Building;
using Empty.UI;
using MonoFlash.Engine;

namespace Empty.Building
{
    public static class BuildProcessing
    {
        private static Structure curStructure;
        private static Island island;
        public static bool lockFlag = false;

        public static void CallBuilding(this Island island, Structure structure)
        {
            Clean();
            Game1.UpdateEvent += Bulding;
            island.Structures.Add(structure);
            BuildProcessing.island = island;
            curStructure = structure;
        }

		public static void Bulding()
		{
			if (curStructure == null)
			{
				return;
			}

			Vector2 node = Main.instance.MousePosition * 16f;
			curStructure.position = (node / 16f).ToPoint().ToVector2() * 16;

			if (curStructure.IsCanPut(island))
			{
				curStructure.StateColor = Color.Green;

				if (Mouse.GetState().LeftButton.Equals(ButtonState.Pressed)&&Mouse.GetState().Y<600)
				{
					SetBuild(curStructure);
				}
                if (Mouse.GetState().RightButton.Equals(ButtonState.Pressed))
                {
                    Clean();
                }
            }
			else
			{
				curStructure.StateColor = Color.Red;
			}
		}

        public static void Clean()
        {
            if (curStructure != null)
            {
                Game1.UpdateEvent -= Bulding;

                Resources.Stone += curStructure.StoneCost;
                Resources.Timber += curStructure.TimberCost;
                Resources.Iron += curStructure.IronCost;

                Property.mainProperty.UpdateMainProperties();
                Interface.UpdateInterface();

                if(curStructure.StateColor!=Color.White)
                island.Structures.Remove(curStructure);
            }
            curStructure = null;
        }

		public static void SetBuild(Structure curStructure)
		{
			Vector2 vector = curStructure.position;
			Game1.UpdateEvent -= Bulding;
			curStructure.OnAddOnGrid(ref island.Cells, (int)vector.X, (int)vector.Y);
			curStructure.StateColor = Color.White;
			curStructure            = null;
		}
	}
}