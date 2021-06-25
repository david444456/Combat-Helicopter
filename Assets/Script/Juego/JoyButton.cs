using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
//IPointerEnterHandler, IPointerExitHandler
{
    public bool Pressed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnPointerDown(PointerEventData eventData) {
        Pressed = true;
    }
    public void OnPointerUp(PointerEventData eventData) {
        Pressed = false;
    }

    /*
    public void OnPointerEnter(PointerEventData eventData)
    {
        Pressed = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Pressed = false;
    }*/

}
