using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class DebuffController
{
    private static readonly Dictionary<(string, string), Debuff> debuffFormulas;

    static DebuffController()
    {
        debuffFormulas = new Dictionary<(string, string), Debuff>()
        {
            {(nameof(WetDebuff), nameof(ChilledDebuff)), new FrozenDebuff()},
        };
    }

    public static bool TryMixDebuffs(Debuff firstDebuff, Debuff secondDebuff, out Debuff mixDebuff)
    {
        if (firstDebuff == null || secondDebuff == null)
        {
            mixDebuff = null;
            return false;
        }

        if (debuffFormulas.TryGetValue((nameof(firstDebuff), nameof(secondDebuff)), out Debuff newDebuff1))
        {
            mixDebuff = newDebuff1;
            return true;
        }

        if (debuffFormulas.TryGetValue((nameof(secondDebuff), nameof(firstDebuff)), out Debuff newDebuff2))
        {
            mixDebuff = newDebuff2;
            return true;
        }

        mixDebuff = null;
        return false;
    }
}