using Kryz.CharacterStats;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_AttributesUpdateValue : MonoBehaviour {
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
    public Text healthEffectivValue;
    public Text healthValue;
    public Text toxityValue;

    [Space(1, order = 0)] [Header("Quick Menu Stats", order = 1)] [Space(4, order = 2)]
    public Text quickHealth;

    public Text quickAttackPower;
    public Text quickSpellPower;
    public Text quickMana;

    void Start() {
        StartCoroutine(Rewrite()); //Старт регенерации хитов, маны и усталости.
    }

    IEnumerator Rewrite() {
        while (true) {
            //Primary
            strengthValue.text = player.GetComponent<UnitAttributes>().Strength.Value.ToString();
            agilityValue.text = player.GetComponent<UnitAttributes>().Agility.Value.ToString();
            intelligenceValue.text = player.GetComponent<UnitAttributes>().Intelligence.Value.ToString();
            spiritValue.text = player.GetComponent<UnitAttributes>().Spirit.Value.ToString();
            vitalityValue.text = player.GetComponent<UnitAttributes>().Vitality.Value.ToString();

            skillPointsValue.text = player.GetComponent<UnitAttributes>().skillPointsValue.ToString();

            //Secondary
            attackPowerValue.text = player.GetComponent<UnitAttributes>().AttackPower.Value.ToString();
            blockPowerValue.text = player.GetComponent<UnitAttributes>().BlockPower.Value.ToString();
            attackSpeedValue.text = player.GetComponent<UnitAttributes>().AttackSpeed.Value.ToString();
            moveSpeedValue.text = player.GetComponent<UnitAttributes>().MoveSpeed.Value.ToString();
            evasionValue.text = player.GetComponent<UnitAttributes>().Evasion.Value.ToString();
            spellPowerValue.text = player.GetComponent<UnitAttributes>().SpellPower.Value.ToString();
            manaValue.text = player.GetComponent<UnitAttributes>().Mana.Value.ToString();
            energyValue.text = player.GetComponent<UnitAttributes>().Energy.Value.ToString();
            healthRegenValue.text = player.GetComponent<UnitAttributes>().HealthRegeneration.Value.ToString();
            manaRegenValue.text = player.GetComponent<UnitAttributes>().ManaRegeneration.Value.ToString();
            energyRegenValue.text = player.GetComponent<UnitAttributes>().EnergyRegeneration.Value.ToString();
            healthEffectivValue.text = player.GetComponent<UnitAttributes>().HealEffectiveness.Value.ToString();
            healthValue.text = player.GetComponent<UnitAttributes>().Health.Value.ToString();
            toxityValue.text = player.GetComponent<UnitAttributes>().Toxity.Value.ToString();

            //Quick
            quickHealth.text = healthValue.text;
            quickAttackPower.text = attackPowerValue.text;
            quickSpellPower.text = spellPowerValue.text;

            yield return new WaitForSeconds(0.05f);
        }
    }
}