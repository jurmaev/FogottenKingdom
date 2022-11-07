using UnityEngine;

public class Room
{
    public string DoorPositions { get; private set; }
    public bool DoorRight { private get; set; }
    public bool DoorTop { private get; set; }
    public bool DoorBottom { private get; set; }
    public bool DoorLeft { private get; set; }
    public Vector2 GridPos { get; }
    private int type;

    public Room(Vector2 gridPos, int type)
    {
        GridPos = gridPos;
        this.type = type;
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