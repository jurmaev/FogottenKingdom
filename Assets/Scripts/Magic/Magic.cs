using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public abstract class Magic : MonoBehaviour
{
    [field: SerializeField] public float Speed { get; protected set; }
    [field: SerializeField] public int Damage { get; protected set; }
    [field: SerializeField] public int Mana { get; protected set; }
    [field: SerializeField] public Debuff SuperimposedDebuff { get; protected set; }
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
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.TryGetComponent(out Magic otherMagic))
            OnCollisionWithMagic(col.gameObject);
        else if (col.gameObject.TryGetComponent(out Enemy enemy))
            OnCollisionWithEnemy(enemy);
        else if (col.gameObject.CompareTag("Obstacle") && !col.isTrigger)
            Destroy(gameObject);
    }

    protected virtual void Move()
    {
        magicRb.AddForce(transform.up * Speed, ForceMode2D.Impulse);
    }

    protected virtual void OnCollisionWithMagic(GameObject otherMagic)
    {
        UpdateSuperimposedDebuff(otherMagic.GetComponent<Magic>().SuperimposedDebuff);
    }
    protected abstract void OnCollisionWithEnemy(Enemy enemy);

    private void UpdateSuperimposedDebuff(Debuff anotherDebuff)
    {
        if (DebuffController.TryMixDebuffs(SuperimposedDebuff, anotherDebuff, out Debuff mixedDebuff))
        {
            if(mixedDebuff != null)
                SuperimposedDebuff = mixedDebuff;
        }
    }
}