using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityPowerUp : MonoBehaviour
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
            PlayerControllerAnimation.PowerUp = 3;
            //PlayerControllerAnimation.powerUp3Flag = 1;
            Destroy(gameObject);
        }
        
    }
}
