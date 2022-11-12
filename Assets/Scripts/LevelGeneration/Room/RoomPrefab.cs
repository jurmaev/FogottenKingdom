using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class RoomPrefab : MonoBehaviour
{
    public Room Room { get; set; }
    [SerializeField] private Tilemap floorTilemap;
    [SerializeField] private List<Tile> tiles;
    [SerializeField] private Room.RoomType roomType;
    private CameraController controller;

    private void Start()
    {
        controller = Camera.main.GetComponent<CameraController>();
        roomType = Room.Type;
        PaintFloor();
    }

    private void PaintFloor()
    {
        for (var x = -(int) Room.RoomSize.x / 2 + 2; x < (int) Room.RoomSize.x / 2 - 2; x++)
        for (var y = -(int) Room.RoomSize.y / 2 + 2; y < (int) Room.RoomSize.y / 2 - 2; y++)
            floorTilemap.SetTile(new Vector3Int(x, y, 0), tiles[Random.Range(0, tiles.Count)]);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
            controller.MoveToPos(new Vector2(Room.GridPos.x * Room.RoomSize.x, Room.GridPos.y * Room.RoomSize.y));
    }
}