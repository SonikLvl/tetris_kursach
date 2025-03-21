using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreScript : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text levelText;

    public int score = 0;
    public int level = 1;
    public int linesCleared = 0;



    private int[] pointsPerLine = { 0, 40, 100, 300, 1200 }; 

    public void AddScore(int lines)
    {
        if (lines > 0 && lines <= 4)
        {
            score += pointsPerLine[lines] * level;
            linesCleared += lines;
            
            
            scoreText.text = score.ToString("0");

            if (linesCleared >= level * 10)
            {
                level++;
                if (levelText != null)
                {
                    levelText.text = level.ToString("0");
                }
                else
                {
                    Debug.LogError("levelText is not assigned!");
                
            }
            }
        }
    }
}
