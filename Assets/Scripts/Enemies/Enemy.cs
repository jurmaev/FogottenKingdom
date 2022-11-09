using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [field: SerializeField] public float Health { get; private set; }
    [field: SerializeField] public float Speed { get; private set; }
    [field: SerializeField] public float Damage { get; private set; }
    [SerializeField] protected Debuff imposedDebuff;
    protected Rigidbody2D enemyRigidbody;

    protected virtual void Start()
    {
        InitializeElements();
    }

    public virtual void GetDamage(float amountOfDamage)
    {
        Health -= amountOfDamage;
        if (Health < 0)
            Die();
    }

    public virtual void SetSpeed(float speed)
    {
        Speed = speed;
    }

    public virtual void SetDamage(float damage)
    {
        Damage = damage;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.TryGetComponent(out Magic magic))
            CollideWithMagic(magic);
    }


    private void CollideWithMagic(Magic magic)
    {
        GetDamage(magic.Damage);
        DebuffController.TryMixDebuffs(imposedDebuff, magic.SuperimposedDebuff, out imposedDebuff);
    }

    protected virtual void InitializeElements()
    {
        enemyRigidbody = GetComponent<Rigidbody2D>();
    }
    
    protected virtual void Die()
    {
        Destroy(gameObject);
    }
}