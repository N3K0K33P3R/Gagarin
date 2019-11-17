using Empty.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoFlash.Engine;

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
			island = new Island(20,20);
			AddChild(new UI.Property());
            camera = new Camera() { Zoom=3f};
        }

        public override void Update(float delta)
        {

            camera.UpdateCamera();
            base.Update(delta);
        }

        /// <inheritdoc />
        public override void Draw(SpriteBatch sb, GameTime gameTime = null)
		{

            sb.Begin();//UI
            base.Draw(sb, gameTime);
            sb.End();

            sb.Begin(samplerState:SamplerState.PointClamp,transformMatrix:camera.get_transformation(gd));//Map
            island.Draw(sb);
			sb.End();
		}
	}
}