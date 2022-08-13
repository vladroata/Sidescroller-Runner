using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScript : MonoBehaviour
{
    float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer += Time.deltaTime;
        if (timer >= 2.5) {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other) {
        print(other.tag);
        if (other.tag == "MovingEnemy" || other.tag == "BouncyProjectileEnemy" || other.tag == "ExplodingEnemy") {
            Destroy(other.gameObject);
            //KillCount.killConfirmed();
            //KillCount.kills += 1;

            //KillCount.kills += 1;
            //KillCount.killConfirmed(KillCount.kills);
            KillCount KillCount = GameObject.Find("DisplayCanvas/CoinScoreSystem").GetComponent(typeof(KillCount)) as KillCount;
            KillCount.killConfirmed();

        }
    }
}
