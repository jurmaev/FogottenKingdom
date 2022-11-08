using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTarget : Enemy
{
    [SerializeField] private float flashTime = 0.5f;
    private SpriteRenderer _spriteRenderer;
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void InitializeElements()
    {
        base.InitializeElements();
        maxSpeed = 5;
        maxHealth = 10000;
        maxDamage = 1;
        currentSpeed = maxSpeed;
        currentHealth = maxHealth;
        currentDamage = maxDamage;
    }

    public override void GetDamage(float amountOfDamage)
    {
        _spriteRenderer.color = Color.red;
        Invoke(nameof(ResetColor), flashTime);
        Debug.Log($"Получил {amountOfDamage}");
    }
    
    private void ResetColor()
    {
        _spriteRenderer.color = Color.white;
    }
}