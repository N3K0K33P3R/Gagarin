using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Empty.Panet
{
    class WindState : EnvironmentState
    {

        public WindState()
        {
            MaxWindForce = new Random().Next(0, 100);
        }


        public readonly float MaxWindForce;
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
