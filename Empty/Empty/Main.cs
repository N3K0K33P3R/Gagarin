using Empty.GameObjects;
using Empty.GameObjects.Humans;
using Empty.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoFlash.Engine;
using System.Collections.Generic;
using IDrawable = MonoFlash.Engine.IDrawable;

namespace Empty
{
	internal class Main : Sprite
	{
		private readonly Camera          camera;
		private readonly Island          island;
		private readonly GraphicsDevice  gd;
		private readonly List<IDrawable> mapChilds;
		public static    Main            instance;


		/// <inheritdoc />
		public Main(GraphicsDevice gd)
		{
			instance  = this;
			mapChilds = new List<IDrawable>();
			this.gd   = gd;
			island    = new Island();
			AddChild(new Property());
			camera = new Camera { Zoom = 3f };

			var bh = new BaseHuman(Assets.textures["Human"], 0, 0);
			bh.SetTilePos(4, 5);
			mapChilds.Add(bh);
		}

		public override void Update(float delta)
		{
			camera.UpdateCamera();
			
			base.Update(delta);

			foreach (IDrawable drawable in mapChilds)
			{
				drawable.Update(delta);
			}
		}

		/// <inheritdoc />
		public override void Draw(SpriteBatch sb, GameTime gameTime = null)
		{
			sb.Begin(samplerState: SamplerState.PointClamp); //UI
			base.Draw(sb, gameTime);
			sb.End();

			sb.Begin(samplerState: SamplerState.PointClamp, transformMatrix: camera.get_transformation(gd)); //Map
			island.Draw(sb);

			foreach (IDrawable drawable in mapChilds)
			{
				drawable.Draw(sb, null);
			}

			sb.End();
		}

		public TileType[,] GetMap() => island.GetMap();
	}
}