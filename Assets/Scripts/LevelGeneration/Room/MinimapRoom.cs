using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MinimapRoom : MonoBehaviour
{
    public Room Room { get; set; }
    [SerializeField] private Tilemap floorTilemap;
    [SerializeField] private Tile activeTile;
    [SerializeField] private Tile inactiveTile;
    [SerializeField] private List<GameObject> icons;

    private void Start()
    {
        PaintRoom(Room.Type == Room.RoomType.EntryRoom ? activeTile : inactiveTile);
        ChooseSprite();
        EventManager.OnActiveRoomChanged.AddListener(activeRoomPos =>
        {
            PaintRoom(activeRoomPos == Room.GridPos ? activeTile : inactiveTile);
        });
    }

    private void PaintRoom(Tile paintTile)
    {
        for (var x = -(int) Room.MinimapRoomSize.x / 2; x < (int) Room.MinimapRoomSize.x / 2; x++)
        for (var y = -(int) Room.MinimapRoomSize.y / 2; y < (int) Room.MinimapRoomSize.y / 2; y++)
            floorTilemap.SetTile(new Vector3Int(x, y, 0), paintTile);
        if (Room.DoorBottom)
            PaintDoor(-1, -(int) Room.MinimapRoomSize.y / 2 - 1, 0, -(int) Room.MinimapRoomSize.y / 2 - 1, paintTile);
        if (Room.DoorTop)
            PaintDoor(-1, (int) Room.MinimapRoomSize.y / 2, 0, (int) Room.MinimapRoomSize.y / 2, paintTile);
        if (Room.DoorLeft)
            PaintDoor(-(int) Room.MinimapRoomSize.x / 2 - 1, -1, -(int) Room.MinimapRoomSize.x / 2 - 1, 0, paintTile);
        if (Room.DoorRight)
            PaintDoor((int) Room.MinimapRoomSize.x / 2, -1, (int) Room.MinimapRoomSize.x / 2, 0, paintTile);
    }

    private void PaintDoor(int x1, int y1, int x2, int y2, Tile paintTile)
    {
        for (var i = x1; i <= x2; i++)
        for (var j = y1; j <= y2; j++)
            floorTilemap.SetTile(new Vector3Int(i, j, 0), paintTile);
    }

    private void ChooseSprite()
    {
        // Debug.Log(Room.Type);
        switch (Room.Type)
        {
            case Room.RoomType.BossRoom:
                Instantiate(icons[0], transform.position, quaternion.identity);
                break;
            case Room.RoomType.ShopRoom:
                Instantiate(icons[1], transform.position, quaternion.identity);
                break;
            case Room.RoomType.TreasureRoom:
                Instantiate(icons[2], transform.position, quaternion.identity);
                break;
            // default: break;
        }
    }
}