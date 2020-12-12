using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicLib
{
    public interface iView
    {
        Dictionary<string, iFeature> AllFeature { get; }
    }
}
