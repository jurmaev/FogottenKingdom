using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectrifiedDebuff : Debuff
{
   [SerializeField][Tooltip("Урон который будет наноситься ближайшим врагам")] private float damage;
   [SerializeField][Tooltip("Радиус круга")] private float radius;
   
    protected override void ActivateEffectOnEnemy()
    {
        target.GetDamage(damage);
    }
}
