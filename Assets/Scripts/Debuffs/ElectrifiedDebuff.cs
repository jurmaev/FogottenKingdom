using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectrifiedDebuff : Debuff
{
    private float damage = 0.3f;

    protected override void ActivateEffectOnEnemy(Enemy enemy)
    {
        enemy.GetDamage(damage);
    }
}
