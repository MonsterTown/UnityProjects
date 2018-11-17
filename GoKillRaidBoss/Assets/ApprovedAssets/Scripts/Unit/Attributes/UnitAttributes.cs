using System;
using System.Collections.Generic;
using Kryz.CharacterStats;
using UnityEngine;


//Класс с атрибутами юнита, тут они пересчитываются и обновляются
[Serializable]
public class UnitAttributes {
    private GameObject self;

    #region PrimaryAttributes

    [Space(1, order = 0)]
    [Header("Primary Attributes", order = 1)]
    [Space(4, order = 2)]
    [SerializeField]
    public CharacterStat Strength;
    public CharacterStat Agility;
    public CharacterStat Intelligence;
    public CharacterStat Spirit;
    public CharacterStat Vitality;

    #endregion

    [Space(1, order = 0)]
    [Header("Attribute Points", order = 1)]
    [Space(4, order = 2)]
    public float skillPointsValue;

    #region SecondaryAttributes

    [Space(1, order = 0)]
    [Header("Secondary Attributes", order = 1)]
    [Space(4, order = 2)]
    public CharacterStat AttackPower;
    public CharacterStat BlockPower;
    public CharacterStat AttackSpeed;
    public CharacterStat MoveSpeed;
    public CharacterStat Evasion;
    public CharacterStat Mana;
    public CharacterStat Energy;
    public CharacterStat SpellPower;
    public CharacterStat HealthRegeneration;
    public CharacterStat ManaRegeneration;
    public CharacterStat EnergyRegeneration;
    public CharacterStat HealEffectiveness;
    public CharacterStat Health;
    public CharacterStat Toxity;

    private StatModifier attackPowerMod;
    private StatModifier blockPowerMod;
    private StatModifier attackSpeedMod;
    private StatModifier moveSpeedMod;
    private StatModifier evasionMod;
    private StatModifier manaMod;
    private StatModifier spellPowerMod;
    private StatModifier energyMod;
    private StatModifier healthMod;
    private StatModifier healthRegenMod;
    private StatModifier healEffectivMod;
    private StatModifier manaRegenMod;
    private StatModifier energyRegenMod;
    private StatModifier toxityMod;  

    #endregion


    public void Init(GameObject self) { //Возможно нужно OnEnable
        this.self = self;
        InitStatsModifiers(); //TODO: Возможно нужен класс инициализатор который будет пересчитывать характеристики где то в одном классе
        SecondaryAttributesObserverInit();
        CalculateAttributes();
    }

    public void InitStatsModifiers() {
        Strength.AddObserver(AttackPower); //Подписка по паттерну обсервер. Если ктото дернет Strength.Value то у AttackPower сработает метод UpdateObserver
        Strength.AddObserver(BlockPower);

        Agility.AddObserver(AttackSpeed);
        Agility.AddObserver(MoveSpeed);
        Agility.AddObserver(Evasion);

        Intelligence.AddObserver(SpellPower);
        Intelligence.AddObserver(Mana);

        Vitality.AddObserver(Health);
        Vitality.AddObserver(Toxity);

        Spirit.AddObserver(Energy);
        Spirit.AddObserver(HealthRegeneration);
        Spirit.AddObserver(ManaRegeneration);
        Spirit.AddObserver(EnergyRegeneration);
        Spirit.AddObserver(HealEffectiveness);

        //======================

        Strength.owner = self;
        Agility.owner = self;
        Intelligence.owner = self;
        Spirit.owner = self;
        Vitality.owner = self;

        //============
        AttackPower.owner = self;
        BlockPower.owner = self;

        AttackSpeed.owner = self;
        MoveSpeed.owner = self;
        Evasion.owner = self;

        SpellPower.owner = self;
        Mana.owner = self;

        Health.owner = self;
        Toxity.owner = self;

        Energy.owner = self;
        HealthRegeneration.owner = self;
        ManaRegeneration.owner = self;
        EnergyRegeneration.owner = self;
        HealEffectiveness.owner = self;
    }

    //Подписка вторичных аттрибутов на классы статы.
    public void SecondaryAttributesObserverInit() {
        Health.AddObserver(self.GetComponent<UnitStats>().Health); //Подписка по паттерну обсервер
        
        HealthRegeneration.AddObserver(self.GetComponent<UnitStats>().Health);
    }

