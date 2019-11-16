using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Empty
{
    public class BackgroundDrawer
    {
        Rectangle rectangle1;
        Rectangle rectangle2;
        private readonly Texture2D backgroundTexture;
        static int ScreenWidth, ScreenHeight;

        public BackgroundDrawer(Texture2D texture, int wight,int height)
        {
            backgroundTexture = texture;
            ScreenWidth = wight;
            ScreenHeight = height;

            rectangle1 = new Rectangle(0, 0, ScreenWidth, ScreenHeight);
            rectangle2 = new Rectangle(ScreenWidth , 0, ScreenWidth, ScreenHeight);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(samplerState:SamplerState.PointClamp);
            spriteBatch.Draw(backgroundTexture, rectangle1, Color.White);
            spriteBatch.Draw(backgroundTexture, rectangle2, Color.White);
            spriteBatch.End();

        }
       public void Update(GameTime gameTime)
        {

        
                if (rectangle1.X + ScreenWidth <= 0)
                    rectangle1.X = rectangle2.X + ScreenWidth;
                if (rectangle2.X + ScreenWidth <= 0)
                    rectangle2.X = rectangle1.X + ScreenWidth;
       
       
            rectangle1.X -= 1;
            rectangle2.X -= 1;    
            //rectangle1.X += (int)Planet.CurrentPlanet.Wind.WindForce;
            //rectangle2.X += (int)Planet.CurrentPlanet.Wind.WindForce;          
        }
    }
}
