using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class DuplicateSphere : Magic
{
    [field: SerializeField] public int NumberOfDuplicates { get; private set; }
    [SerializeField] private bool isDuplicatingNow;

    [SerializeField] [Tooltip("На сколько будет понижаться скорость каждый кадр")]
    private float speedReduction;

    [SerializeField] [Tooltip("Магия, которая не будет копироваться")]
    private List<Magic> exceptions;


    protected override void OnCollisionWithMagic(GameObject otherMagic)
    {
        if (CheckMagicForDuplication(otherMagic) && !isDuplicatingNow)
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

            otherMagic.GetComponent<Magic>().Disappear();
            Disappear();
        }
    }

    protected override void MoveForward()
    {
        base.MoveForward();
        if (currentSpeed - speedReduction < 0)
            currentSpeed = 0;
        else
            currentSpeed -= speedReduction;
    }

    private bool CheckMagicForDuplication(GameObject otherMagic)
    {
        if (otherMagic.TryGetComponent(out Magic someMagic))
        {
            string magicName = someMagic.GetType().Name;
            return !exceptions.Exists(magic =>
            {
                return magic.GetType().Name == magicName;
            });
        }
        return false;
    }

    protected override void OnCollisionWithObstacle()
    {
        currentSpeed = 0;
        speedReduction = 0;
    }
}