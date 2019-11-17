using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoFlash.Engine;
using Microsoft.Xna.Framework.Input;

namespace Empty.GameObjects
{
    public class Island : Sprite
    {
        public int Offset;
        public const int IslandSize = 16;
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
        public Vector2 node;
        public override void Draw(SpriteBatch sb, GameTime gameTime = null)
        {

            for (int i = 0; i < wight; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (cells[i, j].Equals(TileType.Grass))
                        sb.Draw(Assets.textures["Grass"], pos(i, j, Offset), null, Color.White);
                    if (cells[i, j].Equals(TileType.Sand))
                        sb.Draw(Assets.textures["Sand"], pos(i, j, Offset), null, Color.White);
                    if (cells[i, j].Equals(TileType.Stone))
                        sb.Draw(Assets.textures["Stone"], pos(i, j, Offset), null, Color.White);
                }
            }
            sb.DrawRectangle(node,Vector2.One*16, re,1);
            base.Draw(sb, gameTime);
        }
        public Color re =Color.White;

        internal TileType GetCellByMouse
        {
            get {
                var vc = node /= 16f;
                if (vc.X>0&&
                    vc.Y>0&&
                    vc.X < wight/16f&&
                    vc.Y < height / 16f)
                {
                    return cells[(int)(vc.X * 16), (int)(vc.Y * 16)];
                }
                return TileType.Empty;
            }
        }

        internal TileType GetCellByPose(Vector2 vector)
        {     
                var vc = vector / 16f;
                if (vc.X > 0 &&
                    vc.Y > 0 &&
                    vc.X < wight &&
                    vc.Y < height)
                {
                    return cells[(int)(vc.X), (int)(vc.Y)];
                }
                return TileType.Empty;       
        }

        public void Posing(Vector2 node)
        {
            this.node = node;
        }
     

        private Vector2 pos(int i, int j, int offset = 0) =>
            Vector2.UnitX * 16 * (i) + Vector2.UnitY * 16 * (j) - Vector2.UnitX * offset;
     
    
    }


}