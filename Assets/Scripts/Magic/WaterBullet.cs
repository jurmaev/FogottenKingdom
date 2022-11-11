using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class WaterBullet : Magic
{
    
    protected override void OnCollisionWithMagic(GameObject otherMagic)
    {
    }

    protected override void OnCollisionWithEnemy(Enemy enemy)
    {
        Destroy(gameObject);
    }
}