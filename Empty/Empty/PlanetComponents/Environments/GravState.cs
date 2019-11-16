using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Empty.Environments
{
   public class GravState:EnvironmentState
    {
        public GravState()
        {
            GravForce = new Random().Next(10, 100);
        }

        public readonly int GravForce;
        public override float MainEnvironmentProperty => GravForce / 100f;

        public override string State
        {
            get
            {
                if (MainEnvironmentProperty > .7f) return "Массивная";
                if (MainEnvironmentProperty > .5f) return "Средней тяжести";
                if (MainEnvironmentProperty > .3f) return "Легкая";
                return "Невесомая";
            }
        }


    }
}
