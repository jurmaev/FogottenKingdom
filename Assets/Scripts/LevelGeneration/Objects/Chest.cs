using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Chest : MonoBehaviour
{
    public int CoinMultiplier { private get; set; }
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private List<GameObject> artefacts;
    public Room.RoomType RoomType { get; set; }
    private Animator animator;
    private int coins;
    private bool IsGiveArtifact;
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
            if(RoomType == Room.RoomType.TreasureRoom)
                Invoke(nameof(DropTreasure), 1);
        }
    }

    private void SpawnCoins()
    {
        Instantiate(coinPrefab, transform.position, Quaternion.identity);
    }

    private void DropTreasure()
    {
        if (!IsGiveArtifact)
        {
            var randomArtefactPrefab = artefacts[Random.Range(0, artefacts.Count - 1)];
            var artifact = Instantiate(randomArtefactPrefab, transform.position, Quaternion.identity);
            artifact.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 2, ForceMode2D.Impulse);
            IsGiveArtifact = true;
        }
    }
}
