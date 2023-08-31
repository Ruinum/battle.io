using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public sealed class ExpOrbPool : Pool<ExpOrb>
{
    public ExpOrbPool(string name, int capacityPool) : base(name, capacityPool)
    {
    }
}