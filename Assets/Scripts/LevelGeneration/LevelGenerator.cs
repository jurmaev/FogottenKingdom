using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private Transform mapRoot;
    [SerializeField] private Transform minimapRoot;
    [SerializeField] private Vector2 worldSize; // половина размеров
    [SerializeField] private int numberOfRooms;
    [SerializeField] private GameObject roomPrefab;
    [SerializeField] private GameObject minimapRoom;
    private Room[,] rooms;
    private List<Vector2> takenPositions;
    private int gridSizeX, gridSizeY;
    private ObstaclesSelector obstaclesSelector;
    private List<Room> deadEnds;

    private void GenerateRooms()
    {
        takenPositions = new List<Vector2>();
        deadEnds = new List<Room>();
        CreateMap();
        SetDoors();
        SetSpecialRooms();
    }

    private void Start()
    {
        obstaclesSelector = GetComponent<ObstaclesSelector>();
        if (numberOfRooms >= worldSize.x * 2 * worldSize.y * 2)
            numberOfRooms = Mathf.RoundToInt(worldSize.x * 2 * worldSize.y * 2);
        gridSizeX = Mathf.RoundToInt(worldSize.x);
        gridSizeY = Mathf.RoundToInt(worldSize.y);
        GenerateRooms();
    }

    private void CreateMap()
    {
        rooms = new Room[gridSizeX * 2, gridSizeY * 2];
        rooms[gridSizeX, gridSizeY] = new Room(Vector2.zero, Room.RoomType.EntryRoom);
        takenPositions.Add(Vector2.zero);
        const float randomCompareStart = 0.01f, randomCompareEnd = 0.2f;
        for (var i = 0; i < numberOfRooms - 1; i++)
        {
            var lerpValue = i / ((float) numberOfRooms - 1);
            var randomCompare = Mathf.Lerp(randomCompareStart, randomCompareEnd, lerpValue);
            var checkPos = NewPosition(false);
            if (NumberOfNeighbors(checkPos, takenPositions) > 1 &&
                Random.value > randomCompare)
                checkPos = NewPosition(true);
            rooms[(int) checkPos.x + gridSizeX, (int) checkPos.y + gridSizeY] =
                new Room(checkPos, Room.RoomType.NormalRoom);
            takenPositions.Insert(0, checkPos);
        }
    }

    private Vector2 NewPosition(bool isOneNeighbour)
    {
        var x = 0;
        var y = 0;
        var counter = 0;
        var checkingPos = Vector2.zero;
        while (takenPositions.Contains(checkingPos) || x >= gridSizeX || x < -gridSizeX || y >= gridSizeY ||
               y < -gridSizeY)
        {
            var randIndex = Mathf.RoundToInt(Random.value * (takenPositions.Count - 1));
            if (isOneNeighbour)
            {
                while (NumberOfNeighbors(takenPositions[randIndex], takenPositions) > 1 && counter < 100)
                {
                    randIndex = Mathf.RoundToInt(Random.value * (takenPositions.Count - 1));
                    counter++;
                }

                if (counter >= 100)
                    print("Error: could not find position with only one neighbor");
            }

            x = (int) takenPositions[randIndex].x;
            y = (int) takenPositions[randIndex].y;
            var axis = Random.value < 0.5f;
            var positive = Random.value < 0.5f;
            if (axis)
                y += positive ? 1 : -1;
            else
                x += positive ? 1 : -1;
            checkingPos = new Vector2(x, y);
        }

        return checkingPos;
    }

    private int NumberOfNeighbors(Vector2 checkingPos, List<Vector2> usedPositions)
    {
        return new[] {Vector2.right, Vector2.left, Vector2.down, Vector2.up}.Count(direction =>
            usedPositions.Contains(checkingPos + direction));
    }

    private void InstantiateRooms()
    {
        foreach (var room in rooms)
        {
            if (room == null)
                continue;
            var drawPos = room.GridPos;
            drawPos.x *= Room.RoomSize.x;
            drawPos.y *= Room.RoomSize.y;

            var newRoom = Instantiate(roomPrefab, drawPos, quaternion.identity);
            var roomStats = newRoom.GetComponent<RoomPrefab>();
            roomStats.Room = room;
            newRoom.transform.parent = mapRoot;

            if (roomStats.Room.Type is Room.RoomType.NormalRoom or Room.RoomType.EntryRoom or Room.RoomType.ShopRoom)
            {
                var obstacles = Instantiate(obstaclesSelector.PickObstacles(roomStats.Room.Type), drawPos,
                    quaternion.identity);
                obstacles.transform.parent = newRoom.transform;
            }

            var minimapDrawPos = room.GridPos;
            minimapDrawPos.x *= Room.MinimapRoomSize.x + 1;
            minimapDrawPos.y *= Room.MinimapRoomSize.y + 1;
            var newMinimapRoom = Instantiate(minimapRoom, minimapDrawPos, quaternion.identity);
            var minimapRoomStats = newMinimapRoom.GetComponent<MinimapRoom>();
            minimapRoomStats.Room = room;
            newMinimapRoom.transform.parent = minimapRoot;
        }
    }

    private void SetDoors()
    {
        for (var x = 0; x < gridSizeX * 2; x++)
        {
            for (var y = 0; y < gridSizeY * 2; y++)
            {
                if (rooms[x, y] == null)
                    continue;
                rooms[x, y].DoorBottom = y - 1 >= 0 && rooms[x, y - 1] != null;
                rooms[x, y].DoorTop = y + 1 < gridSizeY * 2 && rooms[x, y + 1] != null;
                rooms[x, y].DoorLeft = x - 1 >= 0 && rooms[x - 1, y] != null;
                rooms[x, y].DoorRight = x + 1 < gridSizeX * 2 && rooms[x + 1, y] != null;
                if (rooms[x, y].GetDoorCount() == 1)
                    deadEnds.Add(rooms[x, y]);
            }
        }
    }

    private void SetSpecialRooms()
    {
        if (deadEnds.Count < 3)
        {
            Debug.Log("Not enough dead ends, regenerating rooms...");
            GenerateRooms();
            return;
        }

        var rnd = new System.Random();
        deadEnds = deadEnds.OrderBy(x => rnd.Next()).ToList();
        deadEnds[0].Type = Room.RoomType.BossRoom;
        deadEnds[1].Type = Room.RoomType.ShopRoom;
        deadEnds[2].Type = Room.RoomType.TreasureRoom;
        InstantiateRooms();
    }
}