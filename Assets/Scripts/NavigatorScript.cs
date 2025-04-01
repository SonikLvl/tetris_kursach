using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NavigatorScript : MonoBehaviour
{


    public void EndlessMode (){
        SceneManager.LoadScene("main");
    }

    public void Levels(Button button){
        

        PlayerPrefs.SetString("LevelToActivate", button.name);
        PlayerPrefs.Save();

        SceneManager.LoadScene("test");
        
    }
}
