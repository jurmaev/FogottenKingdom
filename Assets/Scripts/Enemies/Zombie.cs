using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Update = Unity.VisualScripting.Update;

public class Zombie : Enemy
{
    [SerializeField] private GameObject playerTarget;
    
    protected override void Start()
    {
        base.Start();
        playerTarget = GameObject.Find("Player");
    }

    private void Update()
    {
        // Debug.Log("Текущее здоровье:" + currentHealth);
        var direction = (playerTarget.transform.position - transform.position).normalized;
        enemyRigidbody.velocity = new Vector2(direction.x, direction.y) * Speed;
    }
}
