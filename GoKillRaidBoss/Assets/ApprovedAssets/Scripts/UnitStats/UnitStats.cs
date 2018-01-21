using Kryz.CharacterStats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class UnitStats : MonoBehaviour {
    //Компоненты
    [HideInInspector] public Animator animator;
    [HideInInspector] public GameObjectTags gameObjectTags;   //ссылка на свои теги
    public GameObject hitBox; //ссылка на дочерний хитбокс
    public AbstractDead deadScript;
    public AbstractAttack attackScript;

    #region Fields
    //Health
    [Space(1, order = 0)]
    [Header("Health", order = 1)]
    [Space(4, order = 2)]

    public float healthCur;   //Make privats
    public float HealthCur {
        get { return healthCur; }
        set {
            if (value <= 0) {
                healthCur = 0;
                Dead();
            } else if (value > HealthMax) healthCur = HealthMax;
            else healthCur = value;
        }
    }

    public float healthMax;
    public float HealthMax {
        get { return healthMax; }
        set {
            if (value <= 0) {
                healthMax = 0;
                healthCur = 0;
            } else {
                float temp;
                temp = healthCur / healthMax;
                healthMax = value;
                healthCur = healthMax * temp;
            }
        }
    }

    public float healthRegen = 0.1f;
    public float HealthRegen {
        get {
            return healthRegen;
        }

        set {
            healthRegen = value;
        }
    }
    [HideInInspector] public bool regenOn = true;  //включает / выключает регенрацию

    //Attack
    [Space(1, order = 0)]
    [Header("Attack", order = 1)]
    [Space(4, order = 2)]

    public float lookRange;
    public float attackRange;
    public float attackDamage;
    public float minDamage = 0.8f;
    public float maxDamage = 1.2f;

    [HideInInspector] public List<GameObject> targetHitList = new List<GameObject>();
    [HideInInspector] public GameObject attackTarget;

    //Defence
    public float armor;
    #endregion

    //Init
    void Awake() {
        animator = GetComponent<Animator>();
        gameObjectTags = transform.root.gameObject.GetComponent<GameObjectTags>();
    }

    void Start() {
        StartCoroutine(Regeneration()); //Старт регенерации хитов, маны и усталости.
    }

    #region Health/Mana/Concentration Regeneration
    IEnumerator Regeneration() {
        while (regenOn) {
            HealthCur += HealthRegen * 0.05f;
            yield return new WaitForSeconds(0.05f);
        }
    }
    #endregion

    #region TakingDamage

    float DamageArmorReduce(float damage) {
        return damage -= armor;
    }

    public void TakingDamage(GameObject from, float damage) {

        float damageReducedByArmor = DamageArmorReduce(damage);
        HealthCur -= damageReducedByArmor;
        if (!animator.GetCurrentAnimatorStateInfo(1).IsName("Hit"))   //Проигрывается ли анимация получения удара? Если да то ненадо снова ее слать (1)значит 2 слой в аниматоре
        {
            animator.SetTrigger("Hit");
        }

        //Если это AI то добавить в список атакующих
        AddAttackerToEnemyList(from);
    }
    #endregion

    #region AddAttackersToEnemyList
    //Когда юнит получает урон он добавляется в список тех кто его атакнул, если врагов во круге нет, он пойдет искать обидчика в течении 10 сек.
    public List<GameObject> attackers;
    public void AddAttackerToEnemyList(GameObject attacker) {
        if (!attackers.Contains(attacker)                                     //Еще нет в листе?
        && attacker.gameObject.transform.root.gameObject != transform.root.gameObject /*Не сам*/
        && attacker.gameObject.GetComponent<GameObjectTags>() //Есть компонент? 
        && attacker.gameObject.GetComponent<GameObjectTags>().unit == true //Является юнитом 
        && attacker.gameObject.GetComponent<GameObjectTags>().dead == false //не мертвый 
        && attacker.gameObject.GetComponent<GameObjectTags>().fraction != gameObjectTags.fraction) { //*Не союзники (обьекты с одинаковыми тегами - фракциями)

            attackers.Add(attacker);
            StartCoroutine(RemoveAttackerFromList(attacker));
        }
    }

    IEnumerator RemoveAttackerFromList(GameObject attacker) {
        yield return new WaitForSeconds(10f);
        attackers.Remove(attacker);
    }
    #endregion

    #region HitEvent
    //Событие, приходит из аниматора
    void HitEvent() {
        attackScript.Attack(gameObject);
    }
    #endregion

    #region AttackAnimBegin
    //Событие, приходит из аниматора начало клипа атаки ТОЛЬКО ДЛЯ НПС НУЖНО ПЕРЕРАБОТАТЬ БОЛЕЕ АБСТРАКТНО
    void AnimatorAttackBeginEvent() {
        if (!GetComponent<StateController>().targetForChaseOrAttack) {
            transform.LookAt(GetComponent<StateController>().targetForChaseOrAttack.transform);  //поворот к цели во время атаки
        }
    }
    #endregion

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
    public CharacterStat Health;
    public CharacterStat Toxity;
    #endregion

    #region Dead
    void Dead() {
        deadScript.Dead(gameObject);
    }
    #endregion
}
