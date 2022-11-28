using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [field:SerializeField] public float Damage { get; protected set; }
    [SerializeField] protected float speed;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(transform.up * speed, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Obstacle") && !col.isTrigger)
            Destroy(gameObject);
        if (col.gameObject.CompareTag("Player"))
            Destroy(gameObject);
    }
}