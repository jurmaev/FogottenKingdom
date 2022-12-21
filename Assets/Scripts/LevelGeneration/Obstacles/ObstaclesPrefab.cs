using UnityEngine;

public class ObstaclesPrefab : MonoBehaviour
{
    [SerializeField] private GameObject[] spawners;
    [SerializeField] private GameObject[] lights;
    public bool AreSpawned { get; private set; }

    public void SpawnEnemies()
    {
        if (AreSpawned) return;
        if (spawners.Length > 0)
        {
            var spawner = Instantiate(spawners[Random.Range(0, spawners.Length)], transform.position, Quaternion.identity);
            spawner.transform.parent = transform.parent;
            AreSpawned = true;
        }
    }

    public void GenerateLights()
    {
        var spawnedLight = Instantiate(lights[Random.Range(0, spawners.Length)], transform.position, Quaternion.identity);
        spawnedLight.transform.parent = transform.parent;
    }
}