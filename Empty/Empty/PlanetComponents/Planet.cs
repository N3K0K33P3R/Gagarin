using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Empty.Environments;
using MonoFlash.Engine;

namespace Empty
{
    public class Planet
    {
        public Planet()
        {
            Grav = new GravState();
            Termal = new TermalState();
            Wind = new WindState();

            bgDrawer = new BackgroundDrawer(Assets.textures[SkySetting],1280,720);

            CurrentPlanet = this;
        }
        public string GetPlanetProperty =>
            $"Grav:{Grav.GravForce}G\nTemp:{Termal.Temperature}C\nWind:{Wind.WindInfo}m/s";

        public static Planet CurrentPlanet;

        public readonly GravState Grav;
        public readonly TermalState Termal;
        public readonly WindState Wind;

        private static BackgroundDrawer bgDrawer;

        public void Draw(SpriteBatch spriteBatch)
        {
            bgDrawer.Draw(spriteBatch);
            spriteBatch.Begin();
            spriteBatch.DrawString(Assets.font, GetPlanetProperty, Vector2.Zero + Vector2.One, Color.Black);
            spriteBatch.DrawString(Assets.font,GetPlanetProperty,Vector2.Zero,Color.White);        
            spriteBatch.End();
        }
        public void Update(GameTime gameTime)
        {
            Planet.CurrentPlanet.Wind.Update(gameTime);
            bgDrawer.Update(gameTime);
        }


        private string SkySetting
        {
            get
            {

                if (Wind.MainEnvironmentProperty > .5f) return "sky1";
                return "sky0";

            }
        }


    }


}
