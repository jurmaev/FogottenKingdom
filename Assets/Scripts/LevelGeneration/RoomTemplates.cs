using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour
{
    [SerializeField] private GameObject[] leftRooms;
    [SerializeField] private GameObject[] rightRooms;
    [SerializeField] private GameObject[] topRooms;
    [SerializeField] private GameObject[] bottomRooms;

    public GameObject[] LeftRooms => leftRooms;
    public GameObject[] RightRooms => rightRooms;
    public GameObject[] TopRooms => topRooms;
    public GameObject[] BottomRooms => bottomRooms;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
