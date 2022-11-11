using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WetDebuff : Debuff
{
    
    protected override void InitializeElements()
    {
        timeOfAction = 4;
        activationDelay = 0;
        repeatRate = 0.1f;
    }

    protected override void ActivateEffectOnEnemy()
    {
        
    }
}
