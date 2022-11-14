using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    public static UnityEvent<Vector2> OnCameraPosChanged = new UnityEvent<Vector2>();
    public static UnityEvent<Vector2> OnActiveRoomChanged = new UnityEvent<Vector2>();

    public static void SendCameraPosChanged(Vector2 newGridPos)
    {
        OnCameraPosChanged.Invoke(newGridPos);
    }

    public static void SendActiveRoomChanged(Vector2 newRoomPos)
    {
        OnActiveRoomChanged.Invoke(newRoomPos);
    }
}
