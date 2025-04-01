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

    
    

    void Start()
    {
        if (Tetrominoes.Count == 0)
        {
            Debug.LogError("Tetrominoes list is empty!");
            return;
        }

        if (SceneManager.GetActiveScene().name == "main")
        {
            nextTetrominoPrefab = Tetrominoes[Random.Range(0, Tetrominoes.Count)];
            NewTetromino();
            Preview();
        }

    }
    void Update()
    {
        if (SceneManager.GetActiveScene().name != "main"){
            ChooseBlock();
            if  (Tetrominoes.Count == 0 && !isActiveBlock){
                
                GameOver();
        }
        }
    }

    public void ChooseBlock()
    {
        if (!choosen)
        {
            if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.Z) && !isActiveBlock)
            {
                currentIndex--;
                if (currentIndex < 0)
                {
                    currentIndex = Tetrominoes.Count - 1;
                }
            }
            else if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.X) && !isActiveBlock)
            {
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