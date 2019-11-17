﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoFlash.Engine;
using System;

namespace Empty.UI.Building
{
	public class BuildingCell : Sprite
	{
		private readonly int                         borderThickness = 5;
		private readonly Image                       icon;
		private readonly Action<Interface.BuildType> build;

		private ButtonState wasPressed = ButtonState.Released;
		public  int         quadWidth  = 100;
		public  Quad        background;
		public  int         costStone;
		public  int         costTimber;
		public  int         costIron;

		public Property            costs;
		public Interface.BuildType buildType;

		public BuildingCell(Texture2D icon, int stoneCost, int timberCost, int ironCost, Interface.BuildType type, Action<Interface.BuildType> build)
		{
			costStone  = stoneCost;
			costTimber = timberCost;
			costIron   = ironCost;
			this.icon  = new Image(icon);
			this.build = build;
			buildType  = type;
			DrawCell();
		}

		public override void Update(float delta)
		{
			if (Resources.Stone >= costStone &&
				Resources.Timber >= costTimber &&
				Resources.Iron >= costIron)
			{
				if (Mouse.GetState().X > globalX &&
					Mouse.GetState().X < globalX + quadWidth &&
					Mouse.GetState().Y > globalY &&
					Mouse.GetState().Y < globalY + quadWidth)
				{
					icon.Alpha = 1;

					if (Mouse.GetState().LeftButton == ButtonState.Pressed && wasPressed != ButtonState.Pressed)
					{
						LeftButtonPressed();
					}
				}
				else
				{
					icon.Alpha = 0.15;
				}
			}
			else
			{
				icon.Alpha = 0.15;
			}

			wasPressed = Mouse.GetState().LeftButton;
			base.Update(delta);
		}


		public override void Draw(SpriteBatch sb, GameTime gameTime = null)
		{
			sb.DrawRectangle(new Vector2((float)x, (float)y), new Vector2((float)background.width, (float)background.height), Color.Black, borderThickness, 0);
			base.Draw(sb, gameTime);
		}

		public void DrawCosts()
		{
			costs = new Property(false);

			if (costStone > 0)
			{
				if (costStone <= Resources.Stone)
				{
					costs.SetProperty(Game1.stoneTexture, costStone.ToString(), Color.Green);
				}
				else
				{
					costs.SetProperty(Game1.stoneTexture, costStone.ToString(), Color.Red);
				}
			}

			if (costTimber > 0)
			{
				if (costTimber <= Resources.Timber)
				{
					costs.SetProperty(Game1.woodTexture, costTimber.ToString(), Color.Green);
				}
				else
				{
					costs.SetProperty(Game1.woodTexture, costTimber.ToString(), Color.Red);
				}
			}

			if (costIron > 0)
			{
				if (costIron <= Resources.Iron)
				{
					costs.SetProperty(Game1.ironTexture, costIron.ToString(), Color.Green);
				}
				else
				{
					costs.SetProperty(Game1.ironTexture, costIron.ToString(), Color.Red);
				}
			}

			AddChild(costs);
		}

		public void LeftButtonPressed()
		{
			build(buildType);
		}

		private void DrawCell()
		{
			background = new Quad(quadWidth, quadWidth, 0x333333, 0.15f);
			AddChild(background);
			icon.ScaleX =  (background.width - borderThickness * 2) / icon.width;
			icon.ScaleY =  (background.height - borderThickness * 2) / icon.height;
			icon.x      += background.width / 2;
			icon.y      += background.height / 2 + borderThickness;
			icon.Alpha  =  0.15;
			DrawCosts();

			AddChild(icon);
		}
	}
}