using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    //Player
    private GameObject player;
    private PlayerControllerAnimation sc;
    private float startPos;
    private float barScale;
    void Start()
    {
        barScale = this.gameObject.transform.parent.gameObject.transform.localScale.x;
        //startPos = this.gameObject.transform.localPosition.x;
    }

    // Update is called once per frame
    void Update()
    {
        if(player == null){
            player = GameObject.Find("PlagueDoctor(Clone)");
            if(player != null)
                sc = player.gameObject.GetComponent<PlayerControllerAnimation>();
        }else{
            //this.gameObject.transform.localScale.x = (sc.playerCurrentHealth/sc.playerMaxHealth)*barScale;*
            if (sc.playerCurrentHealth != 0)
            {
                this.gameObject.transform.parent.gameObject.transform.localScale = new Vector3(((float)sc.playerCurrentHealth / sc.playerMaxHealth) * barScale, 1, 1);
            }
            else {
                this.gameObject.transform.parent.gameObject.transform.localScale = new Vector3(0 * barScale, 1, 1);
            }
        }
    }
}
