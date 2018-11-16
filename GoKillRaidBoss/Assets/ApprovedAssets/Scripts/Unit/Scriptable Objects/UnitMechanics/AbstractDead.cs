using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractDead : ScriptableObject {
    public abstract void Dead(GameObject self);
}