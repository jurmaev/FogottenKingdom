using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class WaterBullet : Magic
{
    protected override void InitializeElements()
    {
        base.InitializeElements();
        Speed = 15;
        Damage = 1;
        Mana = 0;
    }

    protected override void OnCollisionWithMagic(GameObject otherMagic)
    {
        throw new System.NotImplementedException();
    }

    protected override void OnCollisionWithEnemy(Enemy enemy)
    {
        throw new System.NotImplementedException();
    }
}
