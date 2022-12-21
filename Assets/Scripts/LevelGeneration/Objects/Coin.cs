using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Coin : MonoBehaviour
{
    public int Value { get; set; }
    [SerializeField] private float movingTime;
    [SerializeField] private float magnetTime;
    [SerializeField] private float movingSpeed;
    private float delay;
    private Animator animator;
    private Vector3 direction;
    private Rigidbody2D coinRb;
    private GameObject player;
    private bool magnetize;

    private void Start()
    {
        direction = new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), 0);
        animator = GetComponent<Animator>();
        coinRb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        Value = 1;
        // Debug.Log(direction);
        // animator.SetTrigger("jump");
        // coinRb.AddRelativeForce(direction, ForceMode2D.Impulse);
        StartCoroutine(Magnet());
    }

    private void Update()
    {
        if (delay < movingTime)
        {
            Debug.Log("working");
            transform.position += direction * Time.deltaTime * 1000;
            // Debug.Log(transform.position);
            delay += Time.deltaTime;
        }


        if (magnetize)
        {
            var targetPosition = Vector3.MoveTowards(transform.position,
                player.transform.position + new Vector3(0, -1f, 0),
                movingSpeed * Time.deltaTime);
            coinRb.MovePosition(targetPosition);
        }
    }

    private IEnumerator Magnet()
    {
        yield return new WaitForSeconds(magnetTime);
        magnetize = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            EventManager.SendCoinAmountChanged(Value);
            EventManager.SendCoinPicked(Value);
            Destroy(gameObject);
        }
    }
}