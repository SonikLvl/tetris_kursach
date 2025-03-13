using UnityEngine;

using UnityEngine;
using System.Collections.Generic;

public class NewSpawnerScript : MonoBehaviour
{
    public GameObject[] Tetrominoes;
    private GameObject PreviewTetromino;
    private GameObject nextTetrominoPrefab;

    private List<GameObject> spawnQueue = new List<GameObject>();
    private int currentIndex = 0;

    void Start()
    {
        ShuffleTetrominoes();
        nextTetrominoPrefab = spawnQueue[currentIndex++];
        NewTetromino();
        Preview();
    }

    private void ShuffleTetrominoes()
    {
        spawnQueue.Clear();
        spawnQueue.AddRange(Tetrominoes);
        for (int i = 0; i < spawnQueue.Count; i++)
        {
            int randomIndex = Random.Range(i, spawnQueue.Count);
            (spawnQueue[i], spawnQueue[randomIndex]) = (spawnQueue[randomIndex], spawnQueue[i]);
        }
        currentIndex = 0;
    }

    public void Preview()
    {
        if (PreviewTetromino != null)
        {
            Destroy(PreviewTetromino);
        }

        if (currentIndex >= spawnQueue.Count)
        {
            ShuffleTetrominoes();
        }

        nextTetrominoPrefab = spawnQueue[currentIndex++];

        Vector3 previewPosition = nextTetrominoPrefab.name switch
        {
            "Square block" => new Vector3(14.5f, 17, 0),
            "l block" => new Vector3(15.5f, 16.5f, 0),
            "L_rotate block" => new Vector3(15, 17, 0),
            _ => new Vector3(15, 16, 0),
        };

        PreviewTetromino = Instantiate(nextTetrominoPrefab, previewPosition, Quaternion.identity);

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
    }
}

