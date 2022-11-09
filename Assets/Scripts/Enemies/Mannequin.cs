using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mannequin : Enemy
{
    [SerializeField] private float flashTime = 0.5f;
    private SpriteRenderer spriteRenderer;
    
    protected override void InitializeElements()
    {
        base.InitializeElements();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public override void GetDamage(float amountOfDamage)
    {
        spriteRenderer.color = Color.red;
        Invoke(nameof(ResetColor), flashTime);
        Debug.Log($"Получил {amountOfDamage}");
    }
    
    private void ResetColor()
    {
        spriteRenderer.color = Color.white;
    }
}