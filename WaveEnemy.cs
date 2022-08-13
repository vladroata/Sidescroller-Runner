using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveEnemy : MonoBehaviour
{
    public GameObject player;
    float timer = 0f;
    float distance;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
        }
        distance = Vector3.Distance(player.transform.position, transform.position);
        

        if (distance <= 16) {
            if (player.transform.position.y < transform.position.y) //track and follow the player
            {
                transform.position += new Vector3(0, -1, 0) * Time.deltaTime;
            }
            if (player.transform.position.y > transform.position.y)
            {
                transform.position += new Vector3(0, 1, 0) * Time.deltaTime;
            }
            if (player.transform.position.x < transform.position.x)
            {
                transform.position += new Vector3(-1, 0, 0) * Time.deltaTime;
            }
            if (player.transform.position.x > transform.position.x)
            {
                transform.position += new Vector3(+1, 0, 0) * Time.deltaTime;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //print("DAMAGE!");
            other.gameObject.GetComponent<PlayerControllerAnimation>().playerCurrentHealth -= 10;
        }
    }
}
