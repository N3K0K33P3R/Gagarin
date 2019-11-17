using Empty.GameObjects;
using Empty.GameObjects.Humans;
using Empty.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoFlash.Engine;
using System.Collections.Generic;
using IDrawable = MonoFlash.Engine.IDrawable;

namespace Empty
{
    internal class Main : Sprite
    {
        private readonly CameraNew camera;
        private readonly GraphicsDevice gd;
        private readonly Island island;
        public static Main instance;


        private Vector2 node;

        /// <inheritdoc />
        public Main(GraphicsDevice gd)
        {
            instance = this;

            this.gd = gd;
            island = new Island(25, 25);
            AddChild(new Property());
            camera = new CameraNew(gd.Viewport) { Zoom = 1f, Position = Vector2.UnitY * 350 +Vector2.UnitX*600};

            var bh = new BaseHuman(Assets.textures["Human"], 0, 0);
            bh.SetTilePos(4, 5);
            AddChild(bh);
        }

        public override void Update(float delta)
        {
            Vector2 mouse = Mouse.GetState().Position.ToVector2();
            mouse -= camera.Bounds.Size.ToVector2() / (2f) - camera.Position;

            node = (mouse / (16f * Values.MapScale)).ToPoint().ToVector2() * 16* Values.MapScale;
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
            }
            else
            {
                island.re = Color.White;
            }

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