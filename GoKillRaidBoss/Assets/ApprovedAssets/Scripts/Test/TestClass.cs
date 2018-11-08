using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestClass : MonoBehaviour {


    public  delegate void MyDelegate();
    public  MyDelegate myD = delegate { };

    void Start() {
        myD = Text;
    }

    void Update() {
        if (Input.GetButtonDown("Fire1")) {
            myD();
        }
    }


    private void Text() {

        Debug.Log("text!", gameObject);
    }
}
