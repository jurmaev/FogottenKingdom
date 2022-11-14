using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private bool isMinimapCamera;

    private void Awake()
    {
        var size = isMinimapCamera
            ? new Vector2(Room.MinimapRoomSize.x + 2, Room.MinimapRoomSize.y + 2)
            : Room.RoomSize;
        EventManager.OnCameraPosChanged.AddListener(newPos =>
            transform.position = new Vector3(newPos.x * size.x, newPos.y * size.y, transform.position.z));
    }
}