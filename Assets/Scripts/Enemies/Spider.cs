using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class Spider : MovingEnemy
{
    private void Update()
    {
        Move();
        Rotate();
    }

    private void Rotate()
    {
        var angle = Mathf.Atan2(enemyRigidbody.velocity.y, enemyRigidbody.velocity.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
