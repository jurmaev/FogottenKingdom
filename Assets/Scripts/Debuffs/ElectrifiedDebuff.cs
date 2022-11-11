using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectrifiedDebuff : Debuff
{
    private float damage;
    private float radius;
    
    protected override void InitializeElements()
    {
        timeOfAction = 4;
        activationDelay = 0;
        repeatRate = 0.1f;

        damage = 0.5f;
        radius = 15f;
    }

    protected override void ActivateEffectOnEnemy()
    {
        target.GetDamage(damage);
    }
}
