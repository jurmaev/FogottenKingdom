using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectrifiedDebuff : Debuff
{
    private float Damage;
    public void ActivateEffectOnEnemy(Enemy enemy)
    {
        enemy.Health -= Damage;
    }
}
