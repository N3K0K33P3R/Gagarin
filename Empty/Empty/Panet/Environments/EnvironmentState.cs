﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Empty.Panet
{
    abstract class EnvironmentState
    {
       public abstract float MainEnvironmentProperty { get; }
       public abstract string State { get; }
    }
}