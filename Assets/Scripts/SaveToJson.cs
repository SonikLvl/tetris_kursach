//Скрипт для збереження і відправки рахунку на сайт

using UnityEngine;
using System.Runtime.InteropServices;

public class SaveToJson : MonoBehaviour
{
    // Оголошуємо зовнішню функцію JavaScript
    // ВАЖЛИВО: Ця JS функція має бути визначена у global scope (window)
    // на вашій Vue.js сторінці перед завантаженням Unity WebGL.
    // Як це зробити у Vue, описано в попередній відповіді.
    [DllImport("__Internal")]
    private static extern void sendGameDataToWeb(string jsonData); // Назва функції в JS, яку ви викличете

    public void SendScoreToDatabase(int score) // Змінимо назву методу для ясності
    {
        Score scoreObj = new Score(score);
        string ScoreDataJson = JsonUtility.ToJson(scoreObj); // Згенерували JSON

        // *************************************************************
        // !!! ЗАМІСТЬ збереження у файл, викликаємо JS функцію !!!
        // System.IO.File.WriteAllText(Application.persistentDataPath + "/ScoreData.json", ScoreDataJson); // Видалити цей рядок
        // Debug.Log(Application.persistentDataPath + "/ScoreData.json"); // Видалити цей рядок про шлях до файлу

#if UNITY_WEBGL // Перевірка, що ми в WebGL
        sendGameDataToWeb(ScoreDataJson); // Викликаємо зовнішню JS функцію
        Debug.Log("Score JSON sent to web: " + ScoreDataJson);
#else
        Debug.LogWarning("Attempted to send data to web, but not in WebGL build.");
        // Можливо, тут логіка для інших платформ (наприклад, збереження у файл або інша API)
        // Для тестування в редакторі можна зберегти в файл, але для WebGL це не спрацює для відправки на сервер.
        System.IO.File.WriteAllText(Application.persistentDataPath + "/ScoreData.json", ScoreDataJson);
        Debug.Log("Score JSON saved locally for testing: " + Application.persistentDataPath + "/ScoreData.json");
#endif
        // *************************************************************
    }
}

[System.Serializable]
public class Score {
    public int score;

    public Score(int score) {
        this.score = score;

    }
}
