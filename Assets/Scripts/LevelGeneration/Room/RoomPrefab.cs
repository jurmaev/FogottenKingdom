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
    [SerializeField] private GameObject chestPrefab;
    [SerializeField] private GameObject portal;
    private List<GameObject> enemies;
    private int numberOfEnemies;
    private List<GameObject> doors;
    private bool enemiesDefeated;
    private bool obstaclesSpawned;

    private void Start()
    {
        doors = new List<GameObject>();
        enemies = new List<GameObject>();
        PaintFloor();
        PaintDoors(false);
        if (Room.Type == Room.RoomType.EntryRoom)
        {
            ActivateDoors();
            GetComponentInChildren<ObstaclesPrefab>().GenerateLights();
            obstaclesSpawned = true;
        }
        else if (Room.Type != Room.RoomType.NormalRoom) ActivateDoors();
    }

    private void PaintFloor()
    {
        for (var x = -(int) Room.RoomSize.x / 2 + 2; x < (int) Room.RoomSize.x / 2 - 2; x++)
        for (var y = -(int) Room.RoomSize.y / 2 + 2; y < (int) Room.RoomSize.y / 2 - 2; y++)
            floorTilemap.SetTile(new Vector3Int(x, y, 0), tiles[Random.Range(0, tiles.Count)]);
    }

    private void PaintDoors(bool isOpen)
    {
        var paintIndex = isOpen ? 0 : 1;
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
        doors.Add(newDoor);
    }

    private void ActivateDoors()
    {
        PaintDoors(true);
        foreach (var door in doors)
            door.GetComponent<Door>().IsActive = true;
    }

    public void DeactivateDoors()
    {
        PaintDoors(false);
        foreach (var door in doors)
            door.GetComponent<Door>().IsActive = false;
    }

    private void RemoveEnemy(GameObject enemy)
    {
        if (enemies.Count != 0 && numberOfEnemies == 0) numberOfEnemies = enemies.Count;
        if (enemies.Count != 0) enemies.Remove(enemy);

        if (enemies.Count == 0)
        {
            var chest = Instantiate(chestPrefab, enemy.transform.position, Quaternion.identity);
            chest.GetComponent<Chest>().CoinMultiplier = numberOfEnemies;
            chest.GetComponent<Chest>().RoomType = Room.Type;
            enemiesDefeated = true;
            ActivateDoors();
            EventManager.OnEnemyDeath.RemoveListener(RemoveEnemy);
        }
    }

    // private void OnTriggerExit2D(Collider2D other)
    // {
    //     if (other.gameObject.CompareTag("Player"))
    //     {
    //         Debug.Log("Player left the room");
    //         var magic = Physics2D.OverlapBoxAll(new Vector2(transform.position.x, transform.position.y), Room.RoomSize / 2,
    //             0).Where(col => col.gameObject.CompareTag("Magic"));
    //         Debug.Log(string.Join(" ", magic));
    //         Debug.Log(magic.Count());
    //         Debug.Log(activeMagic);
    //         foreach(var spell in activeMagic) Destroy(spell.gameObject);
    //     }
    // }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
            enemies.Add(col.gameObject);

        if (col.gameObject.CompareTag("Player"))
        {
            var obstaclesPrefab = GetComponentInChildren<ObstaclesPrefab>();
            if (obstaclesPrefab != null && Room.Type != Room.RoomType.EntryRoom && !obstaclesSpawned)
            {
                obstaclesPrefab.SpawnEnemies();
                obstaclesPrefab.GenerateLights();
                obstaclesSpawned = true;
            }

            if (Room.Type == Room.RoomType.NormalRoom && !enemiesDefeated)
            {
                DeactivateDoors();
                EventManager.OnEnemyDeath.AddListener(RemoveEnemy);
            }

            if (Room.Type == Room.RoomType.TreasureRoom)
            {
                var chest = Instantiate(chestPrefab, transform.position, Quaternion.identity);
                chest.GetComponent<Chest>().RoomType = Room.Type;
            }

            if (Room.Type == Room.RoomType.BossRoom) Instantiate(portal, transform.position, quaternion.identity);

            EventManager.SendCameraPosChanged(Room.GridPos);
            EventManager.SendActiveRoomChanged(Room.GridPos);
        }
    }
}