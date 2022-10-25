using UnityEngine;

public class Room
{
    public Vector2 gridPos;
    public int type;
    public bool doorTop, doorRight, doorBottom, doorLeft;
    public string doorPositions;

    public Room(Vector2 gridPos, int type)
    {
        this.gridPos = gridPos;
        this.type = type;
    }

    public void SetDoorPositions()
    {
        var doors = "";
        if (doorTop)
            doors += "T";
        if (doorRight)
            doors += "R";
        if (doorBottom)
            doors += "B";
        if (doorLeft)
            doors += "L";
        doorPositions = doors;
    }
}