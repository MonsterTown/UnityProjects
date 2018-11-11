using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Содержит информацию о том как атрибуты конвертируются в статы (Например 1 Сила дает 15 жизней)
public class StatConverter : MonoBehaviour {

    //Strength
    public static float strengthToAttackPower = 1.5f;
    public static float strengthToBlockPower = 1;
                  
    //Agility     
    public static float agilityToAttackSpeed = 1;
    public static float agilityToEvasion = 1;
    public static float agilityToMoveSpeed = 1;
                  
    //Intelligence
    public static float intelligenceToSpellPower = 1;
    public static float intelligenceToMana = 1;
                  
    //Spirit      
    public static float spiritToEnergy = 1;
    public static float spiritToHealthRegen = 0.1f;
    public static float spiritToManaRegen = 1;
    public static float spiritToEnergyRegen = 1;
    public static float spiritToHealEffectiveness = 1;

    //Vitality
    public static float vitalityToHealth = 13;
    public static float vitalityToToxity = 1;
}
