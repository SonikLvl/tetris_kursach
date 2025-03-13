using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnerScript : MonoBehaviour
{
    public GameObject[] Tetrominoes;
    private GameObject PreviewTetromino;
    private GameObject nextTetrominoPrefab;
    private int currentIndex = 0;

    void Start()
    {
        Debug.Log(SceneManager.GetActiveScene().name);
        if (Tetrominoes.Length == 0)
        {
            Debug.LogError("Tetrominoes list is empty!");
            return;
        }

        if (SceneManager.GetActiveScene().name == "main")
        {
            nextTetrominoPrefab = Tetrominoes[Random.Range(0, Tetrominoes.Length)];
            NewTetromino();
            Preview();
        }
        else if (SceneManager.GetActiveScene().name == "test")
        {
            nextTetrominoPrefab = Tetrominoes[currentIndex++];
            NewTetromino2();
            Preview2();
        }
    }

    public void Preview()
    {
        if (PreviewTetromino != null)
        {
            Destroy(PreviewTetromino);
        }
        
        nextTetrominoPrefab = Tetrominoes[Random.Range(0, Tetrominoes.Length)];

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
    public void Preview2()
    {
        if (PreviewTetromino != null)
        {
            Destroy(PreviewTetromino);
        }

        if (currentIndex >= Tetrominoes.Length)
        {
            nextTetrominoPrefab = null;
            return;
        }

        nextTetrominoPrefab = Tetrominoes[currentIndex++];

        Vector3 previewPosition = nextTetrominoPrefab.name switch
        {
            "Square easyblock" => new Vector3(14.5f, 17, 0),
            "l easyblock" => new Vector3(15.5f, 16.5f, 0),
            "L_rotate easyblock" => new Vector3(15, 17, 0),
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

        Preview2();
    }

    void GameOver()
    {
        Debug.Log("Game Over!");
    }
}
