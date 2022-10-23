using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoomSpawner : MonoBehaviour
{
    enum Directions
    {
        Top,
        Right,
        Left,
        Bottom
    }

    [SerializeField] private Directions openingDirection;
    private Transform grid;

    private RoomTemplates _roomTemplates;
    private bool spawned;

    void Start()
    {
        _roomTemplates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        grid = GameObject.FindGameObjectWithTag("Grid").GetComponent<Transform>();
        Invoke(nameof(SpawnRoom), 0.1f);
    }

    private void SpawnRoom()
    {
        if (spawned) return;
        GameObject roomToSpawn;
        switch (openingDirection)
        {
            case Directions.Bottom:
                roomToSpawn = ChooseRoom(_roomTemplates.BottomRooms);
                InstantiateRoom(roomToSpawn);
                break;
            case Directions.Top:
                roomToSpawn = ChooseRoom(_roomTemplates.TopRooms);
                InstantiateRoom(roomToSpawn);
                break;
            case Directions.Left:
                roomToSpawn = ChooseRoom(_roomTemplates.LeftRooms);
                InstantiateRoom(roomToSpawn);
                break;
            case Directions.Right:
                roomToSpawn = ChooseRoom(_roomTemplates.RightRooms);
                InstantiateRoom(roomToSpawn);
                break;
            default:
                throw new ArgumentException();
        }
        spawned = true;
    }

    private GameObject ChooseRoom(GameObject[] rooms)
    {
        var randIndex = Random.Range(0, rooms.Length);
        return rooms[randIndex];
    }

    private void InstantiateRoom(GameObject roomToSpawn)
    {
        var spawnedRoom = Instantiate(roomToSpawn, transform.position, roomToSpawn.transform.rotation);
        spawnedRoom.transform.parent = grid;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("SpawnPoint"))
            Destroy(gameObject);
    }
}