using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    protected float maxSpeed;
    protected float maxHealth;
    protected float maxDamage;

    [SerializeField] protected float currentSpeed;
    [SerializeField] protected float currentHealth;
    [SerializeField] protected float currentDamage;
    [SerializeField] protected Debuff imposedDebuff;
    protected Rigidbody2D rigidbody;


    void Start()
    {
        InitializeElements();
    }
    

    public virtual void ReturnSpeedToMax()
    {
        currentSpeed = maxSpeed;
    }

    public virtual void SetSpeed(float speed)
    {
        if(speed >= 0)
            currentSpeed = speed;
    }
    
    public virtual void ReduceDamageBy(float amountOfDamage)
    {
        currentDamage -= amountOfDamage;
        if (currentDamage < 0)
            currentDamage = 0;
    }

    public virtual void ReduceSpeedBy(float amountOfSpeed)
    {
        currentSpeed -= amountOfSpeed;
        if (currentSpeed < 0)
            currentSpeed = 0;
    }
    

    public virtual void GetDamage(float amountOfDamage)
    {
        currentHealth -= amountOfDamage;
        if (currentHealth <= 0)
            Die();
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }
    
    private void OnCollisionEnter2D(Collision2D col)
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
        rigidbody = GetComponent<Rigidbody2D>();
    }
}