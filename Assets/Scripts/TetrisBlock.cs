using UnityEngine;
using System.Collections.Generic;

public class TetrisBlock : MonoBehaviour
{
    public Vector3 rotationPoint;
    private float previousTime;
    public float fallTimeDef = 0.8f;
    public static int height = 20;
    public static int width = 10;
    private static Transform[,] grid = new Transform[width, height];

    public List<Transform> blocktList = new List<Transform>();

    public enum BlockType
    {
        classicBlock,
        easyBlock,
        differenceBlock,
        negDifferenceBlock
    }

    [SerializeField]
    public BlockType selectedBlockType;

    // Start is called before the first frame update
    void Start()
    {
        // Ініціалізація (якщо потрібно)
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    public void Movement()
    {
        float fallTime;

        if (FindObjectOfType<ScoreScript>() != null)
        {
            fallTime = Mathf.Max(0.09f, fallTimeDef * Mathf.Pow(0.9f, FindObjectOfType<ScoreScript>().level - 1));
        }
        else
        {
            fallTime = 0.8f;
        }

        if (selectedBlockType == BlockType.classicBlock)
        {
            // Рух для класичних блоків
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                transform.position += new Vector3(-1, 0, 0);
                if (!ValidMove())
                    transform.position -= new Vector3(-1, 0, 0);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                transform.position += new Vector3(1, 0, 0);
                if (!ValidMove())
                    transform.position -= new Vector3(1, 0, 0);
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);
                if (!ValidMove())
                    transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), -90);
            }

            if (Time.time - previousTime > ((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) ? fallTime / 10 : fallTime))
            {
                transform.position += new Vector3(0, -1, 0);
                if (!ValidMove())
                {
                    transform.position -= new Vector3(0, -1, 0);
                    AddToGrid();
                    CheckForLines();

                    this.enabled = false;
                    FindObjectOfType<SpawnerScript>().NewTetromino();
                }
                previousTime = Time.time;
            }
        }
        else if (selectedBlockType == BlockType.easyBlock)
        {
            // Рух для easy block
            Vector3 previousPosition = transform.position;

            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                transform.position += new Vector3(-1, 0, 0);
                if (!ValidMove())
                    transform.position -= new Vector3(-1, 0, 0);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                transform.position += new Vector3(1, 0, 0);
                if (!ValidMove())
                    transform.position -= new Vector3(1, 0, 0);
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);
                if (!ValidMove())
                    transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), -90);
            }

            if (Time.time - previousTime > ((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) ? fallTime / 10 : fallTime))
            {
                transform.position += new Vector3(0, -1, 0);
                if (!ValidMove())
                {
                    transform.position -= new Vector3(0, -1, 0);
                    AddToGrid();
                    CheckForLines();

                    this.enabled = false;
                    FindObjectOfType<SpawnerScript>().NewTetromino();
                }
                previousTime = Time.time;
            }
        }
    }

    bool HitGround()
    {
        foreach (Transform child in transform)
        {
            int roundedY = Mathf.RoundToInt(child.transform.position.y);
            if (roundedY <= -1)
                return true;
        }
        return false;
    }

    void CheckForLines()
    {
        int linesDeleted = 0;

        for (int i = height - 1; i >= 0; i--)
        {
            if (HasLine(i))
            {
                DeleteLine(i);
                RowDown(i);
                linesDeleted++;
            }
        }
        if (FindObjectOfType<ScoreScript>() != null)
        {
            FindObjectOfType<ScoreScript>().AddScore(linesDeleted);
        }
    }

    bool HasLine(int i)
    {
        for (int j = 0; j < width; j++)
        {
            if (grid[j, i] == null)
                return false;
        }

        return true;
    }

    void DeleteLine(int i)
    {
        for (int j = 0; j < width; j++)
        {
            Destroy(grid[j, i].gameObject);
            grid[j, i] = null;
        }
    }

    void RowDown(int i)
    {
        for (int y = i; y < height; y++)
        {
            for (int j = 0; j < width; j++)
            {
                if (grid[j, y] != null)
                {
                    grid[j, y - 1] = grid[j, y];
                    grid[j, y] = null;
                    grid[j, y - 1].transform.position -= new Vector3(0, 1, 0);
                }
            }
        }
    }

    void AddToGrid()
    {
        foreach (Transform children in transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);

            if (selectedBlockType == BlockType.classicBlock)
            {
                // Для класичних блоків додаємо до сітки
                grid[roundedX, roundedY] = children;
            }
            else if (selectedBlockType == BlockType.easyBlock)
            {
                // Для easy block додаємо до сітки, якщо клітинка вільна або вже містить easy block
                if (grid[roundedX, roundedY] == null || grid[roundedX, roundedY].parent.GetComponent<TetrisBlock>().selectedBlockType == BlockType.easyBlock)
                {
                    grid[roundedX, roundedY] = children;
                }
            }

            blocktList.Add(grid[roundedX, roundedY]);
        }
    }

    public bool ValidMove()
    {
        foreach (Transform children in transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);

            if (roundedX < 0 || roundedX >= width || roundedY < 0 || roundedY >= height)
            {
                return false;
            }

            if (selectedBlockType == BlockType.classicBlock && grid[roundedX, roundedY] != null)
            {
                
                return false;
            }
            else if (selectedBlockType == BlockType.easyBlock && grid[roundedX, roundedY] != null)
            {
                
                if (grid[roundedX, roundedY].parent.GetComponent<TetrisBlock>().selectedBlockType == BlockType.classicBlock)
                {
                    return false;
                }
            }
        }

        return true;
    }
}