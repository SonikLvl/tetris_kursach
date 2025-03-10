using UnityEngine;
using System.Collections.Generic;
using System.Linq;


public class GhostScript : MonoBehaviour
{
    public static int height = 20;
    public static int width = 10;
    private static Transform[,] gridGhost = new Transform[width, height];
    List<Transform> ghostList = new List<Transform>();
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (Transform children in transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);

            gridGhost[roundedX, roundedY] = children;

            Debug.Log($"Дочірній об'єкт {children.name} має координати: ({roundedX}, {roundedY})");
            ghostList.Add(gridGhost[roundedX, roundedY]);

            
        }
        Debug.Log(ghostList.Count);
        
    }

    // Update is called once per frame
    void Update()
    {
        TetrisBlock tetrisBlock = FindObjectOfType<TetrisBlock>();

        if (tetrisBlock != null)
        {
            List<Transform> blocktList = tetrisBlock.blocktList; 

            
            if (blocktList.All(ghostList.Contains))
            {
                Debug.Log("blocktList є підмножиною ghostList.");
            }
        }
        
    }

    
}
