using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public abstract class Debuff : MonoBehaviour
{
    [field: SerializeField] public string Name { get; protected set; }

    [SerializeField] [Tooltip("Время действия дебаффа")]
    protected float timeOfAction;

    [SerializeField] [Tooltip("Раз в сколько секунд,будет активироваться эффект дебаффа")]
    protected float repeatRate;

    [SerializeField] protected Enemy target;
    
    public void Activate(Enemy target)
    {
        this.target = target;
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        StartCoroutine(nameof(AwakeDebuff));
        transform.position = target.GetHealthBarCoordinates() +
                             new Vector3(0, gameObject.GetComponent<SpriteRenderer>().size.y / 2, 0);
    }


    protected virtual IEnumerator AwakeDebuff()
    {
        InvokeRepeating(nameof(ActivateEffectOnEnemy), 0, repeatRate);
        yield return new WaitForSeconds(timeOfAction);
        CancelInvoke(nameof(ActivateEffectOnEnemy));
        DeactivateEffectOnEnemy();
        Destroy(gameObject);
    }

    protected abstract void ActivateEffectOnEnemy();
    protected abstract void DeactivateEffectOnEnemy();
}