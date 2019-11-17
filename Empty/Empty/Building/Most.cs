using Empty.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Empty.Building
{
    internal class Most : Structure
    {
        public Most(Texture2D texture, int stoneCost, int timberCost, int ironCost, int workCost, Vector2 offset = default) : base(
            texture,
            stoneCost,
            timberCost,
            ironCost,
            workCost,
            offset)
        { }

        public override bool IsCanPut(GameObjects.Island island)
        {
            var vector = position / 16f;
            return
            island.GetCellByMouse == TileType.Empty &&
            vector.X > 0 &&
            vector.Y > 0 &&
            vector.X < 25 &&
            vector.Y < 25;
        }



        public override void OnAddOnGrid(ref TileType[,] types, int i, int j)
        {
            i /= 16;
            j /= 16;
            if (
               i > 0 &&
               j > 0 &&
               i < 16 &&
               j < 16)
            {
                System.Console.WriteLine($"{i}:====:{j}");
                types[i, j] = TileType.Grass;
            }
        }

    }
}