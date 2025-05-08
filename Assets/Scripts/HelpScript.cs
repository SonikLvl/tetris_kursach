//Скрипт для відображення підказок для нового режиму

using UnityEngine;
using UnityEngine.EventSystems;

public class HelpScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GameObject image;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (image != null)
            image.SetActive(true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (image != null)
            image.SetActive(false);
    }
}
