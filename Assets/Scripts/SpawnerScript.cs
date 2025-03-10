using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    public GameObject[] Tetrominoes;
    private GameObject PreviewTetromino;
    private GameObject nextTetrominoPrefab;

    // Start is called before the first frame update
    void Start()
{
    nextTetrominoPrefab = Tetrominoes[Random.Range(0, Tetrominoes.Length)]; // Ініціалізація
    NewTetromino();
    Preview();
}

    public void Preview()
    {
        if (PreviewTetromino != null)
        {
            Destroy(PreviewTetromino);
        }
        
        nextTetrominoPrefab = Tetrominoes[Random.Range(0, Tetrominoes.Length)];
        if (nextTetrominoPrefab.name == "Square block"){
            PreviewTetromino = Instantiate(nextTetrominoPrefab, new Vector3(14.5f, 17, 0), Quaternion.identity);
        }
        else if (nextTetrominoPrefab.name == "l block"){
            PreviewTetromino = Instantiate(nextTetrominoPrefab, new Vector3(15.5f, 16.5f, 0), Quaternion.identity);
        }
        else if (nextTetrominoPrefab.name == "L_rotate block"){
            PreviewTetromino = Instantiate(nextTetrominoPrefab, new Vector3(15, 17, 0), Quaternion.identity);
        }
        else{
            PreviewTetromino = Instantiate(nextTetrominoPrefab, new Vector3(15, 16, 0), Quaternion.identity);
        }
        
        foreach (MonoBehaviour script in PreviewTetromino.GetComponents<MonoBehaviour>())
        {
            script.enabled = false;
        }
    }

    public void NewTetromino()
    {
        if (nextTetrominoPrefab != null)
        {
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
    }

    void GameOver()
    {
        Debug.Log("Game Over!");
        return;

    }

}