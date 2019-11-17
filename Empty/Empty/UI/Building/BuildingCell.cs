using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoFlash.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Empty.UI.Building
{
    public class BuildingCell : Sprite
    {
        public int quadWidth = 100;
        public Quad background;
        public int costStone = 0;
        public int costTimber = 0;
        public int costIron = 0;

        private int borderThickness = 5;
        private Image icon;

        public  Property costs;
        private Action<Interface.BuildType> build;
        public Interface.BuildType buildType;
        public BuildingCell(Texture2D icon, int stoneCost, int timberCost, int ironCost, Interface.BuildType type, Action<Interface.BuildType> build)
        {
            costStone = stoneCost;
            costTimber = timberCost;
            costIron = ironCost;
            this.icon = new Image(icon);
            this.build = build;
            buildType = type;
            DrawCell();
            
        }

        private void DrawCell()
        {
            
            background = new Quad(quadWidth, quadWidth, 0x333333, 0.15f);
            AddChild(background);
            icon.ScaleX = (background.width - borderThickness * 2) / icon.width;
            icon.ScaleY = (background.height - borderThickness * 2) / icon.height;
            icon.x += background.width / 2;
            icon.y += background.height / 2 + borderThickness;
            icon.Alpha = 0.15;
            DrawCosts();

            AddChild(icon);
        }

        public void DrawCosts()
        {
            costs = new Property(false);
            if (costStone > 0)
            {
                if (costStone <= Resources.Stone)
                {
                    costs.SetProperty(Game1.stoneTexture, costStone.ToString(), Microsoft.Xna.Framework.Color.Green);
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
                    costs.SetProperty(Game1.woodTexture, costTimber.ToString(), Microsoft.Xna.Framework.Color.Green);
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
                    costs.SetProperty(Game1.ironTexture, costIron.ToString(), Microsoft.Xna.Framework.Color.Green);
                }
                else
                {
                    costs.SetProperty(Game1.ironTexture, costIron.ToString(), Color.Red);
                }
            }

            AddChild(costs);
        }

        private ButtonState wasPressed = ButtonState.Released;
        public override void Update(float delta)
        {
            if (Resources.Stone >= costStone &&
                Resources.Timber >= costTimber &&
                Resources.Iron >= costIron)
            {
                if (Mouse.GetState().X > globalX && Mouse.GetState().X < globalX + quadWidth &&
                Mouse.GetState().Y > globalY && Mouse.GetState().Y < globalY + quadWidth)
                {
                    icon.Alpha = 1;
                    Empty.Building.BuildProcessing.lockFlag = true;
                    if (Mouse.GetState().LeftButton == ButtonState.Pressed && wasPressed != ButtonState.Pressed)
                    {
                        LeftButtonPressed();
                    }
                }
                else
                {
                    icon.Alpha = 0.15;
                    Empty.Building.BuildProcessing.lockFlag = false;
                }
            }
            else icon.Alpha = 0.15;


            wasPressed = Mouse.GetState().LeftButton;
            base.Update(delta);
        }

        public void LeftButtonPressed()
        {
            build(buildType);
        }



        public override void Draw(SpriteBatch sb, GameTime gameTime = null)
        {
            sb.DrawRectangle(new Vector2((float)this.x, (float)this.y), new Vector2((float)background.width, (float)background.height), Microsoft.Xna.Framework.Color.Black, borderThickness, 0);
            base.Draw(sb, gameTime);

            
        }
    }

}
