using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public abstract class Enemy : MonoBehaviour
{
    [field: SerializeField] public float MaxHealth { get; private set; }
    [SerializeField] protected float currentHealth; 
    [field: SerializeField] public float Speed { get; private set; }
    [field: SerializeField] public float Damage { get; private set; }
    [SerializeField] protected Debuff imposedDebuff;
    [SerializeField] protected Slider healthBar;
    protected Rigidbody2D enemyRigidbody;

    protected virtual void Start()
    {
        InitializeElements();
        currentHealth = MaxHealth;
    }

    public virtual void GetDamage(float amountOfDamage)
    {
        currentHealth -= amountOfDamage;
        if (currentHealth < 0)
            Die();
        healthBar.value = GetSliderFill();
    }

    private float GetSliderFill()
    {
        return currentHealth / MaxHealth;
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
        
        DebuffController.TryMixDebuffs(imposedDebuff, magic.SuperimposedDebuff, out Debuff mixDebuff);
        
    }

    protected virtual void InitializeElements()
    {
        enemyRigidbody = GetComponent<Rigidbody2D>();
        Component imposedDebuffComponent = FindDebuffComponent();
        imposedDebuff = null;
    }
    
    protected virtual void Die()
    {
        Destroy(gameObject);
    }

    private Component FindDebuffComponent()
    {
        var allComponents = GetComponents<Component>();
        var debuffComponent = allComponents.FirstOrDefault(component => component.ToString().Contains("Debuff"));
        return debuffComponent;
    }
}