using UnityEngine;

public class MovingEnemy : Enemy
{
    private GameObject playerTarget;
    private SpriteRenderer spriteRenderer;
    
    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
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
        if (gameObject.name.Contains("Ghost"))
            spriteRenderer.flipX = !(playerTarget.transform.position.x <= transform.position.x);
        else
            spriteRenderer.flipX = playerTarget.transform.position.x <= transform.position.x;
        enemyRigidbody.velocity = new Vector2(direction.x, direction.y) * Speed;
        
    }
}
