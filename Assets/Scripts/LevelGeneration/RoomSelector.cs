using System.Linq;
using UnityEngine;

public class RoomSelector : MonoBehaviour
{
    [SerializeField] private GameObject[] rooms;

    // public int type; // 0: normal, 1: enter

    public GameObject PickRoom(Room room)
    {
        room.SetDoorPositions();
        return rooms.FirstOrDefault(r => r.name == room.doorPositions);
    }
}