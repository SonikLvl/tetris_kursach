using UnityEngine;


public class NewSpawnerScript : MonoBehaviour
{
    public GameObject[] objectsToClone; // Масив об'єктів, які потрібно клонувати

    void Start()
    {
        CloneObjects();
    }

    void CloneObjects()
    {
        if (objectsToClone == null || objectsToClone.Length == 0)
        {
            Debug.LogWarning("Масив об'єктів для клонування порожній.");
            return;
        }

        // Проходимо по масиву об'єктів один раз
        for (int i = 0; i < objectsToClone.Length; i++)
        {
            if (objectsToClone[i] != null)
            {
                // Клонуємо об'єкт
                GameObject clonedObject = Instantiate(objectsToClone[i]);

                // Можна змінити позицію клонованого об'єкта, якщо потрібно
                clonedObject.transform.position = objectsToClone[i].transform.position + new Vector3(2, 0, 0); // Наприклад, зміщуємо на 2 одиниці по осі X

                // Можна також змінити ім'я клонованого об'єкта, щоб було зрозуміло, що це клон
                clonedObject.name = objectsToClone[i].name + "_Clone";
            }
            else
            {
                Debug.LogWarning("Один з об'єктів у масиві для клонування є null.");
            }
        }
    }
}