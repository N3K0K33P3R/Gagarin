using Microsoft.Xna.Framework;

namespace Empty.Building
{
    public static class StructureFabric
    {

        public static Structure GetStoneWall => new Wall(Assets.textures["Wall"], 10, 3, 0, 10,new Vector2(0,-8));

        public static Structure GetWoodMost => new Most(Assets.textures["Most"], 0, 7, 1, 10);

        public static Structure GetCannon => new Wall(Assets.textures["Gun"],  0, 4, 10, 10);


        public static Structure GetStructure(UI.Building.Interface.BuildType bt)
        {
            switch (bt)
            {
                case UI.Building.Interface.BuildType.Wall:
                    return GetStoneWall;
                case UI.Building.Interface.BuildType.Cannon:
                    return GetCannon;
                case UI.Building.Interface.BuildType.Bridge:
                    return GetWoodMost;
            }
            return null;
        }
    }
}
