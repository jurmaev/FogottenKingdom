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

    [field: SerializeField]
    public GameObject SuperimposedDebuff
    {
        get
        {
            if (transform.childCount != 0)
                return transform.GetChild(0).gameObject;
            return null;
        }
    }

    private Rigidbody2D magicRb;
    private DebuffController debuffController;

    void Start()
    {
        InitializeElements();
        Move();
    }

    protected virtual void InitializeElements()
    {
        magicRb = GetComponent<Rigidbody2D>();
        debuffController = GameObject.FindWithTag("DebuffController").GetComponent<DebuffController>();
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
        GameObject anotherDebuff = otherMagic.GetComponent<Magic>().SuperimposedDebuff;
        UpdateSuperimposedDebuff(anotherDebuff);
    }

    protected abstract void OnCollisionWithEnemy(Enemy enemy);

    private void UpdateSuperimposedDebuff(GameObject anotherDebuff)
    {
        if (debuffController.TryMixDebuffs(SuperimposedDebuff, anotherDebuff, out GameObject mixedDebuff))
        {
            Destroy(SuperimposedDebuff);
            mixedDebuff.transform.SetParent(gameObject.transform);
        }
    }
}