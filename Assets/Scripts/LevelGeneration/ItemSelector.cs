using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class ItemSelector : MonoBehaviour
{

    [SerializeField] private List<GameObject> items;
    [SerializeField] private List<GameObject> spawnPoints;

    private void Start()
    {
        foreach (var spawnPoint in spawnPoints)
        {
            if (items.Count != 0)
            {
                var randItem = items[Random.Range(0, items.Count)];
                Instantiate(randItem, spawnPoint.transform.position,
                    quaternion.identity);
                items.Remove(randItem);
                // Скрипт для добавления предмета игроку
            }
        }
    }
}
