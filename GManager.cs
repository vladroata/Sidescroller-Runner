using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GManager : MonoBehaviour
{
    bool gameHasEnded = false;

    bool IsPaused = false;

    public float restartDelay = 1f;

    public GameObject completeLevelUI;

    public GameObject completeLevelFinalUI;

    public GameObject IncompleteLevelUI;

    public GameObject pauseLevelUI;

    public void CompleteLevel()
    {
        if (gameHasEnded == false)
        {
            gameHasEnded = true;
            Time.timeScale = 0;
            if (SceneManager.GetActiveScene().name == "Level3")
            {
                completeLevelFinalUI.SetActive(true);
            }
            else
            {
                completeLevelUI.SetActive(true);
            }
            Debug.Log("GAME COMPLETE");
        }
    }

    public void InCompleteLevel()
    {
        if (gameHasEnded == false)
        {
            gameHasEnded = true;
            Time.timeScale = 0;
            IncompleteLevelUI.SetActive(true);
            Debug.Log("GAME OVER");
        }
    }

    public void Pause()
    {
        if (gameHasEnded == false)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                IsPaused = !IsPaused;

                if (IsPaused)
                {
                    Time.timeScale = 0;
                    pauseLevelUI.SetActive(true);
                    Debug.Log("PAUSED");
                }
                else
                {
                    Time.timeScale = 1;
                    pauseLevelUI.SetActive(false);
                    Debug.Log("NOT PAUSED");
                }

            }
        }


    }

    public void QuitToMainMenu()
    {
        Time.timeScale = 1;
        LevelGenerator.levelPlatformPointer = new Vector3(0, 0, 0);
        LevelGenerator.flagFirst = 0;
        SceneManager.LoadScene(2);
    }

    public void NextLevel()
    {
        if (SceneManager.GetActiveScene().name == "SampleScene" || SceneManager.GetActiveScene().name == "Level2")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            Time.timeScale = 1;
            LevelGenerator.levelPlatformPointer = new Vector3(0, 0, 0);
            LevelGenerator.flagFirst = 0;
        }
    }
    public void Restart()
    {
        if (SceneManager.GetActiveScene().name == "SampleScene" || SceneManager.GetActiveScene().name == "Level2" || SceneManager.GetActiveScene().name == "Level3")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Time.timeScale = 1;
            LevelGenerator.levelPlatformPointer = new Vector3(0, 0, 0);
            LevelGenerator.flagFirst = 0;
        }
    }

    public void Update()
    {
        Pause();
    }
}
