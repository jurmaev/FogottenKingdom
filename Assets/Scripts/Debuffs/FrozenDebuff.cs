using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrozenDebuff : Debuff
{
    protected override void InitializeElements()
    {
        timeOfAction = 2;
        activationDelay = 0;
        repeatRate = 0.1f;
    }

    protected override void ActivateEffectOnEnemy()
    {
        target.SetSpeed(3);
    }
}
