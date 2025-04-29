using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NavigatorScript : MonoBehaviour
{
    public AudioSource buttonAudio; 
    private bool isButtonClicked = false; 
    public bool muted = false;
    GameObject onSprite;
    GameObject offSprite;

    private void Start()
    {
        
        muted = PlayerPrefs.GetInt("Muted", 0) == 1;
        AudioListener.pause = muted; 
        onSprite = GameObject.Find("on");
        offSprite = GameObject.Find("off");
        if (muted == true){
            offSprite.SetActive(true);
            onSprite.SetActive(false);
        }
        else{
            offSprite.SetActive(false);
            onSprite.SetActive(true);
        }
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

    public void Muted()
    {
        muted = !muted;
        AudioListener.pause = muted;
        MusicBtn();
        PlayerPrefs.SetInt("Muted", muted ? 1 : 0);
        PlayerPrefs.Save();
    }
    public void MusicBtn(){
        if (muted == true){
            offSprite.SetActive(true);
            onSprite.SetActive(false);
        }
        else{
            offSprite.SetActive(false);
            onSprite.SetActive(true);
        }
    }
}