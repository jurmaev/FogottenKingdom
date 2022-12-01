using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowingDebuff : Debuff
{
    [SerializeField] [Range(0,100)] [Tooltip("Какую скорость будет иметь противник в процентах")] private float enemySpeedDuring;
    private float startEnemySpeed;
    protected override IEnumerator AwakeDebuff()
    {
        startEnemySpeed = target.Speed;
        return base.AwakeDebuff();
    }

    protected override void ActivateEffectOnEnemy()
    {
        target.SetSpeed(startEnemySpeed * enemySpeedDuring / 100);
    }

    public override void DeactivateEffect()
    {
        if(target != null)
            target.SetSpeed(startEnemySpeed);
        base.DeactivateEffect();
    }
}
