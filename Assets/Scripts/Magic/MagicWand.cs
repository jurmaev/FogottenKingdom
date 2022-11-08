using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MagicWand : MonoBehaviour
{
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

    public void Shoot(GameObject magic)
    {
        Instantiate(magic, firePoint.position, firePoint.rotation);
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
