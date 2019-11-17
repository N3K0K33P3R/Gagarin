using Empty.Effects;
using Empty.GameObjects;
using Empty.Helpers;
using Empty.UI;
using Empty.UI.Building;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoFlash.Engine;
using System.Collections.Generic;
using System.Linq;
using Empty.Building;
using IDrawable = MonoFlash.Engine.IDrawable;
using System;

namespace Empty
{
	internal class Main : Sprite
	{
		private readonly CameraNew      camera;
		private readonly GraphicsDevice gd;
		private readonly OurIsland      island;
		private readonly Interface      buildingInterface;
		private readonly TimerUI        timerUI;
		private          EnemyIsland    enemyIsland;
		private readonly CloudCanvas    cloudCanvas;
		private          bool           wasPressed;
		private          double         timer;
		private          double         timerSpeed;


		private       Vector2 node;
		public static Main    instance;

		/// <inheritdoc />
		public Main(GraphicsDevice gd)
		{
			instance    = this;
			this.gd     = gd;
			island      = new OurIsland();
			cloudCanvas = new CloudCanvas();
			AddChild(new UI.Property());
			buildingInterface = new UI.Building.Interface(BuyStructure);
			AddChild(buildingInterface);
			camera = new CameraNew(gd.Viewport) { Zoom = 2f, Position = (Vector2.UnitY * 12 + Vector2.UnitX * 12) * Values.TILE_SIZE };

			timerUI = new TimerUI();
			AddChild(timerUI);

			SetTimer();
		}

		private void SetTimer()
		{
			timer      = 1;
			timerSpeed = Values.RANDOM.NextDouble(0.001, 0.01);
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

			if (enemyIsland == null && timer > 0)
			{
				timer -= timerSpeed;
				timerUI.SetTimer((float)timer);
			}
			else if (timer < 0)
			{
				enemyIsland = new EnemyIsland { x = 30 * Values.TILE_SIZE };
				SetTimer();
			}

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

					if (enemyIsland == null) { }
					else
					{
						KillEnemyIsland();
					}
				}
			}
			else
			{
				island.re = Color.White;
			}

			cloudCanvas.Update(delta);
			island.Update(delta);
			enemyIsland?.Update(delta);

			if (enemyIsland?.y > 570)
			{
				enemyIsland = null;
				Trace("Killed");
				SetTimer();
			}

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
			island.DrawIsland(sb);
			enemyIsland?.DrawIsland(sb);
			island.Draw(sb);
			enemyIsland?.Draw(sb);
			sb.End();

			sb.Begin(samplerState: SamplerState.PointClamp); //UI
			base.Draw(sb, gameTime);
			//island.Draw(sb);
			sb.End();
		}

		public TileType[,] GetMap() => island.GetMap();

		public void KillEnemyIsland()
		{
			enemyIsland.Kill();
		}

		private void TestAction(Interface.BuildType bt)
		{
			Resources.Stone  -= 1;
			Resources.Timber -= 1;
			Resources.Iron   -= 1;
			Property.mainProperty.UpdateMainProperties();
			Interface.UpdateInterface();
		}
	}
}