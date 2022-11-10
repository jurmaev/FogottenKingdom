using UnityEngine;

public class Room
{
    public string DoorPositions { get; private set; }
    public bool DoorRight { private get; set; }
    public bool DoorTop { private get; set; }
    public bool DoorBottom { private get; set; }
    public bool DoorLeft { private get; set; }
    public Vector2 GridPos { get; }
    public Vector2 RoomSize { get; } = new(32, 18);
    public int Type { get; set; }

    public Room()
    {
    }

    public Room(Vector2 gridPos, int type)
    {
        GridPos = gridPos;
        Type = type;
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