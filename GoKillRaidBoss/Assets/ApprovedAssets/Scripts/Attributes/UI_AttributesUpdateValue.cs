using Kryz.CharacterStats;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_AttributesUpdateValue : MonoBehaviour {

    public GameObject player;

    public Text strengthValue;
    public Text agilityValue;
    public Text intelligenceValue;
    public Text spiritValue;
    public Text vitalityValue;

    public Text skillPointsValue;

    [Space(1, order = 0)]
    [Header("Secondary Attributes", order = 1)]
    [Space(4, order = 2)]
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
    public Text healthValue;
    public Text toxityValue;

    void Start() {
        StartCoroutine(Rewrite()); //Старт регенерации хитов, маны и усталости.
    }

    IEnumerator Rewrite() {
        while (true) {
            strengthValue.text = player.GetComponent<UnitStats>().Strength.Value.ToString();
            agilityValue.text = player.GetComponent<UnitStats>().Agility.Value.ToString();
            intelligenceValue.text = player.GetComponent<UnitStats>().Intelligence.Value.ToString();
            spiritValue.text = player.GetComponent<UnitStats>().Spirit.Value.ToString();
            vitalityValue.text = player.GetComponent<UnitStats>().Vitality.Value.ToString();

            skillPointsValue.text = player.GetComponent<UnitStats>().skillPointsValue.ToString();

            yield return new WaitForSeconds(0.05f);
        }
    }

    /*
    void Update() {
        strengthValue.text = player.GetComponent<UnitStats>().Strength.Value.ToString();
        agilityValue.text = player.GetComponent<UnitStats>().Agility.Value.ToString();
        intelligenceValue.text = player.GetComponent<UnitStats>().Intelligence.Value.ToString();
        spiritValue.text = player.GetComponent<UnitStats>().Spirit.Value.ToString();
        vitalityValue.text = player.GetComponent<UnitStats>().Vitality.Value.ToString();

        skillPointsValue.text = player.GetComponent<UnitStats>().skillPointsValue.ToString();

        attackPowerValue.text = player.GetComponent<UnitStats>().AttackPower.Value.ToString();
        blockPowerValue.text = player.GetComponent<UnitStats>().BlockPower.Value.ToString();
     //   attackSpeedValue.text = player.GetComponent<UnitAttributes>().AttackSpeed.Value.ToString();
      //  moveSpeedValue.text = player.GetComponent<UnitAttributes>().MoveSpeed.Value.ToString();
     //   evasionValue.text = player.GetComponent<UnitAttributes>().Evasion.Value.ToString();
        spellPowerValue.text = player.GetComponent<UnitStats>().SpellPower.Value.ToString();
        manaValue.text = player.GetComponent<UnitStats>().Mana.Value.ToString();
        energyValue.text = player.GetComponent<UnitStats>().Energy.Value.ToString();
        healthRegenValue.text = player.GetComponent<UnitStats>().HealthRegeneration.Value.ToString();
        manaRegenValue.text = player.GetComponent<UnitStats>().ManaRegeneration.Value.ToString();
        energyRegenValue.text = player.GetComponent<UnitStats>().EnergyRegeneration.Value.ToString();
        healthValue.text = player.GetComponent<UnitStats>().Health.Value.ToString();
        toxityValue.text = player.GetComponent<UnitStats>().Toxity.Value.ToString();
    }
    */
}
