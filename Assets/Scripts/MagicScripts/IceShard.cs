using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceShard : Magic
{
    protected override void InitializeElements()
    {
        base.InitializeElements();
        Speed = 8;
        Damage = 4;
        Mana = 2;
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
