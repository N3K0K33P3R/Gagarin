using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoFlash.Engine;

namespace Empty.GameObjects
{
    public class Island : Sprite
    {
        public int Offset;
        public const int IslandSize = 15;
        private IslandGenerator islandGenerator;
        private TileType[,] cells;

        private int wight, height;
        /// <inheritdoc />
        public Island(int w = IslandSize, int h = IslandSize)
        {
            wight = w;
            height = h;
            islandGenerator = new IslandGenerator(wight, height);
            cells = islandGenerator.island;
        }
        public override void Draw(SpriteBatch sb, GameTime gameTime = null)
        {

            for (int i = 0; i < wight; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    var p = pos(i, j, Offset);
                    if (cells[i, j].Equals(TileType.Grass))
                        sb.Draw(Assets.textures["Grass"], pos(i, j, Offset), null, Color.White);
                    if (cells[i, j].Equals(TileType.Sand))
                        sb.Draw(Assets.textures["Sand"], pos(i, j, Offset), null, Color.White);
                    if (cells[i, j].Equals(TileType.Stone))
                        sb.Draw(Assets.textures["Stone"], pos(i, j, Offset), null, Color.White);
                }
            }

            base.Draw(sb, gameTime);
        }

        public TileType[,] GetMap() => cells;


        private Vector2 pos(int i, int j, int offset = 0) =>
            //Vector2.UnitX * 16 * (i-wight/4) + Vector2.UnitY * 16 * (j-height/4) - Vector2.UnitX * offset;
            Vector2.UnitX * i * Values.TILE_SIZE + Vector2.UnitY * j * Values.TILE_SIZE;
    }


}