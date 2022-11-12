using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChilledDebuff : Debuff
{
    [SerializeField][Tooltip("Какая станет скорость у врага")]private float enemySpeed;
    protected override void ActivateEffectOnEnemy()
    {
        target.SetSpeed(enemySpeed);
    }
}
