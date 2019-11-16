using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Empty.Environments
{
    public class WindState : EnvironmentState
    {

        public float WindForce { get; protected set; }
        public int WindInfo => (int)(WindForce / 4f) * 4; 

        public WindState()
        {
            MaxWindForce = new Random().Next(0, 100);
        }

        public void Update(GameTime gameTime)
        {
            WindForce =
                MathHelper.Clamp(WindForce + Perlin.Noise((float)gameTime.TotalGameTime.TotalMilliseconds),
                1, MaxWindForce);          

        }

        public readonly int MaxWindForce;
        public override float MainEnvironmentProperty => MaxWindForce / 100f;

        public override string State
        {
            get
            {
                if (MainEnvironmentProperty > .5f) return "Ветренно";
                return "Нормально";
            }
        }

    }
}
