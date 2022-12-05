using UnityEngine;


public class ObstaclesSelector : MonoBehaviour
{
    [SerializeField] private GameObject[] obstacles;
    [SerializeField] private GameObject entryRoom;
    public GameObject PickObstacles(bool isEntry)
    {
        return isEntry ? entryRoom :  obstacles[Random.Range(0, obstacles.Length)];
    }
}
