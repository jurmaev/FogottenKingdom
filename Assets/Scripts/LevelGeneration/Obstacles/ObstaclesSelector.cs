using UnityEngine;


public class ObstaclesSelector : MonoBehaviour
{
    [SerializeField] private GameObject[] obstacles;
    public GameObject PickObstacles()
    {
        return obstacles[Random.Range(0, obstacles.Length)];
    }
}
