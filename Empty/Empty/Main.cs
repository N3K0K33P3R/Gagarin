using Empty.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoFlash.Engine;
using System;

namespace Empty
{
    internal class Main : Sprite
    {
        private readonly Camera camera;
        private Island island;
        private readonly GraphicsDevice gd;
        /// <inheritdoc />
        public Main(GraphicsDevice gd)
        {
            this.gd = gd;
            island = new Island(25, 25);
            AddChild(new UI.Property());
            camera = new Camera() { Zoom = 2.3f ,Pos =Vector2.UnitY*130};
        }


        Vector2 node;

        public override void Update(float delta)
        {
            camera.UpdateCamera();

            var mouse = Mouse.GetState().Position.ToVector2();
            node = ((mouse / 16f).ToPoint()).ToVector2() * 16;
            island.Posing(node);

            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                if (island.GetCellByPose(Mouse.GetState().Position.ToVector2()) == TileType.Empty) island.re = Color.Red;
                if (island.GetCellByPose(Mouse.GetState().Position.ToVector2()) != TileType.Empty) island.re = Color.Blue;
            }

            base.Update(delta);
        }

        /// <inheritdoc />
        public override void Draw(SpriteBatch sb, GameTime gameTime = null)
        {
            sb.Begin(samplerState:SamplerState.PointClamp,transformMatrix:camera.get_transformation(gd));//UI
            island.Draw(sb);
            sb.End();

            sb.Begin();//UI
            base.Draw(sb, gameTime);
            island.Draw(sb);
            sb.End();


        }
    }
}