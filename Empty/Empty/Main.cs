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
		private readonly Camera         camera;
		private readonly GraphicsDevice gd;
		private readonly Island         island;
		public static    Main           instance;


		private Vector2 node;

		/// <inheritdoc />
		public Main(GraphicsDevice gd)
		{
			this.gd = gd;
			island  = new Island(25, 25);
			AddChild(new Property());
			camera = new Camera { Zoom = 2.3f };

			var bh = new BaseHuman(Assets.textures["Human"], 0, 0);
			bh.SetTilePos(4, 5);
			AddChild(bh);
		}

		public override void Update(float delta)
		{
			Vector2 mouse = Mouse.GetState().Position.ToVector2();
			node = (mouse / 16f).ToPoint().ToVector2() * 16;
			island.Posing(node);

			if (Mouse.GetState().LeftButton == ButtonState.Pressed)
			{
				if (island.GetCellByMouse == TileType.Empty)
				{
					island.re = Color.Red;
				}

				if (island.GetCellByMouse != TileType.Empty)
				{
					island.re = Color.Blue;
				}
			}

			base.Update(delta);
		}

		/// <inheritdoc />
		public override void Draw(SpriteBatch sb, GameTime gameTime = null)
		{
			sb.Begin(samplerState: SamplerState.PointClamp); //UI
			base.Draw(sb, gameTime);
			island.Draw(sb);
			sb.End();
		}

		public TileType[,] GetMap() => island.GetMap();
	}
}