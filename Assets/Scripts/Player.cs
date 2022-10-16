using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Search;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    private Vector2 moveDirection;
    private Rigidbody2D playerRb;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject projectilePrefab;
    private Vector2 mousePos;
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        ProcessInputs();
        if (Input.GetButtonDown("Fire1"))
            Shoot();
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate()
    {
        Move();
        RotateFirepoint();
    }

    private void ProcessInputs()
    {
        moveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
    }

    private void RotateFirepoint()
    {
        var lookDir = mousePos - playerRb.position;
        var angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        firePoint.transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void Move()
    {
        playerRb.velocity = new Vector2(moveDirection.x * speed, moveDirection.y * speed);
    }

    private void Shoot()
    {
        var projectile = Instantiate(projectilePrefab, firePoint.position, projectilePrefab.transform.rotation);
        var rb = projectile.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * projectileSpeed, ForceMode2D.Impulse);
    }
}
