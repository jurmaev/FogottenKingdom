using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private bool isMinimapCamera;

    private void Awake()
    {
        var size = isMinimapCamera
            ? new Vector2(Room.MinimapRoomSize.x + 1, Room.MinimapRoomSize.y + 1)
            : Room.RoomSize;
        EventManager.OnCameraPosChanged.AddListener(newPos =>
            transform.position = new Vector3(newPos.x * size.x, newPos.y * size.y, transform.position.z));
    }
}