using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class RoomPrefab : MonoBehaviour
{
    public Room Room { get; set; }
    [SerializeField] private Tilemap floorTilemap;
    [SerializeField] private List<Tile> tiles;
    [SerializeField] private Tile[] rightDoor;
    [SerializeField] private Tile[] leftDoor;
    [SerializeField] private Tile[] topDoor;
    [SerializeField] private Tile[] bottomDoor;
    [SerializeField] private GameObject doorPrefab;
    private List<GameObject> doors;

    private void Start()
    {
        doors = new List<GameObject>();
        PaintFloor();
        PaintDoors(false);
        if(Room.Type == Room.RoomType.EntryRoom) ActivateDoors();
    }

    private void PaintFloor()
    {
        for (var x = -(int) Room.RoomSize.x / 2 + 2; x < (int) Room.RoomSize.x / 2 - 2; x++)
        for (var y = -(int) Room.RoomSize.y / 2 + 2; y < (int) Room.RoomSize.y / 2 - 2; y++)
            floorTilemap.SetTile(new Vector3Int(x, y, 0), tiles[Random.Range(0, tiles.Count)]);
    }

    private void PaintDoors(bool isOpen)
    {
        var paintIndex = isOpen ? 1 : 0;
        if (Room.DoorBottom)
        {
            PaintDoor(-1, -(int) Room.RoomSize.y / 2, 0, -(int) Room.RoomSize.y / 2, bottomDoor[paintIndex]);
            SetDoor(transform.position.x, transform.position.y - Room.RoomSize.y / 2 + 1, 90, Door.Position.Bottom);
        }

        if (Room.DoorTop)
        {
            PaintDoor(-1, (int) Room.RoomSize.y / 2 - 1, 0, (int) Room.RoomSize.y / 2 - 1, topDoor[paintIndex]);
            SetDoor(transform.position.x, transform.position.y + Room.RoomSize.y / 2 - 1, 270, Door.Position.Top);
        }

        if (Room.DoorLeft)
        {
            PaintDoor(-(int) Room.RoomSize.x / 2, -1, -(int) Room.RoomSize.x / 2, 0, leftDoor[paintIndex]);
            SetDoor(transform.position.x - Room.RoomSize.x / 2 + 1, transform.position.y, 0, Door.Position.Left);
        }

        if (Room.DoorRight)
        {
            PaintDoor((int) Room.RoomSize.x / 2 - 1, -1, (int) Room.RoomSize.x / 2 - 1, 0, rightDoor[paintIndex]);
            SetDoor(transform.position.x + Room.RoomSize.x / 2 - 1, transform.position.y, 180, Door.Position.Right);
        }
    }

    private void PaintDoor(int x1, int y1, int x2, int y2, Tile doorTile)
    {
        for (var i = x1; i <= x2; i++)
        for (var j = y1; j <= y2; j++)
            floorTilemap.SetTile(new Vector3Int(i, j, 0), doorTile);
    }

    private void SetDoor(float x, float y, float angle, Door.Position position)
    {
        var newDoor = Instantiate(doorPrefab, new Vector3(x, y, transform.position.z), Quaternion.Euler(0, 0, angle));
        newDoor.transform.parent = transform;
        newDoor.GetComponent<Door>().DoorPosition = position;
        newDoor.GetComponent<Door>().currentRoom = this;
        doors.Add(newDoor);
    }

    private void ActivateDoors()
    {
        foreach (var door in doors)
            door.GetComponent<Door>().isActive = true;
    }

    public void DeactivateDoors()
    {
        foreach (var door in doors)
            door.GetComponent<Door>().isActive = false;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            var obstaclesPrefab = GetComponentInChildren<ObstaclesPrefab>();
            if (obstaclesPrefab != null) obstaclesPrefab.SpawnEnemies();
            Invoke(nameof(ActivateDoors), .5f);
            EventManager.SendCameraPosChanged(Room.GridPos);
            EventManager.SendActiveRoomChanged(Room.GridPos);
        }
    }
}