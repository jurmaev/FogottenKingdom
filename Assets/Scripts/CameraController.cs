using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private bool isMinimapCamera;
    private Vector2 roomSize;

    private void Start()
    {
        roomSize = isMinimapCamera
            ? new Vector2(Room.MinimapRoomSize.x + 1, Room.MinimapRoomSize.y + 1)
            : Room.RoomSize;
        EventManager.OnCameraPosChanged.AddListener(MoveCamera);
    }

    private void MoveCamera(Vector2 newPos)
    {
        transform.position = new Vector3(newPos.x * roomSize.x, newPos.y * roomSize.y, transform.position.z);
    }

    private void OnDestroy()
    {
        EventManager.OnCameraPosChanged.RemoveListener(MoveCamera);
    }
}