using UnityEngine;


public class ObstaclesSelector : MonoBehaviour
{
    [SerializeField] private GameObject[] obstacles;
    [SerializeField] private GameObject entryRoom;
    [SerializeField] private GameObject shopRoom;
    public GameObject PickObstacles(Room.RoomType roomType)
    {
        return roomType switch
        {
            Room.RoomType.EntryRoom => entryRoom,
            Room.RoomType.ShopRoom => shopRoom,
            _ => obstacles[Random.Range(0, obstacles.Length)]
        };
    }
}
