using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicWand : MonoBehaviour
{
    [SerializeField] private float projectileSpeed;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    private Vector2 mousePos;
    private Camera camera;
    void Start()
    {
        camera = Camera.main;
    }

    private void Update()
    {
        RotateFirepoint();
    }

    public void Shoot()
    {
        var projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        var projectileRb = projectile.GetComponent<Rigidbody2D>();
        projectileRb.AddForce(firePoint.up * projectileSpeed, ForceMode2D.Impulse);
    }
    
    private void RotateFirepoint()
    {
        mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
        var lookDirection = mousePos - new Vector2(firePoint.position.x, firePoint.position.y);
        lookDirection.Normalize();
        var angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(0,0, angle);
    }
}
