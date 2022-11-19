using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class RangedEnemy : Enemy
{
    [SerializeField] private float minDistance;
    [SerializeField] private GameObject projectile;
    [SerializeField] private float cooldown;
    private float nextShotTime;
    private Transform target;
    private Rigidbody2D rb;

    protected override void Start()
    {
        base.Start();
        target = FindObjectOfType<PlayerController>().transform;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        var lookDir = (target.position - transform.position).normalized;
        var playerRotation = Quaternion.Euler(0, 0, Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f);
        if (Time.time > nextShotTime)
        {
            Instantiate(projectile, transform.position, playerRotation);
            nextShotTime = Time.time + cooldown;
            
            if (Vector2.Distance(transform.position, target.position) < minDistance)
                rb.velocity = new Vector2(lookDir.x, lookDir.y) * -Speed;
        }
        if(Vector2.Distance(transform.position, target.position) >= minDistance)
            rb.Sleep();
    }
}