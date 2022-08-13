using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPowerUp: MonoBehaviour    //All powerups follow the same structure, they apply a powerup to the player and the gameobject is deleted
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
        if (other.tag == "Player") {
            PlayerControllerAnimation.PowerUp = 1;
            Destroy(gameObject);
            //KillCount.killConfirmed();
            //KillCount.kills += 1;

            //KillCount.kills += 1;
            KillCount KillCount = GameObject.Find("DisplayCanvas/CoinScoreSystem").GetComponent(typeof(KillCount)) as KillCount;
            KillCount.killConfirmed();
            //KillCount.killConfirmed(KillCount.kills);
        }
    }
}
