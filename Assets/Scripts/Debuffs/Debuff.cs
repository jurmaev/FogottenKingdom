using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Debuff: MonoBehaviour
{

    protected float startTime = 0f;
    protected float effectRepeatTime = 0.1f;
    private void Start()
    {
        if(gameObject.TryGetComponent(out Enemy enemy))
            InvokeRepeating(nameof(ActivateEffectOnEnemy), startTime, effectRepeatTime);
    }
    
    protected abstract void ActivateEffectOnEnemy(Enemy enemy);
}