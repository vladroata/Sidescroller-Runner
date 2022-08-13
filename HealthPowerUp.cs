using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPowerUp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            /*PlayerControllerAnimation.playerCurrentHealth += 20;
            if (PlayerControllerAnimation.playerCurrentHealth > 100) {
                PlayerControllerAnimation.playerCurrentHealth = 100;
            }*/
            PlayerControllerAnimation sc = other.gameObject.GetComponent<PlayerControllerAnimation>();
            sc.playerCurrentHealth += 20;
            if (sc.playerCurrentHealth > 100) {
                sc.playerCurrentHealth = 100;
            }
            Destroy(gameObject);
        }
        
    }
}
