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
    private Transform target;
    

    protected override void Start()
    {
        base.Start();
        target = FindObjectOfType<PlayerController>().transform;
        InvokeRepeating(nameof(Shoot), 0.5f, cooldown);
    }

    private void FixedUpdate()
    {
        var lookDirection = (target.position - transform.position).normalized;
        if (Vector2.Distance(transform.position, target.position) < minDistance)
            enemyRigidbody.velocity = new Vector2(lookDirection.x, lookDirection.y) * -Speed;
        else if (Vector2.Distance(transform.position, target.position) >= minDistance)
            enemyRigidbody.Sleep();
    }

    protected virtual void Shoot()
    {
        var lookDirection = (target.position - transform.position).normalized;
        var playerRotation =
            Quaternion.Euler(0, 0, Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f);
        Instantiate(projectile, transform.position, playerRotation);
    }
}