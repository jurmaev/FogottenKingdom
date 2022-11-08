using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrozenDebuff : Debuff
{
    protected override void ActivateEffectOnEnemy(Enemy enemy)
    {
        enemy.SetSpeed(3);
    }
}
