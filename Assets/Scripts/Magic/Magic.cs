using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.UIElements;
using UnityEngine;

public abstract class Magic : MonoBehaviour
{
    [field: SerializeField] public float StartSpeed { get; protected set; }
    public float CurrentSpeed { get; protected set; }
    [field: SerializeField] public int Damage { get; protected set; }
    private Rigidbody2D magicRb;
    private DebuffController debuffController;

    public GameObject SuperimposedDebuff
    {
        get
        {
            if (transform.childCount != 0)
                return transform.GetChild(0).gameObject;
            return null;
        }
    }
    
    void Start()
    {
        InitializeElements();
    }

    public virtual void Disappear()
    {
        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        MoveForward();
    }
    
    protected virtual void MoveForward()
    {
        magicRb.velocity = transform.up * CurrentSpeed;
    }

    protected virtual void InitializeElements()
    {
        magicRb = GetComponent<Rigidbody2D>();
        debuffController = GameObject.FindWithTag("DebuffController").GetComponent<DebuffController>();
        CurrentSpeed = StartSpeed;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.TryGetComponent(out Magic otherMagic))
            OnCollisionWithMagic(col.gameObject);
        else if (col.gameObject.TryGetComponent(out Enemy enemy))
            OnCollisionWithEnemy(enemy);
        else if (col.gameObject.CompareTag("Obstacle") && !col.isTrigger)
            Disappear();
    }

    protected virtual void OnCollisionWithEnemy(Enemy enemy)
    {
        Disappear();
    }


    protected virtual void OnCollisionWithMagic(GameObject otherMagic)
    {
        
    }
}