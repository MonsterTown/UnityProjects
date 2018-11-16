using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectTags : MonoBehaviour {
    [Space(1, order = 0)] [Header("Type of GameObject", order = 1)] [Space(4, order = 2)]
    public bool unit;

    public bool projectile;
    public bool none;

    [Space(1, order = 0)] [Header("Fraction of GameObject", order = 1)] [Space(4, order = 2)]
    public int fraction;

    [Space(1, order = 0)] [Header("Is it Dead?", order = 1)] [Space(4, order = 2)]
    public bool dead;
}