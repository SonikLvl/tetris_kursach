using UnityEngine;

public class SaveToJson : MonoBehaviour
{
    public void SaveToJSON(int score){
        Score scoreObj = new Score(score);
        string ScoreData = JsonUtility.ToJson(scoreObj);
        string filePath = Application.persistentDataPath + "/ScoreData.json";
        System.IO.File.WriteAllText(filePath, ScoreData);
        Debug.Log(filePath);

    }
}
[System.Serializable]
public class Score {
    public int score;

    public Score(int score) {
        this.score = score;
    }
}
