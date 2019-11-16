using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Empty.Panet
{
    class TermalState : EnvironmentState
    {
        public TermalState()
        {
            Temperature = new Random().Next(-100, 100);
        }


        public readonly int Temperature;
        public override float MainEnvironmentProperty => Temperature/100f;

        public override string State
        {
            get {
                if (MainEnvironmentProperty < -.5f) return"Холодно";
                if (MainEnvironmentProperty > .5f) return "Жарко";
                return "Нормально";
            }
        }
    }
}
