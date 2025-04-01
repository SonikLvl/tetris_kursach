using UnityEngine;
using System.Collections.Generic;

public class GhostScript : MonoBehaviour
{
    public static int height = 20;
    public static int width = 10;
    private static Transform[,] gridGhost = new Transform[width, height];
    private List<Transform> ghostList = new List<Transform>();

    public bool allMatch { get; private set; }

    void Start()
    {

        foreach (Transform children in transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);

            if (roundedX >= 0 && roundedX < width && roundedY >= 0 && roundedY < height)
            {
                gridGhost[roundedX, roundedY] = children;
                ghostList.Add(children);
                
            }
            else
            {
                Debug.LogWarning($"Ghost {children.name} має невірні координати: ({roundedX}, {roundedY})");
            }
        }
        
    }

    void Update()
    {
        Checking();
    }

    public void Checking(){
        TetrisBlock tetrisBlock = FindObjectOfType<TetrisBlock>();

        if (tetrisBlock != null)
        {
            List<Transform> blocktList = tetrisBlock.blocktList;

            if (blocktList != null && blocktList.Count > 0)
            {

                allMatch = true;
                foreach (var block in blocktList)
                {
                    if (block == null) 
                    {
                        allMatch = false;
                        break;
                    }

                    bool match = false;
                    foreach (var ghost in ghostList)
                    {
                        if (ghost == null) 
                        {
                            continue; 
                        }

                        if (Vector3.Distance(block.position, ghost.position) < 0.1f)
                        {
                            match = true;
                            break;
                        }
                    }

                    if (!match)
                    {
                        allMatch = false;
                        break;
                    }
                }

                if (allMatch)
                {
                    Debug.Log("blocktList є підмножиною ghostList (за координатами).");
                }
                else
                {
                    Debug.Log("blocktList НЕ є підмножиною ghostList.");
                }
            }
        }
    }
}