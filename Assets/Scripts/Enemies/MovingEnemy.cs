using UnityEngine;

public class MovingEnemy : Enemy
{
    private GameObject playerTarget;
    
    protected override void Start()
    {
        base.Start();
        playerTarget = GameObject.Find("Player");
    }

    private void Update()
    {
        Move();
    }

    protected void Move()
    {
        if (playerTarget == null) return;
        var direction = (playerTarget.transform.position - transform.position).normalized;
        enemyRigidbody.velocity = new Vector2(direction.x, direction.y) * Speed;
    }
}
