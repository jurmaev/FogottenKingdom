using System.Linq;
using UnityEngine;

public class Room
{
    public enum RoomType
    {
        EntryRoom,
        NormalRoom,
        BossRoom,
        ShopRoom,
        TreasureRoom
    }

    public string DoorPositions { get; private set; }
    public bool DoorRight { get; set; }
    public bool DoorTop { get; set; }
    public bool DoorBottom { get; set; }
    public bool DoorLeft { get; set; }
    public Vector2 GridPos { get; }
    public static Vector2 RoomSize { get; } = new(32, 18);
    public static Vector2 MinimapRoomSize { get; } = new(6, 6);
    public RoomType Type { get; set; }

    public Room(Vector2 gridPos, RoomType type)
    {
        GridPos = gridPos;
        Type = type;
    }

    public int GetDoorCount()
    {
        return new[] {DoorBottom, DoorLeft, DoorRight, DoorTop}.Count(door => door);
    }

    public void SetDoorPositions()
    {
        var doors = "";
        if (DoorTop)
            doors += "T";
        if (DoorRight)
            doors += "R";
        if (DoorBottom)
            doors += "B";
        if (DoorLeft)
            doors += "L";
        DoorPositions = doors;
    }
}