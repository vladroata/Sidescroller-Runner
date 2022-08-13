using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyEnemy : MonoBehaviour
{
    float distance;
    private float timer = 0f;
    public GameObject player;
    public GameObject bullet;
    // Start is called before the first frame update
    void Start()
    {
    }
    
    void FixedUpdate() {
        if (player == null) {
            player = GameObject.FindWithTag("Player");
        }
        
        distance = Vector3.Distance(player.transform.position, transform.position);
        timer += Time.deltaTime;
        if (distance <= 10) {
            if (timer >= 3) {
                timer = 0;
                if (player.transform.position.x < gameObject.transform.position.x) //shoot a bullet towards the left or the right depending on the player's position every few seconds.
                {
                    Instantiate(bullet, transform.position - new Vector3(1, 0, 0), Quaternion.identity);
                }
                else {
                    Instantiate(bullet, transform.position + new Vector3(1, 0, 0), Quaternion.identity);
                }
            }
        }
    }
}
