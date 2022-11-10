using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Debuff: MonoBehaviour
{
    protected float timeOfAction;
    protected float activationDelay;
    protected float repeatRate;
    
    protected Enemy target;
    private void Start()
    {
        if (gameObject.TryGetComponent(out Enemy enemy))
        {
            target = enemy;
            StartCoroutine(nameof(AwakeDebuff));
        }
    }
    

    private  IEnumerator AwakeDebuff()
    {
        InvokeRepeating(nameof(ActivateEffectOnEnemy), activationDelay, repeatRate);
        yield return new WaitForSeconds(timeOfAction);
        CancelInvoke(nameof(ActivateEffectOnEnemy));
    }

    protected abstract void InitializeElements();
    protected abstract void ActivateEffectOnEnemy();
}