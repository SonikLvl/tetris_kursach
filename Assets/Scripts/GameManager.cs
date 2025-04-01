using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool loser = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        string objectToActivate = PlayerPrefs.GetString("LevelToActivate");
        
        if (!string.IsNullOrEmpty(objectToActivate))
        {
            GameObject levelsHolder = GameObject.Find("Levels");
            if (levelsHolder != null)
            {
                Transform level = levelsHolder.transform.Find("Level"+objectToActivate);
                if (level != null) 
                {
                    level.gameObject.SetActive(true);
                    return;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        ResetScene();
        Quit();

    }

    public void ResetScene(){

        if (Input.GetKeyDown(KeyCode.R)){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

    }
    public void Quit(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            SceneManager.LoadScene("menu");
        }
    }

    public void GameOver()
    {
        if (SceneManager.GetActiveScene().name != "main"){
            if (FindObjectOfType<GhostScript>().allMatch == true){
                Debug.Log("You won!");
            }
            else if (FindObjectOfType<GhostScript>().allMatch == false){
                Debug.Log("You lose!");
            }
        }
        else{
            loser = true;
            Debug.Log("End");
            
        }
        
    }
}
