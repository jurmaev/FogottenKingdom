using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    public static UnityEvent<Vector2> OnCameraPosChanged = new UnityEvent<Vector2>();
    public static UnityEvent<Vector2> OnActiveRoomChanged = new UnityEvent<Vector2>();
    public static UnityEvent OnPlayCrossfade = new UnityEvent();
    public static UnityEvent<int> OnChangeMagic = new UnityEvent<int>();

    public static void SendCameraPosChanged(Vector2 newGridPos)
    {
        OnCameraPosChanged.Invoke(newGridPos);
    }

    public static void SendActiveRoomChanged(Vector2 newRoomPos)
    {
        OnActiveRoomChanged.Invoke(newRoomPos);
    }

    public static void SendPlayCrossfade()
    {
        OnPlayCrossfade.Invoke();
    }

    public static void SendChangeMagic(int magicNumber)
    {
        OnChangeMagic.Invoke(magicNumber);
    }
}
