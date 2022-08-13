using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZones : MonoBehaviour
{
    [SerializeField]
    private Transform fire;
    
    public float offSet = 5f;

    private float fireCounter = 0f;
    private Vector3 firePointer;

    public List<GameObject> fires = new List<GameObject>();

    private void Start()
    {
        firePointer = new Vector3(transform.position.x, transform.position.y, transform.position.z - transform.localScale.z);

        while (fireCounter < transform.localScale.x/2)
        {
            var currentPlatformLeft = Instantiate(fire, new Vector3(transform.position.x -fireCounter, firePointer.y, firePointer.z), Quaternion.identity); //create the platform
            currentPlatformLeft.transform.SetParent(transform);
            var currentPlatformRight = Instantiate(fire, firePointer, Quaternion.identity); //create the platform
            currentPlatformRight.transform.SetParent(transform);

            firePointer += new Vector3(offSet, 0f, 0f);
            fireCounter += offSet;

            fires.Add(currentPlatformLeft.gameObject);
            fires.Add(currentPlatformRight.gameObject);
        }
    }

    private void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.tag == "Player")
        {
           // Vector3 resetPosition;
           // resetPosition.x = 0;
           // resetPosition.y = 1;
           // resetPosition.z = 0;
           // collider.gameObject.transform.position = resetPosition;

            //DOESNT WORK, look at Player Controller

        }

        //replace with flag triggered on pick up of power-up?
        if (collider.gameObject.tag == "SafeZone")
        {
            gameObject.GetComponent<Collider>().isTrigger = false;
            gameObject.GetComponent<Collider>().enabled = false;
            //collider.isTrigger = false;
        }
    }

    private void OnTriggerExit(Collider collider)
    {

        //replace with flag triggered on pick up of power-up?
        if (collider.gameObject.tag == "SafeZone")
        {
            gameObject.GetComponent<Collider>().isTrigger = true;
            gameObject.GetComponent<Collider>().enabled = true;
            //collider.isTrigger = true;
        }
    }

}
