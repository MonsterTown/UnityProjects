using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MobileController : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler {
    private Image joystickBG;
    [SerializeField] private Image joystick;
    private Vector2 inputVector;

    private void Start() {
        joystickBG = GetComponent<Image>();
        joystick = transform.GetChild(0).GetComponent<Image>();
    }

    public virtual void OnPointerDown(PointerEventData ped) {
        OnDrag(ped);
    }

    public virtual void OnPointerUp(PointerEventData ped) {
        inputVector = Vector2.zero;
        joystick.rectTransform.anchoredPosition = Vector2.zero;
    }

    public virtual void OnDrag(PointerEventData ped) {
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(joystickBG.rectTransform, ped.position, ped.pressEventCamera, out pos)) {
            pos.x = (pos.x / joystick.rectTransform.sizeDelta.x);
            pos.y = (pos.y / joystick.rectTransform.sizeDelta.x);

            inputVector = new Vector2(pos.x * 0.5f - 0, pos.y * 0.5f - 0);
            inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

            joystick.rectTransform.anchoredPosition = new Vector2(inputVector.x * (joystickBG.rectTransform.sizeDelta.x / 2), inputVector.y * (joystickBG.rectTransform.sizeDelta.y / 2));
        }
    }

    public float Horizontal() {
        if (inputVector.x != 0) return inputVector.normalized.x;
        else return Input.GetAxis("Horizontal");
    }

    public float Vertical() {
        if (inputVector.y != 0) return inputVector.normalized.y;
        else return Input.GetAxis("Vertical");
    }
}