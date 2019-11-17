using Empty.GameObjects;
using Empty.GameObjects.Humans;
using Empty.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoFlash.Engine;
using System.Collections.Generic;
using System.Linq;
using IDrawable = MonoFlash.Engine.IDrawable;

namespace Empty
{
	internal class Main : Sprite
	{
		private readonly CameraNew camera;
		private readonly GraphicsDevice  gd;
		private readonly Island          island;
		public static    Main            instance;
		private          List<BaseHuman> humans;
		private          BaseHuman       selectedHuman;


        private Vector2 node;

		/// <inheritdoc />
		public Main(GraphicsDevice gd)
		{
			instance = this;
			this.gd  = gd;
			island   = new Island(25, 25);
			AddChild(new Property());
			camera = new CameraNew(gd.Viewport) { Zoom = 1f, Position = Vector2.UnitY * 350 + Vector2.UnitX * 600 };


			int humanCount = 5;
			humans = new List<BaseHuman>();

			for (int i = 0; i < island.GetMap().GetLength(0); i++)
			{
				bool shouldBreak = false;

				for (int j = 0; j < island.GetMap().GetLength(1); j++)
				{
					if (island.GetMap()[i, j] == TileType.Empty)
					{
						continue;
					}

					var bh = new BaseHuman(Assets.textures["Human"], i, j);
					island.AddChild(bh);
					humans.Add(bh);
					humanCount--;

					if (humanCount == 0)
					{
						shouldBreak = true;
						break;
					}
				}

				if (shouldBreak)
				{
					break;
				}
			}
		}

        public override void Update(float delta)
        {
            Vector2 mouse = Mouse.GetState().Position.ToVector2();
            mouse -= camera.Bounds.Size.ToVector2() / (2f) - camera.Position;

            node = (mouse / 16f).ToPoint().ToVector2() * 16;
            island.Posing(node);

            camera.UpdateCamera(gd.Viewport);

            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                if (island.GetCellByPose(mouse) == TileType.Empty)
                {
                    island.re = Color.Red;
                }

                if (island.GetCellByPose(mouse) != TileType.Empty)
                {
                    island.re = Color.Blue;
                }
				
				
				if (selectedHuman == null)
				{
					BaseHuman human = humans.FirstOrDefault(h => h.tilePos == (node / 16).ToPoint());
					Trace(humans[0].tilePos, node / 16);
					if (human != null)
					{
						selectedHuman = human;
					}
				}
				else
				{
					(int x1, int x2) = (node / 16).ToPoint();
					selectedHuman.SetTilePos(x1, x2);
					selectedHuman = null;
				}
            }
            else
            {
                island.re = Color.White;
            }

			island.Update(delta);
            base.Update(delta);
        }

        /// <inheritdoc />
        public override void Draw(SpriteBatch sb, GameTime gameTime = null)
        {
            sb.Begin(samplerState: SamplerState.PointClamp, transformMatrix: camera.Transform); //UI
            island.Draw(sb);
            sb.End();

            sb.Begin(samplerState: SamplerState.PointClamp); //UI
            base.Draw(sb, gameTime);
            //island.Draw(sb);
            sb.End();
        }

        public TileType[,] GetMap() => island.GetMap();
    }
}