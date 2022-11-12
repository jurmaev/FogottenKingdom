using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceShard : Magic
{
    
    protected override void OnCollisionWithEnemy(Enemy enemy)
    {
        Destroy(gameObject);
    }
}
