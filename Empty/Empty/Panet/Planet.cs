using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Empty.Panet
{
    class Planet
    {

        public Planet()
        {
            Grav = new GravState();
            Termal = new TermalState();
            Wind = new WindState();
        }
        public string GetPlanetProperty =>
            $"{Grav.State}+'\n'+{Termal.State}+'\n'+{Wind.State}";

        public static Planet CurrentPlanet;

        public readonly GravState Grav;
        public readonly TermalState Termal;
        public readonly WindState Wind;


    }
}
