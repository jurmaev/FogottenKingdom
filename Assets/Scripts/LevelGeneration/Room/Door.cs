using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public enum Position
    {
        Top, 
        Right, 
        Bottom,
        Left
    }
    public Position DoorPosition { get; set; }
    public bool isActive { get; set; }
    public RoomPrefab currentRoom;
    [SerializeField] private float TeleportDistance;

    private Vector3 MovePlayer(Vector3 startPos)
    {
        startPos = DoorPosition switch
        {
            Position.Top => new Vector3(startPos.x, transform.position.y + TeleportDistance, startPos.z),
            Position.Right => new Vector3(transform.position.x + TeleportDistance, startPos.y, startPos.z),
            Position.Bottom => new Vector3(startPos.x, transform.position.y - TeleportDistance, startPos.z),
            Position.Left => new Vector3(transform.position.x - TeleportDistance, startPos.y, startPos.z),
            _ => throw new ArgumentException()
        };

        return startPos;
    }
    
    IEnumerator TeleportPlayer()
    {
        yield return new WaitForSeconds(1.3f);
        var player = FindObjectOfType<PlayerController>();
        player.transform.position = MovePlayer(player.transform.position);
        currentRoom.DeactivateDoors();
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player") && isActive)
        {
            EventManager.SendPlayCrossfade();
            StartCoroutine(TeleportPlayer());
            Debug.Log($"{DoorPosition} door is activated");
        }
            
    }
}
