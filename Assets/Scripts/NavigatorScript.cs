using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NavigatorScript : MonoBehaviour
{
    public AudioSource buttonAudio; 
    private bool isButtonClicked = false; 



    private void Start()
    {
        
    }

    public void EndlessMode()
    {
        if (isButtonClicked) return;
        
        isButtonClicked = true;
        buttonAudio.Play();
        Invoke(nameof(LoadEndlessMode), buttonAudio.clip.length);
    }

    public void Levels(Button button)
    {
        if (isButtonClicked) return;
        
        isButtonClicked = true;
        buttonAudio.Play();
        
        PlayerPrefs.SetString("LevelToActivate", button.name);
        PlayerPrefs.Save();
        
        Invoke(nameof(LoadLevelsScene), buttonAudio.clip.length);
    }

    private void LoadEndlessMode()
    {
        SceneManager.LoadScene("main");
    }

    private void LoadLevelsScene()
    {
        SceneManager.LoadScene("test");
    }

}