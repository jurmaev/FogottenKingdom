using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    private CircleCollider2D collider;

    private void Start()
    {
        collider = GetComponent<CircleCollider2D>();
    }

    public void SetShieldRadius(float radius)
    {
        collider.radius = radius;
    }

    public void ToggleShield()
    {
        collider.enabled = !collider.enabled;
    }

    // private void OnTriggerEnter2D(Collider2D col)
    // {
    //     if (col.gameObject.CompareTag("Magic"))
    //     {
    //         var magicRotation = col.gameObject.transform.rotation;
    //         col.gameObject.transform.rotation = Quaternion.Euler(magicRotation.x, magicRotation.y + 180, magicRotation.z);
    //     }
    // }
}
