using Empty.Effects;
using Empty.GameObjects;
using Empty.GameObjects.Humans;
using Empty.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoFlash.Engine;
using System.Collections.Generic;
using System.Linq;
using Empty.Building;
using IDrawable = MonoFlash.Engine.IDrawable;

namespace Empty
{
	internal class Main : Sprite
	{
		private          UI.Building.Interface buildingInterface;
		private readonly CameraNew             camera;
		private readonly GraphicsDevice        gd;
		private readonly Island                island;
		public static    Main                  instance;
		private          CloudCanvas           cloudCanvas;
		private          bool                  wasPressed;


		private Vector2 node;

		/// <inheritdoc />
		public Main(GraphicsDevice gd)
		{
			instance    = this;
			this.gd     = gd;
			island      = new Island(25, 25);
			cloudCanvas = new CloudCanvas();
			AddChild(new UI.Property());
			buildingInterface = new UI.Building.Interface(BuyStructure);
			AddChild(buildingInterface);
			camera = new CameraNew(gd.Viewport) { Zoom = 2f, Position = Vector2.UnitY * 350 + Vector2.UnitX * 600 };
		}

        public Vector2 MousePosition;
		public override void Update(float delta)
		{
			camera.UpdateCamera(gd.Viewport);

			Vector2 mouseTilePos =
				(Mouse.GetState().Position.ToVector2() -
				 new Point(Values.SCREEN_WIDTH / 2, Values.SCREEN_HEIGHT / 2).ToVector2() +
				 camera.Position *
				 camera.Zoom) /
				Values.TILE_SIZE /
				camera.Zoom;

			island.Posing(mouseTilePos.ToPoint().ToVector2());
            MousePosition = mouseTilePos;

			if (Mouse.GetState().LeftButton == ButtonState.Pressed)
			{
				if (island.GetCellByPose(mouseTilePos) == TileType.Empty)
				{
					island.re = Color.Red;
				}

				if (island.GetCellByPose(mouseTilePos) != TileType.Empty)
				{
					island.re = Color.Blue;
				}

				if (!wasPressed)
				{
					island.OnClick(mouseTilePos.ToPoint());
				}
			}
			else
			{
				island.re = Color.White;
			}

			cloudCanvas.Update(delta);
			island.Update(delta);
			base.Update(delta);

			wasPressed = Mouse.GetState().LeftButton == ButtonState.Pressed;
		}

		private void BuyStructure(UI.Building.Interface.BuildType bt)
		{
            var structure = StructureFabric.GetStructure(bt);

			Resources.Stone  -= structure.StoneCost;
			Resources.Timber -= structure.TimberCost;
			Resources.Iron   -= structure.IronCost;

            island.CallBuilding(structure);

			UI.Property.mainProperty.UpdateMainProperties();
			UI.Building.Interface.UpdateInterface();
		}

		/// <inheritdoc />
		public override void Draw(SpriteBatch sb, GameTime gameTime = null)
		{
			sb.Begin(samplerState: SamplerState.PointClamp); //Clouds
			cloudCanvas.Draw(sb, gameTime);
			//island.Draw(sb);
			sb.End();

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