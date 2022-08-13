using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldEnemyHead : MonoBehaviour
{
    
    int active;
    int damageImmune = 0;
    float immunityTimer = 0;
    int immunityTimerFlag = 0;

    // Start is called before the first frame update
    void Start()
    {
        var parentScript = transform.parent.GetComponent<ShieldEnemy>();
        active = parentScript.shieldActive;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (immunityTimerFlag == 1) {
            immunityTimer += Time.deltaTime;
        }
        if (immunityTimer >= 2) {
            immunityTimer = 0;
            damageImmune = 0;
            immunityTimerFlag = 0;
            
        }
    }

    void OnTriggerEnter(Collider col)
    {
        
        if (col.gameObject.tag == "Player" && damageImmune == 0 && col.gameObject.transform.position.y > this.gameObject.transform.position.y) {
            if (active == 0)
            {
                Destroy(transform.parent.gameObject);
            }
            if (active == 1) {
                damageImmune = 1;
                immunityTimerFlag = 1;
                transform.parent.GetComponent<ShieldEnemy>().shieldActive = 0;
                active = 0;
                
            }
            
        }
    }
}
