using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Empty.Environments
{
    public abstract class EnvironmentState
    {
       public abstract float MainEnvironmentProperty { get; }
       public abstract string State { get; }
    }
}
