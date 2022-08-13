using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
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
            //print("DAMAGE!");
            other.gameObject.GetComponent<PlayerControllerAnimation>().playerCurrentHealth -= 10;
        }
    }
}
