using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Chest : MonoBehaviour
{
    public int CoinMultiplier { private get; set; }
    [SerializeField] private GameObject coinPrefab;
    public Room.RoomType RoomType { get; set; }
    private Animator animator;
    private int coins;
    private void Start()
    {
        animator = GetComponent<Animator>();
        coins = Random.Range(CoinMultiplier * 3, CoinMultiplier * 4);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            animator.SetTrigger("open");
            if(RoomType == Room.RoomType.NormalRoom)
                Invoke(nameof(SpawnCoins), 1);
            else if(RoomType == Room.RoomType.TreasureRoom)
                Invoke(nameof(DropTreasure), 1);
        }
    }

    private void SpawnCoins()
    {
        Instantiate(coinPrefab, transform.position, Quaternion.identity);
    }

    private void DropTreasure()
    {
        throw new NotImplementedException();
    }
}
