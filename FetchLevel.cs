using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FetchLevel : MonoBehaviour
{
    private Text currentText;
    private void Awake()
    {
        currentText = GetComponent<Text>();

        if(currentText != null)
        {
            if(SceneManager.GetActiveScene().name == "SampleScene")
            {
                currentText.text = "Level: 1";
            }
            else if(SceneManager.GetActiveScene().name == "Level2")
            {
                currentText.text = "Level: 2";
            }
            else if (SceneManager.GetActiveScene().name == "Level3")
            {
                currentText.text = "Level: 3";
            }
        }
        
    }



}
