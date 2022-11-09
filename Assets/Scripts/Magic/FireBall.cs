using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FireBall : Magic
{

    protected override void OnCollisionWithMagic(GameObject otherMagic)
    {
    }

    protected override void OnCollisionWithEnemy(Enemy enemy)
    {
        Destroy(gameObject);
    }
}