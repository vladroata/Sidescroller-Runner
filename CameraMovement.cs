using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // Public Variables
    public Vector3 offset;
    public float camspeed;
    public static GameObject player;
    public GameObject stationaryPlatform;
    public GameObject movingPlatform;
    public GameObject rotatingPlatform;

    // Private Variables


    // Start is called before the first frame update
    void Start()
    {
        //player = GameObject.Find("Player");
        offset += new Vector3(0, 0, transform.position.z); // Don't want to change the z value of camera so we add it to the offset
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null){
            // Keep the camera locked on to the character, except when on left side of screen
            if(player.gameObject.transform.position.x > transform.position.x)
            transform.position = Vector3.MoveTowards(transform.position, player.gameObject.transform.position+offset, camspeed);
            // keep tracking player y, no matter where the player is
            Vector3 playerytrack = new Vector3(transform.position.x, player.gameObject.transform.position.y, 0);
            transform.position = Vector3.MoveTowards(transform.position, playerytrack+offset, camspeed);
        }
        
    }
}
