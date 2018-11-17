using Kryz.CharacterStats;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Названия значения полей в интерфейсе юзера
public enum AttributesNameUI {
    AttackPowerValue,
    BlockPowerValue,
    AttackSpeedValue,
    MoveSpeedValue,
    EvasionValue,
    SpellPowerValue,
    ManaValue,
    EnergyValue,
    HealthRegenValue,
    ManaRegenValue,
    EnergyRegenValue,
    HealEffectivValue,
    HealthValue,
    ToxityValue,
}

[Serializable]
public class UI_AttributesUpdateValue :  Observer {
    public GameObject player;

    [Space(1, order = 0)] [Header("Primary Attributes", order = 1)] [Space(4, order = 2)]
    public Text strengthValue;
    public Text agilityValue;
    public Text intelligenceValue;
    public Text spiritValue;
    public Text vitalityValue;

    public Text skillPointsValue;

    [Space(1, order = 0)] [Header("Secondary Attributes", order = 1)] [Space(4, order = 2)]
    public Text attackPowerValue;
    public Text blockPowerValue;
    public Text attackSpeedValue;
    public Text moveSpeedValue;
    public Text evasionValue;
    public Text spellPowerValue;
    public Text manaValue;
    public Text energyValue;
    public Text healthRegenValue;
    public Text manaRegenValue;
    public Text energyRegenValue;
    public Text healEffectivValue;
    public Text healthValue;
    public Text toxityValue;

    [Space(1, order = 0)] [Header("Quick Menu Stats", order = 1)] [Space(4, order = 2)]
    public Text quickHealth;
    public Text quickAttackPower;
    public Text quickSpellPower;
    public Text quickMana;
    

    public void Start() {
        Rewrite();
        //  player.GetComponent<UnitStats>().UnitAttributes.Health.AddObserver(this); //Подписка по паттерну обсервер
          player.GetComponent<UnitStats>().UnitAttributes.OnCalculate += ObserverUpdate; //Подписка по паттерну обсервер
    }

    //Переписывает значения с атрибутов в UI меню
    void Rewrite() {

        //Primary
        strengthValue.text = player.GetComponent<UnitStats>().UnitAttributes.Strength.Value.ToString();
        agilityValue.text = player.GetComponent<UnitStats>().UnitAttributes.Agility.Value.ToString();
        intelligenceValue.text = player.GetComponent<UnitStats>().UnitAttributes.Intelligence.Value.ToString();
        spiritValue.text = player.GetComponent<UnitStats>().UnitAttributes.Spirit.Value.ToString();
        vitalityValue.text = player.GetComponent<UnitStats>().UnitAttributes.Vitality.Value.ToString();

        skillPointsValue.text = player.GetComponent<UnitStats>().UnitAttributes.skillPointsValue.ToString();

        //Secondary
        attackPowerValue.text = player.GetComponent<UnitStats>().UnitAttributes.AttackPower.Value.ToString();
        blockPowerValue.text = player.GetComponent<UnitStats>().UnitAttributes.BlockPower.Value.ToString();
        attackSpeedValue.text = player.GetComponent<UnitStats>().UnitAttributes.AttackSpeed.Value.ToString();
        moveSpeedValue.text = player.GetComponent<UnitStats>().UnitAttributes.MoveSpeed.Value.ToString();
        evasionValue.text = player.GetComponent<UnitStats>().UnitAttributes.Evasion.Value.ToString();
        spellPowerValue.text = player.GetComponent<UnitStats>().UnitAttributes.SpellPower.Value.ToString();
        manaValue.text = player.GetComponent<UnitStats>().UnitAttributes.Mana.Value.ToString();
        energyValue.text = player.GetComponent<UnitStats>().UnitAttributes.Energy.Value.ToString();
        healthRegenValue.text = player.GetComponent<UnitStats>().UnitAttributes.HealthRegeneration.Value.ToString();
        manaRegenValue.text = player.GetComponent<UnitStats>().UnitAttributes.ManaRegeneration.Value.ToString();
        energyRegenValue.text = player.GetComponent<UnitStats>().UnitAttributes.EnergyRegeneration.Value.ToString();
        healEffectivValue.text = player.GetComponent<UnitStats>().UnitAttributes.HealEffectiveness.Value.ToString();
        healthValue.text = player.GetComponent<UnitStats>().UnitAttributes.Health.Value.ToString();
        toxityValue.text = player.GetComponent<UnitStats>().UnitAttributes.Toxity.Value.ToString();

        //Quick
        quickHealth.text = healthValue.text;
        quickAttackPower.text = attackPowerValue.text;
        quickSpellPower.text = spellPowerValue.text;
    }

    public void ObserverUpdate() {
        Rewrite();
    }
}