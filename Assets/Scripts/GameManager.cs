//Скрипт для навігаціх по сценам гри і між рівнями

using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;


public class GameManager : MonoBehaviour
{
    public List<GameObject> final = new List<GameObject>();

    public AudioSource restartSound;
    
     
    void Start()
    {
        GameObject restartSoundObject = GameObject.Find("RestartSound");
        if (restartSoundObject != null){
            restartSound = restartSoundObject.GetComponent<AudioSource>();
        }
        
        LoadCurrentLevel();
        
    }

    void Update()
    {
    if (StopGameManager.IsGamePausedGlobally)
    {
        return; 
    }
            ResetScene();
            Quit();
        
    }

    private void LoadCurrentLevel()
    {
        string levelToActivate = PlayerPrefs.GetString("LevelToActivate", "1"); 
        
        GameObject levelsHolder = GameObject.Find("Levels");
        if (levelsHolder == null) return;

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
            if (SceneManager.GetActiveScene().name == "main"){
                int scoreNum = FindObjectOfType<ScoreScript>().score; 
                FindObjectOfType<SaveToJson>().SendScoreToDatabase(scoreNum);
            }
            restartSound.Play();
            Invoke(nameof(Reset),restartSound.clip.length - 0.6f);
        }
    }

    public void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            TetrisBlock.blocktList.Clear();
            QuitScene();
            if (SceneManager.GetActiveScene().name == "main"){
                int scoreNum = FindObjectOfType<ScoreScript>().score; 
                FindObjectOfType<SaveToJson>().SendScoreToDatabase(scoreNum);
            }
        }
    }
    public void QuitScene(){
        SceneManager.LoadScene("menu");
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