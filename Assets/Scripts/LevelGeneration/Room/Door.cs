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
    public bool IsActive { get; set; }
    [SerializeField] private Vector2 TeleportDistance;

    private Vector3 MovePlayer(Vector3 startPos)
    {
        startPos = DoorPosition switch
        {
            Position.Top => new Vector3(startPos.x, transform.position.y + TeleportDistance.y, startPos.z),
            Position.Right => new Vector3(transform.position.x + TeleportDistance.x, startPos.y, startPos.z),
            Position.Bottom => new Vector3(startPos.x, transform.position.y - TeleportDistance.y + 1.4f, startPos.z),
            Position.Left => new Vector3(transform.position.x - TeleportDistance.x, startPos.y, startPos.z),
            _ => throw new ArgumentException()
        };

        return startPos;
    }
    
    IEnumerator TeleportPlayer()
    {
        yield return new WaitForSeconds(1.3f);
        var player = FindObjectOfType<PlayerController>();
        player.transform.position = MovePlayer(player.transform.position);
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        // Debug.Log(IsActive);
        if (col.gameObject.CompareTag("Player") && IsActive)
        {
            EventManager.SendPlayCrossfade();
            StartCoroutine(TeleportPlayer());
        }
    }
}
