using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectrifiedDebuff : Debuff
{
   [SerializeField]private float damage;
   [SerializeField]private float radius;
   
    protected override void ActivateEffectOnEnemy()
    {
        target.GetDamage(damage);
    }

    public override void DeactivateEffect()
    {
        Destroy(gameObject);
    }
}
