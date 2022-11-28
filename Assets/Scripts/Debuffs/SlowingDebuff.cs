using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowingDebuff : Debuff
{
    [SerializeField] [Tooltip("Скорость противника во время дебаффа")] private float enemySpeedDuringEffect;
    private float startEnemySpedd;
    protected override IEnumerator AwakeDebuff()
    {
        startEnemySpedd = target.Speed;
        return base.AwakeDebuff();
    }

    protected override void ActivateEffectOnEnemy()
    {
        target.SetSpeed(enemySpeedDuringEffect);
    }

    protected override void DeactivateEffectOnEnemy()
    {
        if(target != null)
            target.SetSpeed(startEnemySpedd);
    }
}
