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
    [SerializeField] protected Slider healthBar;
    protected Rigidbody2D enemyRigidbody;
    private DebuffController debuffController;

    protected virtual void Start()
    {
        currentHealth = MaxHealth;
        InitializeElements();
    }

    public virtual void GetDamage(float amountOfDamage)
    {
        currentHealth -= amountOfDamage;
        if (currentHealth <= 0)
            Die();
        healthBar.value = GetSliderFill();
    }

    /// <summary>
    /// Возращает локльные координаты полоски здоровья, нужел для того, чтобы 
    /// </summary>
    /// <returns></returns>
    public Vector3 GetHealthBarCoordinates()
    {
        return healthBar.transform.position;
    }

    public virtual void SetSpeed(float speed)
    {
        Speed = speed;
    }

    public virtual void SetDamage(float damage)
    {
        Damage = damage;
    }

    private float GetSliderFill()
    {
        return currentHealth / MaxHealth;
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.TryGetComponent(out Magic magic))
            CollideWithMagic(magic);
    }

    protected virtual void CollideWithMagic(Magic magic)
    {
        GetDamage(magic.Damage);
        ApplyDebuffFromMagic(magic);
    }

    protected virtual void InitializeElements()
    {
        enemyRigidbody = GetComponent<Rigidbody2D>();
        debuffController = GameObject.FindWithTag("DebuffController").GetComponent<DebuffController>();
    }


    private void ApplyDebuffFromMagic(Magic magic)
    {
        if (TryGetImposedDebuff(out GameObject imposedDebuff) && debuffController.TryMixDebuffs(imposedDebuff,
                magic.SuperimposedDebuff,
                out GameObject mixedDebuff))
        {
            imposedDebuff.GetComponent<Debuff>().DeactivateEffect();
            mixedDebuff.transform.SetParent(gameObject.transform);
            mixedDebuff.GetComponent<Debuff>().Activate(this);
        }
        
        else if (magic.SuperimposedDebuff != null)
        {
            imposedDebuff = Instantiate(magic.SuperimposedDebuff, transform.position, Quaternion.identity);
            imposedDebuff.transform.SetParent(gameObject.transform);
            imposedDebuff.GetComponent<Debuff>().Activate(this);
        }
    }

    private bool TryGetImposedDebuff(out GameObject imposedDebuff)
    {
        foreach (Transform child in transform)
        {
            if (child.tag == "Debuff")
            {
                imposedDebuff = child.gameObject;
                return true;
            }
        }
        imposedDebuff = null;
        return false;
    }

    protected virtual void Die()
    {
        EventManager.SendEnemyDeath(gameObject);
        Destroy(gameObject);
    }
}