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
    private RoomSelector roomSelector;

    private void Start()
    {
        if (numberOfRooms >= worldSize.x * 2 * worldSize.y * 2)
            numberOfRooms = Mathf.RoundToInt(worldSize.x * 2 * worldSize.y * 2);

        roomSelector = GetComponent<RoomSelector>();
        gridSizeX = Mathf.RoundToInt(worldSize.x);
        gridSizeY = Mathf.RoundToInt(worldSize.y);
        CreateMap();
        SetDoors();
        InstantiateRooms();
    }

    private void CreateMap()
    {
        rooms = new Room[gridSizeX * 2, gridSizeY * 2];
        rooms[gridSizeX, gridSizeY] = new Room(Vector2.zero, 1);
        takenPositions.Add(Vector2.zero);
        const float randomCompareStart = 0.01f, randomCompareEnd = 0.2f;
        for (var i = 0; i < numberOfRooms - 1; i++)
        {
            var lerpValue = i / ((float) numberOfRooms - 1);
            var randomCompare = Mathf.Lerp(randomCompareStart, randomCompareEnd, lerpValue);
            var checkPos = NewPosition(false);
            if (NumberOfNeighbors(checkPos, takenPositions) > 1 &&
                Random.value > randomCompare) // проверяем новую позицию
            {
                var iterations = 0;
                while (NumberOfNeighbors(checkPos, takenPositions) > 1 && iterations < 100)
                {
                    checkPos = NewPosition(true);
                    iterations++;
                }
                if (iterations >= 50)
                    print("could not create with fewer neighbors than: " +
                          NumberOfNeighbors(checkPos, takenPositions));
            }
            // итоговая позиция
            rooms[(int) checkPos.x + gridSizeX, (int) checkPos.y + gridSizeY] = new Room(checkPos, 0);
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
               y < -gridSizeY) // убеждаемся, что позиция дейстительна
        {
            var randIndex = Mathf.RoundToInt(Random.value * (takenPositions.Count - 1));
            if (isOneNeighbour)
            {
                while (NumberOfNeighbors(takenPositions[randIndex], takenPositions) > 1 && counter < 100)
                {
                    // вместо того, чтобы искать комнату без соседей, мы ищем ту, 
                    // у которой есть один сосед. Это повысит вероятность того, что вернется комната, котороая ответвляется 
                    randIndex = Mathf.RoundToInt(Random.value * (takenPositions.Count - 1));
                    counter++;
                }
                if (counter >= 100)
                    // Прервать цикл, если он занимает слишком много времени. Этот цикл не гарантирует нахождения решения, что нас устраивает
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
            drawPos.x *= room.RoomSize.x;
            drawPos.y *= room.RoomSize.y;

            var selector = roomSelector;
            var newRoom = Instantiate(selector.PickRoom(room), drawPos, quaternion.identity);
            var roomStats = newRoom.GetComponent<RoomPrefab>();
            roomStats.Room = room;
            newRoom.transform.parent = mapRoot;
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
            }
        }
    }
}