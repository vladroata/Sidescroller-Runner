using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndZoneScript : StationaryPlatform
{
    public void OnTriggerEnter(Collider collider)
    {
        //TODO

        if (collider.gameObject.tag == "Player")
        {
            Renderer rend = this.GetComponent<Renderer>();

            //print("OnTriggerEnter: " + gameObject.name + "\t\t" + collider.gameObject.tag);
            print("NANEAE");
            //Material winnerMaterial = Resources.Load("/Materials/EndZoneReached", typeof(Material)) as Material;
            //D:\Documents\0 - Fall 2020 Game Design\A2 - Cassandra Horne\Assets\Materials

            if (rend != null)
            {
                //this.GetComponent<Renderer>().material = winnerMaterial;
                //rend.sharedMaterial = winnerMaterial;
                //rend.material = winnerMaterial;
                rend.material.color = Color.green;
            }/**/
        }
    }
}
