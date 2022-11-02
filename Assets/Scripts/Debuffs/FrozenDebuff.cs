using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrozenDebuff : Debuff
{
    public void ActivateEffectOnEnemy(Enemy enemy)
    {
        enemy.Speed = 1;
    }
}
