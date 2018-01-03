using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMenuController : MonoBehaviour {

    public GameObject menuCharacter;

	void Update () {

        if (Input.GetKeyDown(KeyCode.I)) {
            if (menuCharacter.activeSelf) {
                menuCharacter.SetActive(false);
            } else {
                menuCharacter.SetActive(true);
            }
        }
    }
}
