using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChilledDebuff : Debuff
{
    protected override void ActivateEffectOnEnemy(Enemy enemy)
    {
        enemy.SetSpeed(6);
    }
}
