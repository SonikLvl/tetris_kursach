using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.EventSystems;
// using UnityEngine.InputSystem; // Додайте, якщо використовуєте нову систему вводу

public class StopGameManager : MonoBehaviour
{

    public static bool IsGamePausedGlobally = false;

    public EventSystem mainEventSystem;

    void Awake()
    {
        mainEventSystem = FindObjectOfType<EventSystem>();
        if (mainEventSystem == null)
        {
            Debug.LogWarning("[Unity C#] EventSystem не знайдено в сцені. Ввід UI може не зупинятися коректно.");
        }
    }

    [Preserve]
    public void SetPaused(int isPaused)
    {
        if (isPaused == 1) // Пауза
        {
            IsGamePausedGlobally = true; // Встановлюємо статичний прапор паузи
            Time.timeScale = 0;
            AudioListener.pause = true;

            if (mainEventSystem != null)
            {
                mainEventSystem.enabled = false;
                Debug.Log("[Unity C#] EventSystem вимкнено.");
            }


            Debug.Log("[Unity C#] Game paused from JS. Global flag set.");
        }
        else // isPaused == 0 - Відновити
        {
            // Важливо: спочатку увімкнути ввід, потім зняти Time.timeScale, щоб уникнути стрибків
            IsGamePausedGlobally = false; // Скидаємо статичний прапор паузи

             if (mainEventSystem != null)
            {
                mainEventSystem.enabled = true;
                Debug.Log("[Unity C#] EventSystem увімкнено.");
            }



            Time.timeScale = 1; // Відновлюємо хід часу
            AudioListener.pause = false; // Відновлюємо звук


            Debug.Log("[Unity C#] Game unpaused from JS. Global flag reset.");
        }
    }
}