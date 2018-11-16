using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MobileControllerCharacterMenuInventoryTab : MonoBehaviour, IPointerUpHandler, IPointerDownHandler {
    public bool input;   //Возвращается всегда true пока нажата
    public bool trigger; //Возвращает true в 1 Update методе и в LateUpdate выключается

    public void OnPointerDown(PointerEventData eventData) {
        input = true;
        trigger = true;
    }

    public void OnPointerUp(PointerEventData eventData) {
        input = false;
    }

    public void LateUpdate() {
        trigger = false;
    }

    public void OnDisable() {
        trigger = false;
        input = false;
    }
}