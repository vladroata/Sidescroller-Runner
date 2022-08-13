using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchCoin : MonoBehaviour
{
   // public GameObject collectibleCounterLevel;
   // public GameObject collectibleCounterGameTotal;

    private void OnTriggerEnter(Collider other)
    {
        //  if (GetComponent<Collider>().gameObject.tag == "Player")
        //  {
        //CoinHandler.coins += 1;


        CoinHandler CoinHandler = GameObject.Find("DisplayCanvas/CoinScoreSystem").GetComponent(typeof(CoinHandler)) as CoinHandler;

        CoinHandler.updateCoins();
        Destroy(gameObject);
      //s  }
    }
    //GameObject.Find("CoinScoreSystem").GetComponent< "CoinHandler" >().updateCoins(CoinHandler.coins);

}
