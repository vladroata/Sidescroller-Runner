using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinScript : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {

        //CoinHandler.coins += 1;
        //CoinHandler.updateCoins(CoinHandler.coins);
        CoinHandler CoinHandler = GameObject.Find("DisplayCanvas/CoinScoreSystem").GetComponent(typeof(CoinHandler)) as CoinHandler;
        CoinHandler.updateCoins();
            Destroy(gameObject);
        
    }

}
