using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    UnitStats unitStats;
    Animator animator;

    private void Awake() {
        unitStats = GetComponent<UnitStats>();
        animator = GetComponent<Animator>();
    }

    void Update() {

        animator.ResetTrigger("Attack");

        //For PC
        // if (Input.GetMouseButton(0)) {
        //     animator.SetTrigger("Attack"); 
        // }

        PlayerMobileController();
    }

    //For Android
    private void PlayerMobileController() {
        if (Input.GetMouseButtonDown(0)) {
            animator.SetTrigger("Attack");
        }
        if (Input.GetKeyDown(KeyCode.Space)) {

        }
    }
}
