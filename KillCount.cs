using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillCount : MonoBehaviour
{
    public Text CounterText;
    //public static Text CounterText;
    //public static int kills = 0;

    void Start()
    {
        if (PlayerPrefs.GetInt("killsTotal") == null || PlayerPrefs.GetInt("killsTotal") == 0)
            PlayerPrefs.SetInt("killsTotal", 0);

        PlayerPrefs.SetInt("killsSession", 0);

        if (CounterText == null)
            CounterText = GameObject.Find("DisplayCanvas/CoinText").GetComponent<Text>();
    }

    // Update is called once per frame
    //void updateKills()
    public void killConfirmed()
    {
        PlayerPrefs.SetInt("killsTotal", PlayerPrefs.GetInt("killsTotal") + 1);
        PlayerPrefs.SetInt("killsSession", PlayerPrefs.GetInt("killsSession") + 1);

        CounterText.text = "Total Coins:  " + PlayerPrefs.GetInt("coinsTotal") + "\nCoins This Session: " + PlayerPrefs.GetInt("coinsSession") //CoinHandler.coins 
            + "\nTotal Enemies Killed: " + PlayerPrefs.GetInt("killsTotal") + "\nEnemies Killed This Session: " + PlayerPrefs.GetInt("killsSession");

        //print(PlayerPrefs.GetInt("killsTotal")); //set the total variable to the previously saved value
        //print(PlayerPrefs.GetInt("killsSession")); //set the total variable to the previously saved value
        PlayerPrefs.Save();
    }
}
