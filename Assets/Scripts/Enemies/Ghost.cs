using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MovingEnemy
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == 9)
            Physics2D.IgnoreCollision(GetComponent<CapsuleCollider2D>(), other.collider);
    }
}
