using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStats : MonoBehaviour
{
    //Компоненты
    [HideInInspector] public Animator animator;
    [HideInInspector] public GameObjectTags gameObjectTags; //ссылка на свои теги
    public GameObject hitBox; //ссылка на дочерний хитбокс
    public AbstractDead deadScript;
    public AbstractAttack attackScript;
    public TakingDamage TakingDamage;
    [HideInInspector] public AttackersList AttackersList;

    #region Fields

    //Health
    [Space(1, order = 0)] [Header("Health", order = 1)] [Space(4, order = 2)] [SerializeField]
    private float healthCur;

    public float HealthCur
    {
        get { return healthCur; }
        set
        {
            if (value <= 0)
            {
                healthCur = 0;
                Dead();
            }
            else if (value > HealthMax) healthCur = HealthMax;
            else healthCur = value;
        }
    }

    [SerializeField] private float healthMax;

    public float HealthMax
    {
        get { return healthMax; }
        set
        {
            if (value <= 0)
            {
                healthMax = 0;
                healthCur = 0;
            }
            else
            {
                float temp = healthCur / healthMax;
                healthMax = value;
                healthCur = healthMax * temp;
            }
        }
    }

    public float healthRegen = 0.1f;

    public float HealthRegen
    {
        get { return healthRegen; }
        set { healthRegen = value; }
    }

    [HideInInspector] public bool regenOn = true; //включает / выключает регенрацию

    //Attack
    [Space(1, order = 0)] [Header("Attack", order = 1)] [Space(4, order = 2)]
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
    void Awake()
    {
        animator = GetComponent<Animator>();
        gameObjectTags = transform.root.gameObject.GetComponent<GameObjectTags>();
        TakingDamage = new TakingDamage(transform.root.gameObject);
        AttackersList = transform.root.gameObject.AddComponent<AttackersList>();
        AttackersList.Init(transform.root.gameObject);
    }

    void Start()
    {
        StartCoroutine(Regeneration()); //Старт регенерации хитов, маны и усталости
    }

    #region Health/Mana/Concentration Regeneration

    IEnumerator Regeneration()
    {
        while (regenOn)
        {
            HealthCur += HealthRegen * 0.05f;
            yield return new WaitForSeconds(0.05f);
        }
    }

    #endregion

    #region HitEvent

    //Событие, приходит из аниматора
    void HitEvent()
    {
        attackScript.Attack(gameObject);
    }

    #endregion

    #region AttackAnimBegin

    //Событие, приходит из аниматора начало клипа атаки ТОЛЬКО ДЛЯ НПС НУЖНО ПЕРЕРАБОТАТЬ БОЛЕЕ АБСТРАКТНО
    void AnimatorAttackBeginEvent()
    {
        if (!GetComponent<StateController>().targetForChaseOrAttack)
        {
            transform.LookAt(GetComponent<StateController>().targetForChaseOrAttack.transform); //поворот к цели во время атаки
        }
    }

    #endregion

    #region Dead

    void Dead()
    {
        deadScript.Dead(gameObject);
    }

    #endregion
}