using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoFlash.Engine;


namespace Empty.GameObjects
{
    public class Island : Sprite
    {

        

        public const int IslandSize = 16;
        private readonly IslandGenerator islandGenerator;
        private readonly TileType[,] cells;

        private readonly int wight;
        private readonly int height;
        public int Offset;
        public Vector2 node;
        public Color re = Color.White;

        internal TileType GetCellByMouse
        {
            get
            {
                Vector2 vc = node /= 16 * Values.MAP_SCALE;

                if (vc.X > 0 &&
                    vc.Y > 0 &&
                    vc.X < wight / 16f * Values.MAP_SCALE &&
                    vc.Y < height / 16f * Values.MAP_SCALE)
                {
                    return cells[(int)(vc.X * 16*Values.MAP_SCALE), (int)(vc.Y * 16* Values.MAP_SCALE)];
                }

                return TileType.Empty;
            }
        }

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
            for (var i = 0; i < wight; i++)
            {
                for (var j = 0; j < height; j++)
                {
                    if (cells[i, j].Equals(TileType.Grass))
                    {
                        sb.Draw(Assets.textures["Grass"], pos(i, j, Offset), null, color: Color.White, scale: Vector2.One * Values.MAP_SCALE);
                    }

                    if (cells[i, j].Equals(TileType.Sand))
                    {
                        sb.Draw(Assets.textures["Sand"], pos(i, j, Offset), null, color: Color.White, scale: Vector2.One * Values.MAP_SCALE);
                    }

                    if (cells[i, j].Equals(TileType.Stone))
                    {
                        sb.Draw(Assets.textures["Stone"], pos(i, j, Offset), null, color: Color.White, scale: Vector2.One * Values.MAP_SCALE);
                    }
                }
            }

            sb.DrawRectangle(node, Vector2.One * 16 * Values.MAP_SCALE, re, Values.MAP_SCALE, 1);
            base.Draw(sb, gameTime);
        }

        public void Posing(Vector2 node)
        {
            this.node = node;
        }

        public TileType[,] GetMap() => cells;


        internal TileType GetCellByPose(Vector2 vector)
        {
            Vector2 vc = vector /= 16f * Values.MAP_SCALE;

            if (vc.X > 0 &&
                vc.Y > 0 &&
                vc.X < wight &&
                vc.Y < height)
            {
                return cells[(int)(vc.X), (int)(vc.Y)];
            }

            return TileType.Empty;
        }


        private Vector2 pos(int i, int j, int offset = 0) => Vector2.UnitX * 16 * i * Values.MAP_SCALE + Vector2.UnitY * 16 * j * Values.MAP_SCALE - Vector2.UnitX * offset;
    }
}