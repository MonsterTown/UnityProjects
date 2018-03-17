using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractAttack : ScriptableObject {

    public abstract void Attack(GameObject self);

    public GameObject targetAttack;  //Используется в PlayerController
}