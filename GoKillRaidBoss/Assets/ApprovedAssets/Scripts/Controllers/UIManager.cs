using System.Collections;
using System.Collections.Generic;
using Homebrew;
using UnityEngine;

public class UIManager :  Singleton<UIManager> {

    public UI_AttributesUpdateValue UIAttributes;

    private void Start() {
        UIAttributes.Start();
    }
}
