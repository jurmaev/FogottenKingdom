using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float speed;
    private Vector2 currentPos;
    private Vector3 velocity = Vector3.zero;

    private void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position,
            new Vector3(currentPos.x, currentPos.y, transform.position.z), ref velocity, speed * Time.deltaTime);
        
    }

    public void MoveToPos(Vector2 newPos)
    {
        currentPos = newPos;
    }
}
