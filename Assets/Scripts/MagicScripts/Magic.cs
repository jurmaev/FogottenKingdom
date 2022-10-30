using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Magic : MonoBehaviour
{
    public float Speed { get; protected set; }
    public int Damage { get; protected set; }
    public int Mana { get; protected set; }
    private Rigidbody2D magicRb;

    void Start()
    {
        InitializeElements();
        Move();
    }

    protected virtual void InitializeElements()
    {
        magicRb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Move()
    {
        magicRb.AddForce(transform.up * Speed, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.TryGetComponent(out Magic otherMagic))
            OnCollisionWithMagic(gameObject);
    }
    
    protected abstract void OnCollisionWithMagic(GameObject otherMagic);
}