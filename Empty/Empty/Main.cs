using Empty.Building;
using Empty.Effects;
using Empty.GameObjects;
using Empty.GameObjects.Humans;
using Empty.Helpers;
using Empty.UI;
using Empty.UI.Building;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoFlash.Engine;
using System.Collections.Generic;

namespace Empty
{
	internal class Main : Sprite
	{
		private readonly CameraNew       camera;
		private readonly GraphicsDevice  gd;
		private readonly OurIsland       island;
		private readonly Interface       buildingInterface;
		private readonly TimerUI         timerUI;
		private readonly CloudCanvas     cloudCanvas;
		private readonly List<Ball>      balls;
		private readonly List<Explosion> explosions;
		private          InfoPanel       infoPanel;
		private          EnemyIsland     enemyIsland;
		private          bool            wasPressed;
		private          double          timer;
		private          double          timerSpeed;


		private       Vector2 node;
		public static Main    instance;

		public Vector2 MousePosition;

		/// <inheritdoc />
		public Main(GraphicsDevice gd)
		{
			instance    = this;
			this.gd     = gd;
			balls       = new List<Ball>();
			explosions  = new List<Explosion>();
			island      = new OurIsland(ShowInfo, RemoveInfo);
			cloudCanvas = new CloudCanvas();
			AddChild(new Property());
			buildingInterface = new Interface(BuyStructure);
			AddChild(buildingInterface);
			camera = new CameraNew(gd.Viewport) { Zoom = 2f, Position = (Vector2.UnitY * 12 + Vector2.UnitX * 12) * Values.TILE_SIZE };

			timerUI = new TimerUI();
			AddChild(timerUI);

			SetTimer();
		}

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
				}
			}
			else
			{
				island.re = Color.White;
			}

			cloudCanvas.Update(delta);
			island.Update(delta);
			enemyIsland?.Update(delta);

			for (int i = balls.Count - 1; i >= 0; i--)
			{
				if (balls[i].ShouldDelete)
				{
					Explosion explosion = new Explosion(Assets.textures["Explosion"], balls[i].x + Values.TILE_SIZE / 2, balls[i].y + Values.TILE_SIZE / 2);
					explosions.Add(explosion);
					balls.Remove(balls[i]);
				}
				else
				{
					balls[i].Update(delta);
				}
			}

			for (int i = explosions.Count - 1; i >= 0; i--)
			{
				if (explosions[i].ShouldDelete)
				{
					explosions.Remove(explosions[i]);
				}
				else
				{
					explosions[i].Update(delta);
				}
			}

			if (enemyIsland?.y > 570)
			{
				enemyIsland = null;
				Trace("Killed");
				SetTimer();
			}

			base.Update(delta);

			wasPressed = Mouse.GetState().LeftButton == ButtonState.Pressed;
		}

		/// <inheritdoc />
		public override void Draw(SpriteBatch sb, GameTime gameTime = null)
		{
			sb.Begin(samplerState: SamplerState.PointClamp); //Clouds
			cloudCanvas.Draw(sb, gameTime);
			//island.Draw(sb);
			sb.End();

			sb.Begin(samplerState: SamplerState.PointClamp, transformMatrix: camera.Transform); //Islands

			island.DrawIsland(sb);
			enemyIsland?.DrawIsland(sb);
			island.Draw(sb);
			enemyIsland?.Draw(sb);

			balls.ForEach(b => b.Draw(sb));
			explosions.ForEach(b => b.Draw(sb));

			sb.End();

			sb.Begin(samplerState: SamplerState.PointClamp); //UI
			base.Draw(sb, gameTime);
			//island.Draw(sb);
			sb.End();
		}

		public TileType[,] GetMap() => island.GetMap();

		public void Shot(Cannon cannon, Point target)
		{
			var angle = Ball.GetAngle(cannon.position / 16, target.ToVector2());

			if (angle > 0.5 || angle < -0.5)
			{
				return;
			}
			Ball ball = new Ball((cannon.position / 16).ToPoint(), target, island, enemyIsland, cannon);
			balls.Add(ball);
		}

		public void KillEnemyIsland()
		{
			enemyIsland.Kill();
		}

		private void SetTimer()
		{
			timer      = 1;
			timerSpeed = Values.RANDOM.NextDouble(0.001, 0.01);
		}

		private void ShowInfo(BaseHuman human)
		{
			infoPanel = new InfoPanel(human);
			AddChild(infoPanel);
		}

		private void RemoveInfo()
		{
			RemoveChild(infoPanel);
			infoPanel = null;
		}

		private void BuyStructure(Interface.BuildType bt)
		{
			Structure structure = StructureFabric.GetStructure(bt);

			Resources.Stone  -= structure.StoneCost;
			Resources.Timber -= structure.TimberCost;
			Resources.Iron   -= structure.IronCost;

			island.CallBuilding(structure);

			Property.mainProperty.UpdateMainProperties();
			Interface.UpdateInterface();
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