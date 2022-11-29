using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    private void Start()
    {
        Instantiate(enemyPrefab, transform.position, Quaternion.identity);
    }
}
