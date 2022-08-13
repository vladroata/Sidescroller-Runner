using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    Material m_Material;
    float bulletTimer = 0;
    public GameObject player;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
        }
        m_Material = GetComponent<Renderer>().material;
        m_Material.color = Color.yellow;
        gameObject.transform.LookAt(player.transform.position); //point toward the player
        rb.AddForce(transform.forward * 300);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        bulletTimer += Time.deltaTime;
        if (bulletTimer >= 5) { //destroy bullet after 5 seconds. Originally had the idea to have the enemy handle the bullet deletion but this makes much more sense.
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //print("DAMAGE!");
            other.gameObject.GetComponent<PlayerControllerAnimation>().playerCurrentHealth -= 10; //deal damage on collision
        }
    }
}
