using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomChanger : MonoBehaviour
{
    // Not used
    private Vector2 cameraChange;

    public Vector3 destination;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 GetDestination()
    {
        return destination;
    }

//    private void OnTriggerEnter2D(Collider2D collision) {
//        Debug.Log("peener");
//        if (collision.gameObject.CompareTag("Player"))
//        {
//            GameObject player = collision.gameObject;
//            collision.transform.position += playerChange;
//            Debug.Log(playerChange);
            //player.transform.position += playerChange;
//        }
//    }
  
}
