using System.Linq;
using UnityEngine;

public class RoomSelector : MonoBehaviour
{
    [SerializeField] private GameObject[] rooms;
    [SerializeField] public GameObject MinimapRoom;
    public GameObject PickRoom(Room room)
    {
        room.SetDoorPositions();
        return rooms.FirstOrDefault(r => r.name == room.DoorPositions);
    }
}