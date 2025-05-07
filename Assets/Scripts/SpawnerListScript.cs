using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonGridGenerator : MonoBehaviour
{
    public SpawnerScript blockList; 
    public GameObject buttonPrefab; 
    public Transform buttonParent; 
    public Vector2 buttonSize = new Vector2(100, 100); 
    public Vector2 spacing = new Vector2(10, 10);
    public List<Sprite> sprites;
    public List<Sprite> selectedSprites;
    public List<ButtonIndexHolder> buttonIndexHolders = new List<ButtonIndexHolder>();

    private Dictionary<string, int> blockToSpriteIndex = new Dictionary<string, int>
    {
        { "l block ", 0 },
        { "L_rotate block", 1 },
        { "Square block", 2 },
        { "S block ", 3 },
        { "T block", 4 },
        { "L block", 5 },
        { "Z block ", 6 },

        { "l deleteblock ", 7 },
        { "L_rotate deleteblock", 8 },
        { "Square deleteblock", 9 },
        { "S deleteblock", 10 },
        { "T deleteblock", 11 },
        { "L deleteblock", 12 },
        { "Z deleteblock ", 13 },

        { "l easyblock ", 14 },
        { "L_rotate easyblock", 15 },
        { "Square easyblock", 16 },
        { "S easyblock", 17 },
        { "T easyblock", 18 },
        { "L easyBlock", 19 },
        { "Z easyblock ", 20 },

        { "l crossBlock ", 21 },
        { "L_rotate crossBlock", 22 },
        { "Square crossBlock", 23 },
        { "S crossBlock", 24 },
        { "T crossBlock", 25 },
        { "L crossBlock", 26 },
        { "Z crossBlock ", 27 }
        

    };
    void Start()
    {
        GenerateButtons();
    }

    void Update()
    {
        if (StopGameManager.IsGamePausedGlobally)
    {
        return; 
    }
        SelectButton();
        
    }

    public void GenerateButtons()
    {
        for (int i = 0; i < blockList.Tetrominoes.Count; i++)
        {
            GameObject buttonObj = Instantiate(buttonPrefab, buttonParent);
            RectTransform buttonRect = buttonObj.GetComponent<RectTransform>();


            float x = (i % 3) * (buttonSize.x + spacing.x);
            float y = -(i / 3) * (buttonSize.y + spacing.y);
            buttonRect.anchoredPosition = new Vector2(x, y);

            buttonRect.sizeDelta = buttonSize;


            ButtonIndexHolder indexHolder = buttonObj.AddComponent<ButtonIndexHolder>();
            indexHolder.Index = i;


            Image buttonImage = buttonObj.GetComponent<Image>();
            if (buttonImage != null && sprites[i] != null)
            {
                string blockName = blockList.Tetrominoes[i].name;

                if (blockToSpriteIndex.ContainsKey(blockName))
                {
                    buttonImage.sprite = sprites[blockToSpriteIndex[blockName]];
                }
                else
                {
                    buttonImage.sprite = sprites[28]; 
                }

                indexHolder.DefaultSprite = buttonImage.sprite;
            }

            buttonIndexHolders.Add(indexHolder); 
        }
    }

    public void SelectButton()
    {
        for (int i = 0; i < buttonIndexHolders.Count; i++)
        {
            ButtonIndexHolder indexHolder = buttonIndexHolders[i];
            Image buttonImage = indexHolder.GetComponent<Image>();

            if (buttonImage != null)
            {

                if (blockList.currentIndex == indexHolder.Index)
                {
                    
                    string blockName = blockList.Tetrominoes[i].name;

                    if (blockToSpriteIndex.ContainsKey(blockName))
                    {
                        buttonImage.sprite = selectedSprites[blockToSpriteIndex[blockName]];
                    }
                    else
                    {
                        buttonImage.sprite = selectedSprites[28]; 
                    }

                    if (Input.GetKeyDown(KeyCode.Space) && blockList.isActiveBlock == false)
                    {

                        buttonIndexHolders.RemoveAt(i);
                        Destroy(indexHolder.gameObject);
                        UpdateButtonPositions(i);
                        blockList.isActiveBlock = true;
                        blockList.choosen = true;
                        blockList.NewTetromino2();
                        break;
                    }
                }
                else
                {
                    buttonImage.sprite = indexHolder.DefaultSprite;
                }
            }
        }
    }


    public  void UpdateButtonPositions(int startIndex)
    {
        if (startIndex < 0 || startIndex >= buttonIndexHolders.Count)
            return;
        for (int i = startIndex; i < buttonIndexHolders.Count; i++)
        {
            ButtonIndexHolder indexHolder = buttonIndexHolders[i];
            RectTransform buttonRect = indexHolder.GetComponent<RectTransform>();
            
            indexHolder.Index = i;

            float x = (i % 3) * (buttonSize.x + spacing.x);
            float y = -(i / 3) * (buttonSize.y + spacing.y);
            buttonRect.anchoredPosition = new Vector2(x, y);
        }
    }
}