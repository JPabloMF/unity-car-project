using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public AudioSource hoverSound;

    void Start()
    {
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hoverSound.Play();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hoverSound.Stop();
    }
}
