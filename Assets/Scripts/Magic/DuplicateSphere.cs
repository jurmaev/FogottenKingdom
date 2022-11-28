using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class DuplicateSphere : Magic
{
    [field: SerializeField] public int NumberOfDuplicates { get; private set; }
    [SerializeField] private bool isDuplicatingNow;
    [SerializeField][Tooltip("На сколько будет понижаться скорость каждый кадр")] private float speedReduction;
    

    protected override void OnCollisionWithMagic(GameObject otherMagic)
    {
        if (!otherMagic.TryGetComponent(out DuplicateSphere duplicateSphere) && !isDuplicatingNow)
        {
            isDuplicatingNow = true;
            var angleBetweenDuplicates = 360.0f / NumberOfDuplicates;
            var startAngle = 0f;
            var startDirection = new Vector2(1, 0);
            for (var i = 0; i < NumberOfDuplicates; i++)
            {
                Instantiate(otherMagic, transform.position, Quaternion.Euler(0f, 0f, startAngle));
                startAngle += angleBetweenDuplicates;
                startDirection.x = startDirection.x * Mathf.Cos(startAngle) - startDirection.y * Mathf.Sin(startAngle);
                startDirection.y = startDirection.x * Mathf.Cos(startAngle) - startDirection.y * Mathf.Sin(startAngle);
            }

            Destroy(otherMagic);
            Destroy(gameObject);
        }
    }
    
    protected override void MoveForward()
    {
        base.MoveForward();
        if (CurrentSpeed - speedReduction < 0)
            CurrentSpeed = 0;
        else
            CurrentSpeed -= speedReduction;
    }

    protected override void OnCollisionWithEnemy(Enemy enemy)
    {
        Destroy(gameObject);
    }
}