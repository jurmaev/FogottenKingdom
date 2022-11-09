using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelGeneration : MonoBehaviour
{
    [SerializeField] private Transform mapRoot;
    [SerializeField] private Vector2 worldSize = new(4, 4); // половина размеров
    [SerializeField] private int numberOfRooms = 20;
    private Room[,] rooms;
    private readonly List<Vector2> takenPositions = new();
    private int gridSizeX, gridSizeY;
    
    private RoomSelector _roomSelector;

    private void Start()
    {
        if (numberOfRooms >= worldSize.x * 2 * worldSize.y * 2)
            numberOfRooms = Mathf.RoundToInt(worldSize.x * 2 * worldSize.y * 2);

        _roomSelector = GetComponent<RoomSelector>();
        gridSizeX = Mathf.RoundToInt(worldSize.x); 
        gridSizeY = Mathf.RoundToInt(worldSize.y);
        CreateRooms(); // создаем саму карту
        SetRoomDoors(); // задаем двери у комнат
        DrawMap(); // создаем объекты комнат
    }

    private void CreateRooms()
    {
        rooms = new Room[gridSizeX * 2, gridSizeY * 2];
        rooms[gridSizeX, gridSizeY] = new Room(Vector2.zero, 1);
        takenPositions.Insert(0, Vector2.zero);
        //magic numbers
        const float  randomCompareStart = 0.2f, randomCompareEnd = 0.01f;
        for (var i = 0; i < numberOfRooms - 1; i++)
        {
            var randomPerc = i / (float) numberOfRooms - 1;
            var randomCompare = Mathf.Lerp(randomCompareStart, randomCompareEnd, randomPerc);
            var checkPos = NewPosition();
            if (NumberOfNeighbors(checkPos, takenPositions) > 1 && Random.value > randomCompare) // проверяем новую позицию
            {
                var iterations = 0;
                do
                {
                    checkPos = SelectiveNewPosition();
                    iterations++;
                } while (NumberOfNeighbors(checkPos, takenPositions) > 1 && iterations < 100);

                if (iterations >= 50)
                    print("error: could not create with fewer neighbors than : " +
                          NumberOfNeighbors(checkPos, takenPositions));
            }

            //finalize position
            rooms[(int) checkPos.x + gridSizeX, (int) checkPos.y + gridSizeY] = new Room(checkPos, 0);
            takenPositions.Insert(0, checkPos);
        }
    }

    private Vector2 NewPosition()
    {
        int x, y;
        var checkingPos = Vector2.zero;
        do
        {
            var index = Mathf.RoundToInt(Random.value * (takenPositions.Count - 1)); // pick a random room
            x = (int) takenPositions[index].x; //capture its x, y position
            y = (int) takenPositions[index].y;
            var UpDown = Random.value < 0.5f; //randomly pick wether to look on hor or vert axis
            var positive = Random.value < 0.5f; //pick whether to be positive or negative on that axis
            if (UpDown)
            {
                //find the position bnased on the above bools
                if (positive)
                    y += 1;
                else
                    y -= 1;
            }
            else
            {
                if (positive)
                    x += 1;
                else
                    x -= 1;
            }

            checkingPos = new Vector2(x, y);
        } while (takenPositions.Contains(checkingPos) || x >= gridSizeX || x < -gridSizeX || y >= gridSizeY ||
                 y < -gridSizeY); //make sure the position is valid

        return checkingPos;
    }

    private Vector2 SelectiveNewPosition()
    {
        // method differs from the above in the two commented ways

        int x, y, inc = 0;
        var checkingPos = Vector2.zero;
        do
        {
            int index;
            do
            {
                //instead of getting a room to find an adject empty space, we start with one that only 
                //has one neighbor. This will make it more likely that it returns a room that branches out
                index = Mathf.RoundToInt(Random.value * (takenPositions.Count - 1));
                inc++;
            } while (NumberOfNeighbors(takenPositions[index], takenPositions) > 1 && inc < 100);

            x = (int) takenPositions[index].x;
            y = (int) takenPositions[index].y;
            var upDown = (Random.value < 0.5f);
            var positive = (Random.value < 0.5f);
            if (upDown)
            {
                if (positive)
                    y += 1;
                else
                    y -= 1;
            }
            else
            {
                if (positive)
                    x += 1;
                else
                    x -= 1;
            }

            checkingPos = new Vector2(x, y);
        } while (takenPositions.Contains(checkingPos) || x >= gridSizeX || x < -gridSizeX || y >= gridSizeY ||
                 y < -gridSizeY);

        if (inc >= 100)
        {
            // break loop if it takes too long: this loop isnt garuanteed to find solution, which is fine for this
            print("Error: could not find position with only one neighbor");
        }

        return checkingPos;
    }

    private int NumberOfNeighbors(Vector2 checkingPos, List<Vector2> usedPositions)
    {
        return new[] {Vector2.right, Vector2.left, Vector2.down, Vector2.up}.Count(direction => usedPositions.Contains(checkingPos + direction));
    }

    private void DrawMap()
    {
        foreach (var room in rooms)
        {
            if (room == null)
                continue;
            var drawPos = room.GridPos;
            drawPos.x *= room.RoomSize.x; // домножаем на размеры комнаты
            drawPos.y *= room.RoomSize.y;
            
            var selector = _roomSelector;
            var newRoom = Instantiate(selector.PickRoom(room), drawPos, quaternion.identity);
            var roomStats = newRoom.GetComponent<RoomPrefab>();
            roomStats.Room = room;
            newRoom.transform.parent = mapRoot;
        }
    }

    private void SetRoomDoors()
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
            }
        }
    }
}