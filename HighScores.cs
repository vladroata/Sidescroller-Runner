using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScores : MonoBehaviour
{
    public TMPro.TextMeshProUGUI CurrentHighscoreCoins;
    public TMPro.TextMeshProUGUI CurrentHighscoreEnemies;
    // Start is called before the first frame update
    void Start()
    {
        int coins = PlayerPrefs.GetInt("coinsTotal");
        int enemies = PlayerPrefs.GetInt("killsTotal");
        CurrentHighscoreCoins.text = coins.ToString();
        CurrentHighscoreEnemies.text = enemies.ToString();
        //CurrentHighscoreCoins.text = PlayerPrefs.GetInt("coinsTotal").toString();
        //CurrentHighscoreEnemies.text = PlayerPrefs.GetInt("enemiesTotal");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateHighScores(int coins, int enemies) { 
        CurrentHighscoreCoins.text = coins.ToString();
        //CurrentHighscoreCoins.text = PlayerPrefs.GetInt("coinsTotal").toString();
        CurrentHighscoreEnemies.text = enemies.ToString();
        //CurrentHighscoreEnemies.text = PlayerPrefs.GetInt("enemiesTotal").toString();

    }
}
