using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public List<GameObject> final = new List<GameObject>();
     
    void Start()
    {

        LoadCurrentLevel();
    }

    void Update()
    {
        ResetScene();
        Quit();
    }

    private void LoadCurrentLevel()
    {
        string levelToActivate = PlayerPrefs.GetString("LevelToActivate", "1"); // Значення за замовчуванням "1"
        
        GameObject levelsHolder = GameObject.Find("Levels");
        if (levelsHolder == null) return;

        // Вимкнути всі рівні спочатку
        foreach (Transform child in levelsHolder.transform)
        {
            child.gameObject.SetActive(false);
        }


        Transform level = levelsHolder.transform.Find("Level" + levelToActivate);
        if (level != null) 
        {
            level.gameObject.SetActive(true);
        }
    }

    public void ResetScene()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            TetrisBlock.blocktList.Clear();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void Quit()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            TetrisBlock.blocktList.Clear();
            SceneManager.LoadScene("menu");
        }
    }


    public void CompleteCurrentLevel()
    {
        TetrisBlock.blocktList.Clear();
        string currentLevelStr = PlayerPrefs.GetString("LevelToActivate", "1");
        if (!int.TryParse(currentLevelStr, out int currentLevel)) 
            {
                currentLevel = 1;
            }
        int nextLevel = currentLevel + 1;
        PlayerPrefs.SetString("LevelToActivate", nextLevel.ToString());
        PlayerPrefs.Save();


        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }
}