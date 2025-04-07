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
        Debug.Log(ghostList.Count);
        
    }

    void Update()
    {

    }

    public void Checking() {

        List<Transform> blocktList = TetrisBlock.blocktList;

        if (blocktList != null && blocktList.Count > 0) {
            bool allBlocksMatchGhosts = true;
            foreach (var block in blocktList) {
                if (block == null) {
                    allBlocksMatchGhosts = false;
                    break;
                }

                bool match = false;
                foreach (var ghost in ghostList) {
                    if (ghost == null) continue;

                    if (Vector3.Distance(block.position, ghost.position) < 0.1f) {
                        match = true;
                        break;
                    }
                }

                if (!match) {
                    allBlocksMatchGhosts = false;
                    break;
                }
            }

            // 2. Перевіряємо, чи всі елементи ghostList перекриваються blocktList
            bool allGhostsCoveredByBlocks = true;
            foreach (var ghost in ghostList) {
                if (ghost == null) continue;

                bool covered = false;
                foreach (var block in blocktList) {
                    if (block == null) continue;

                    if (Vector3.Distance(ghost.position, block.position) < 0.1f) {
                        covered = true;
                        break;
                    }
                }

                if (!covered) {
                    allGhostsCoveredByBlocks = false;
                    break;
                }
            }

            // Фінальний результат:
            allMatch = allBlocksMatchGhosts && allGhostsCoveredByBlocks;

            if (allMatch) {
                Debug.Log("blocktList і ghostList повністю збігаються (двостороння перевірка).");
            } else if (allBlocksMatchGhosts) {
                Debug.Log("blocktList є підмножиною ghostList, але не всі ghost перекриті.");
            } else if (allGhostsCoveredByBlocks) {
                Debug.Log("ghostList є підмножиною blocktList, але не всі блоки збігаються.");
            } else {
                Debug.Log("Немає повного збігу між blocktList і ghostList.");
            }
        }
    }
}
