using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChilledDebuff : Debuff
{
    public void ActivateEffectOnEnemy(Enemy enemy)
    {
        enemy.Speed = 4;
    }
}
