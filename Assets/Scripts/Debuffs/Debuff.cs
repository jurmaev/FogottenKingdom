using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Debuff: MonoBehaviour
{
    [SerializeField][Tooltip("Время действия дебаффа")]protected float timeOfAction;
    [SerializeField][Tooltip("Промежуток времени, через который повторяется дебафф")]protected float repeatRate;
    
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
        InvokeRepeating(nameof(ActivateEffectOnEnemy), 0, repeatRate);
        yield return new WaitForSeconds(timeOfAction);
        CancelInvoke(nameof(ActivateEffectOnEnemy));
    }
    
    protected abstract void ActivateEffectOnEnemy();
}