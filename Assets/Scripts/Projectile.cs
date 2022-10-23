using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Collider2D col;

    private int delay = 5;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitAndDestroy());
        col = GetComponent<Collider2D>();
        Physics2D.IgnoreCollision(FindObjectOfType<Player>().GetComponent<Collider2D>(), col);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Destroy(gameObject);
    }
    
    IEnumerator WaitAndDestroy(){
        yield return new WaitForSeconds(delay);
        Destroy (gameObject);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        Destroy(gameObject);
    }
}
