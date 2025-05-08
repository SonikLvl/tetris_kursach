using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.EventSystems;

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

    
    void Start()
    {
        #if !UNITY_EDITOR && UNITY_WEBGL
        
        Debug.Log($"[Unity C#] Start: WebGLInput.captureAllKeyboardInput = {WebGLInput.captureAllKeyboardInput}");
        #endif
    }


    [Preserve]
    public void SetPaused(int isPaused)
    {
        if (isPaused == 1) // Пауза (модалка відкрита)
        {
            IsGamePausedGlobally = true; // Встановлюємо статичний прапор
            Time.timeScale = 0; // Зупиняємо час
            AudioListener.pause = true; // Зупиняємо звук

            if (mainEventSystem != null)
            {
                mainEventSystem.enabled = false; // Вимикаємо Unity UI ввід
                Debug.Log("[Unity C#] EventSystem вимкнено.");
            }

            #if !UNITY_EDITOR && UNITY_WEBGL
            
            WebGLInput.captureAllKeyboardInput = false;
            Debug.Log("[Unity C#] WebGLInput.captureAllKeyboardInput = false (пауза)");
            #endif


            Debug.Log("[Unity C#] Game paused from JS. Global flag set.");
        }
        else // isPaused == 0 - Відновити (модалка закрита)
        {
            // Важливо: спочатку увімкнути ввід, потім зняти Time.timeScale
            IsGamePausedGlobally = false; // Скидаємо статичний прапор

            #if !UNITY_EDITOR && UNITY_WEBGL
            // !!! УВІМКНАЄМО ЗАХОПЛЕННЯ КЛАВІАТУРИ WEBGL НАЗАД !!!
            WebGLInput.captureAllKeyboardInput = true;
            Debug.Log("[Unity C#] WebGLInput.captureAllKeyboardInput = true (відновлення)");
            #endif

             if (mainEventSystem != null)
            {
                mainEventSystem.enabled = true; // Увімкнення Unity UI ввід
                Debug.Log("[Unity C#] EventSystem увімкнено.");
            }

            
            Time.timeScale = 1; // Відновлюємо хід часу
            AudioListener.pause = false; // Відновлюємо звук

            Debug.Log("[Unity C#] Game unpaused from JS. Global flag reset.");
        }
    }
}