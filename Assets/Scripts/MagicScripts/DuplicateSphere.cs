using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuplicateSphere : Magic
{
    public int NumberOfDuplicates { get; private set; }
    
    protected override void InitializeElements()
    {
        base.InitializeElements();
        Speed = 3f;
        Damage = 0;
        Mana = 3;
    }

    protected override void OnCollisionWithMagic(GameObject otherMagic)
    {
        var angleBetweenDuplicates = 360.0f / NumberOfDuplicates;
        var startAngle = 0f;
        var startDirection = new Vector2(1, 0);
        for (var i = 0; i < NumberOfDuplicates; i++)
        {
            Instantiate(otherMagic, startDirection, Quaternion.Euler(0f, 0f, startAngle));
            startAngle += angleBetweenDuplicates;
            startDirection.x = startDirection.x * Mathf.Cos(startAngle) - startDirection.y * Mathf.Sin(startAngle);
            startDirection.y = startDirection.x * Mathf.Cos(startAngle) - startDirection.y * Mathf.Sin(startAngle);
        }
    }
}