    //Тут пересчитываются все вторичные атрибуты
    public void CalculateAttributes() {
        //Удаляются модификаторы
        AttackPower.RemoveModifier(attackPowerMod);
        BlockPower.RemoveModifier(blockPowerMod);

        AttackSpeed.RemoveModifier(attackSpeedMod);
        MoveSpeed.RemoveModifier(moveSpeedMod);
        Evasion.RemoveModifier(evasionMod);

        SpellPower.RemoveModifier(spellPowerMod);
        Mana.RemoveModifier(manaMod);

        Health.RemoveModifier(healthMod);
        Toxity.RemoveModifier(toxityMod);

        Energy.RemoveModifier(energyMod);
        HealthRegeneration.RemoveModifier(healthRegenMod);
        ManaRegeneration.RemoveModifier(manaRegenMod);
        EnergyRegeneration.RemoveModifier(energyRegenMod);
        HealEffectiveness.RemoveModifier(healEffectivMod);

        // Создаются заново
        attackPowerMod = new StatModifier(StatConverter.strengthToAttackPower * Strength.Value, StatModType.Flat);
        blockPowerMod = new StatModifier(StatConverter.strengthToBlockPower * Strength.Value, StatModType.Flat);

        attackSpeedMod = new StatModifier(StatConverter.agilityToAttackSpeed * Agility.Value, StatModType.Flat);
        moveSpeedMod = new StatModifier(StatConverter.agilityToMoveSpeed * Agility.Value, StatModType.Flat);
        evasionMod = new StatModifier(StatConverter.agilityToEvasion * Agility.Value, StatModType.Flat);

        spellPowerMod = new StatModifier(StatConverter.intelligenceToSpellPower * Intelligence.Value, StatModType.Flat);
        manaMod = new StatModifier(StatConverter.intelligenceToMana * Intelligence.Value, StatModType.Flat);

        healthMod = new StatModifier(StatConverter.vitalityToHealth * Vitality.Value, StatModType.Flat);
        toxityMod = new StatModifier(StatConverter.vitalityToToxity * Vitality.Value, StatModType.Flat);

        energyMod = new StatModifier(StatConverter.spiritToEnergy * Spirit.Value, StatModType.Flat);
        healthRegenMod = new StatModifier(StatConverter.spiritToHealthRegen * Spirit.Value, StatModType.Flat);
        manaRegenMod = new StatModifier(StatConverter.spiritToManaRegen * Spirit.Value, StatModType.Flat);
        energyRegenMod = new StatModifier(StatConverter.spiritToEnergyRegen * Spirit.Value, StatModType.Flat);
        healEffectivMod = new StatModifier(StatConverter.spiritToHealEffectiveness * Spirit.Value, StatModType.Flat);

        //Добавляются модификаторы
        AttackPower.AddModifier(attackPowerMod);
        BlockPower.AddModifier(blockPowerMod);

        AttackSpeed.AddModifier(attackSpeedMod);
        MoveSpeed.AddModifier(moveSpeedMod);
        Evasion.AddModifier(evasionMod);

        SpellPower.AddModifier(spellPowerMod);
        Mana.AddModifier(manaMod);

        Health.AddModifier(healthMod);
        Toxity.AddModifier(toxityMod);

        Energy.AddModifier(energyMod);
        HealthRegeneration.AddModifier(healthRegenMod);
        ManaRegeneration.AddModifier(manaRegenMod);
        EnergyRegeneration.AddModifier(energyRegenMod);
        HealEffectiveness.AddModifier(healEffectivMod);

        //Костыль
        HealthCalculate();

        if (OnCalculate != null) {
            OnCalculate();
        }
    }

    //Перерасчет непосредственно статов
    public void HealthCalculate() {
        self.GetComponent<UnitStats>().Health.HealthMax = Health.Value;
        self.GetComponent<UnitStats>().attackDamage = AttackPower.Value;
        self.GetComponent<UnitStats>().Health.HealthRegen = HealthRegeneration.Value;
    }

    public delegate void CalculateAction();

    public event CalculateAction OnCalculate;
}