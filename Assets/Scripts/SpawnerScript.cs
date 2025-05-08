//Скрипт для реалізації спавну блоків для обох рівнів
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class SpawnerScript : MonoBehaviour
{
    public List<GameObject> Tetrominoes; 
    private GameObject PreviewTetromino; 
    private GameObject nextTetrominoPrefab; 
    public int currentIndex = 0; 
    public bool choosen = false;
    public bool isActiveBlock = false;
    public bool loser = false;

    public AudioSource loseSound;
    public AudioSource winSound;
    public AudioSource choosingSound;

    void Start()
    {

        if (SceneManager.GetActiveScene().name == "main")
        {
            nextTetrominoPrefab = Tetrominoes[Random.Range(0, Tetrominoes.Count)];
            NewTetromino();
            Preview();
        }
        GameObject loseSoundObject = GameObject.Find("LoseSound");
        if (loseSoundObject != null){
            loseSound = loseSoundObject.GetComponent<AudioSource>();
        }
        GameObject winSoundObject = GameObject.Find("WinSound");
        if (winSoundObject != null){
            winSound = winSoundObject.GetComponent<AudioSource>();
        }
        GameObject choosingSoundObject = GameObject.Find("ChoosingSound");
        if (choosingSoundObject != null){
            choosingSound = choosingSoundObject.GetComponent<AudioSource>();
        }

    }
    

    void Update()
    {
        if (StopGameManager.IsGamePausedGlobally)
    {
        return; 
    }
        
        if (SceneManager.GetActiveScene().name != "main"){
            ChooseBlock();
            if (Tetrominoes.Count == 0 && !isActiveBlock){
                Invoke(nameof(GameOver), 0.001f); 
            }
        }
        
    }

    public void ChooseBlock()
    {
        if (!choosen)
        {
            if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.Z) && !isActiveBlock)
            {
                if(Tetrominoes.Count > 1){
                    choosingSound.Play();
                }
                currentIndex--;
                if (currentIndex < 0)
                {
                    currentIndex = Tetrominoes.Count - 1;
                }
            }
            else if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.X) && !isActiveBlock)
            {
                if(Tetrominoes.Count > 1){
                    choosingSound.Play();
                }
                currentIndex++;
                if (currentIndex >= Tetrominoes.Count)
                {
                    currentIndex = 0;
                }
            }
            Debug.Log(currentIndex);
            
        }
        //Спавн переїхав у SpawnerList


    }

    public void Preview()
    {
        if (PreviewTetromino != null)
        {
            Destroy(PreviewTetromino);
        }

        nextTetrominoPrefab = Tetrominoes[Random.Range(0, Tetrominoes.Count)];

        Vector3 previewPosition = nextTetrominoPrefab.name switch
        {
            "Square block" => new Vector3(14.5f, 17, 0),
            "l block" => new Vector3(15.5f, 16.5f, 0),
            "L_rotate block" => new Vector3(15, 17, 0),
            _ => new Vector3(15, 16, 0),
        };

        PreviewTetromino = Instantiate(nextTetrominoPrefab, previewPosition, Quaternion.identity);

        if (PreviewTetromino != null)
        {
            foreach (MonoBehaviour script in PreviewTetromino.GetComponents<MonoBehaviour>())
            {
                script.enabled = false; 
            }
        }
    }
    public void NewTetromino()
    {
        if (nextTetrominoPrefab == null)
        {
            Debug.Log("No more Tetrominoes to spawn!");
            return;
        }

        GameObject newTetromino = Instantiate(nextTetrominoPrefab, transform.position, Quaternion.identity);

        foreach (MonoBehaviour script in newTetromino.GetComponents<MonoBehaviour>())
        {
            script.enabled = true; 
        }

        TetrisBlock tetrisBlock = newTetromino.GetComponent<TetrisBlock>();

        if (!tetrisBlock.ValidMove())
        {
            GameOver();
            return;
        }

        Preview(); 
    }

 
    public void NewTetromino2()
    {
        if (currentIndex >= Tetrominoes.Count)
        {
            Debug.Log("No more Tetrominoes to spawn in test mode!");
            return;
        }

        GameObject newTetromino = Instantiate(Tetrominoes[currentIndex], transform.position, Quaternion.identity);

        foreach (MonoBehaviour script in newTetromino.GetComponents<MonoBehaviour>())
        {
            script.enabled = true; 
        }

        TetrisBlock tetrisBlock = newTetromino.GetComponent<TetrisBlock>();

        choosen = false;

        Tetrominoes.RemoveAt(currentIndex);

        if (currentIndex >= Tetrominoes.Count)
        {
            currentIndex = 0;
        }
        if (!tetrisBlock.ValidMove())
        {
            GameOver();

            return;
        }
    }
    private int num = 1;
    public void GameOver()
    {
    
        if (SceneManager.GetActiveScene().name != "main"){

            List<GameObject> FinalList = FindObjectOfType<GameManager>().final;
            
            if (FindObjectOfType<GhostScript>().allMatch == true){
                if(num == 1){
                    winSound.Play();
                    num = num-1;
                }
                FinalList[0].SetActive(true);
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0)){
                    FindObjectOfType<GameManager>().CompleteCurrentLevel();
                }

            }
            else if (FindObjectOfType<GhostScript>().allMatch == false){
                if(num == 1){
                    loseSound.Play();
                    num = num-1;
                }
                FinalList[1].SetActive(true);

            }
            else{
                FinalList[0].SetActive(false);
                FinalList[1].SetActive(false);
            }

            
        }
        else{
            loseSound.Play();
            loser = true;
            int scoreNum = FindObjectOfType<ScoreScript>().score; 
            FindObjectOfType<SaveToJson>().SendScoreToDatabase(scoreNum);
            Debug.Log("End");
            
        }
           
    }
    
    
}