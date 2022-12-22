using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public  class EventManager : MonoBehaviour
{
    public static UnityEvent<Vector2> OnCameraPosChanged = new UnityEvent<Vector2>();
    public static UnityEvent<Vector2> OnActiveRoomChanged = new UnityEvent<Vector2>();
    public static UnityEvent OnPlayCrossfade = new UnityEvent();
    public static UnityEvent<int> OnChangeMagic = new UnityEvent<int>();
    public static UnityEvent<int, float> OnMagicStartCooldown = new UnityEvent<int, float>();
    public static UnityEvent<GameObject> OnEnemyDeath = new UnityEvent<GameObject>();
    public static UnityEvent<Artifact> OnArtifactSelection = new UnityEvent<Artifact>();

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

    public static void SendMagicStartCooldown(int magicNumber, float magicCooldownTime)
    {
        OnMagicStartCooldown.Invoke(magicNumber, magicCooldownTime);
    }

    public static void SendEnemyDeath(GameObject enemy)
    {
        OnEnemyDeath.Invoke(enemy);
    }

    public static void SendArtifactSelection(Artifact artifact)
    {
        OnArtifactSelection.Invoke(artifact);
    }
}
