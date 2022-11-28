using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class ElectricBall : Magic
{
    [SerializeField][Tooltip("На сколько будет понижаться скорость каждый кадр")] private float speedReduction;
    
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
