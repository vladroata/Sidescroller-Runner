using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHead : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider col) {
        if(col.gameObject.tag == "Player" && col.gameObject.transform.position.y > this.gameObject.transform.position.y){
            Destroy(transform.parent.gameObject);
            //KillCount.killConfirmed();
            //KillCount.kills += 1;
            
            //KillCount.kills += 1;
            KillCount KillCount = GameObject.Find("DisplayCanvas/CoinScoreSystem").GetComponent(typeof(KillCount)) as KillCount;
            KillCount.killConfirmed();
            //KillCount.killConfirmed(KillCount.kills);
        }

    }
}
