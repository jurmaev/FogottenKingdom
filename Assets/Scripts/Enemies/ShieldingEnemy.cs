using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldingEnemy : Enemy
{
    [SerializeField] private float shieldCooldown;
    [SerializeField] private float shieldRadius;
    private float nextShieldActivateTime;
    private LineRenderer lineRenderer;
    private Shield shield;
    private int segments = 50;

    protected override void Start()
    {
        base.Start();
        shield = GetComponentInChildren<Shield>();
        lineRenderer = GetComponentInChildren<LineRenderer>();
        shield.SetShieldRadius(shieldRadius + lineRenderer.widthMultiplier / 2);
        lineRenderer.enabled = false;
        lineRenderer.positionCount = segments + 1;
        lineRenderer.useWorldSpace = false;
        SetShieldLine();
        InvokeRepeating("ToggleShield", 0, shieldCooldown);
    }

    private void SetShieldLine()
    {
        var angle = 20f;
        for (var i = 0; i <= segments; i++)
        {
            var x = Mathf.Sin(Mathf.Deg2Rad * angle) * shieldRadius;
            var y = Mathf.Cos(Mathf.Deg2Rad * angle) * shieldRadius;
            lineRenderer.SetPosition(i, new Vector2(x, y));
            angle += 360f / segments;
        }
    }

    private void ToggleShield()
    {
        lineRenderer.enabled = !lineRenderer.enabled;
        shield.ToggleShield();
    }
}
