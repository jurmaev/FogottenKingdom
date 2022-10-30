using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FireBall : Magic
{
    
    protected override void InitializeElements()
    {
        base.InitializeElements();
        Speed = 10f;
        Damage = 3;
        Mana = 1;
    }
    protected override void OnCollisionWithMagic(GameObject otherMagic)
    {
    }
    
}
