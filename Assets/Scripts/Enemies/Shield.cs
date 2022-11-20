using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField] private float reboundAngle = 45f;
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

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Magic"))
        {
            var magicTransform = col.gameObject.transform;
            // magicTransform.rotation = Quaternion.Euler(0, 0, 180f - 2 *);
        }
    }
}