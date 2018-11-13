using Kryz.CharacterStats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAttributes : MonoBehaviour {

    #region PrimaryAttributes
    [Space(1, order = 0)]
    [Header("Primary Attributes", order = 1)]
    [Space(4, order = 2)]
    public CharacterStat Strength;
    public CharacterStat Agility;
    public CharacterStat Intelligence;
    public CharacterStat Spirit;
    public CharacterStat Vitality;
    #endregion

    #region AttributePoints
    [Space(1, order = 0)]
    [Header("AttributePoints", order = 1)]
    [Space(4, order = 2)]
    public float skillPointsValue;
    #endregion

    #region SecondaryAttributes
    [Space(1, order = 0)]
    [Header("AttributePoints", order = 1)]
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
    public CharacterStat HealEffectiveness;   //Эффективность лечения, увеличивает получаемое лечение
    public CharacterStat Health;
    public CharacterStat Toxity;

    StatModifier attackPowerMod = new StatModifier(1, StatModType.Flat);
    StatModifier blockPowerMod = new StatModifier(1, StatModType.Flat);
    StatModifier attackSpeedMod = new StatModifier(1, StatModType.Flat);
    StatModifier moveSpeedMod = new StatModifier(1, StatModType.Flat);
    StatModifier evasionMod = new StatModifier(1, StatModType.Flat);
    StatModifier manaMod = new StatModifier(1, StatModType.Flat);
    StatModifier spellPowerMod = new StatModifier(1, StatModType.Flat);
    StatModifier energyMod = new StatModifier(1, StatModType.Flat);
    StatModifier healthMod = new StatModifier(1, StatModType.Flat);
    StatModifier healthRegenMod = new StatModifier(1, StatModType.Flat);
    StatModifier healEffectivMod = new StatModifier(1, StatModType.Flat);
    StatModifier manaRegenMod = new StatModifier(1, StatModType.Flat);
    StatModifier energyRegenMod = new StatModifier(1, StatModType.Flat);
    StatModifier toxityMod = new StatModifier(1, StatModType.Flat);
    #endregion

    
    
    void Start () {
     //   InitStatsModifiers(); //TODO: Нужен класс инициализатор который будет пересчитывать характеристики где то в одном классе
    }

    public void InitStatsModifiers() {
        Strength.AddObserver(AttackPower); //Подписка по паттерну обсервер
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

        //============
        AttackPower.owner = gameObject;
        BlockPower.owner = gameObject;

        AttackSpeed.owner = gameObject;
        MoveSpeed.owner = gameObject;
        Evasion.owner = gameObject;

        SpellPower.owner = gameObject;
        Mana.owner = gameObject;

        Health.owner = gameObject;
        Toxity.owner = gameObject;


        Energy.owner = gameObject;
        HealthRegeneration.owner = gameObject;
        ManaRegeneration.owner = gameObject;
        EnergyRegeneration.owner = gameObject;
        HealEffectiveness.owner = gameObject;
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

        //Создаются заново
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
    }
    
    public void HealthCalculate() {
        gameObject.GetComponent<UnitStats>().Health.HealthMax = Health.Value;
        gameObject.GetComponent<UnitStats>().attackDamage = AttackPower.Value;
        gameObject.GetComponent<UnitStats>().Health.HealthRegen = HealthRegeneration.Value;
    }

//    private void OnEnable()
//    {
//        HealthCalculate();
//    }
}
