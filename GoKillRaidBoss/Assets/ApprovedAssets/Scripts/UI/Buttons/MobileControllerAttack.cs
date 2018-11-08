using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MobileControllerAttack : MonoBehaviour, IPointerUpHandler, IPointerDownHandler {

    public bool input;

    public void OnPointerDown(PointerEventData eventData) {
        input = true;
    }

    public void OnPointerUp(PointerEventData eventData) {
        input = false;
    }
}
