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
    [HideInInspector] public Health Health;

    #region Fields

    //Health
    [Space(1, order = 0)] [Header("Health", order = 1)] [Space(4, order = 2)] [SerializeField]
    public float healthMax;
    public float healthRegen;
//    private float healthCur;
//
//    public float HealthCur
//    {
//        get { return healthCur; }
//        set
//        {
//            if (value <= 0)
//            {
//                healthCur = 0;
//                Dead();
//            }
//            else if (value > HealthMax) healthCur = HealthMax;
//            else healthCur = value;
//        }
//    }
//
//    [SerializeField] private float healthMax;
//
//    public float HealthMax
//    {
//        get { return healthMax; }
//        set
//        {
//            if (value <= 0)
//            {
//                healthMax = 0;
//                healthCur = 0;
//            }
//            else
//            {
//                float temp = healthCur / healthMax;
//                healthMax = value;
//                healthCur = healthMax * temp;
//            }
//        }
//    }
//
//    public float healthRegen = 0.1f;
//
//    public float HealthRegen
//    {
//        get { return healthRegen; }
//        set { healthRegen = value; }
//    }
//
//    [HideInInspector] public bool regenOn = true; //включает / выключает регенрацию

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
        Health = transform.root.gameObject.AddComponent<Health>();
      //  Health.Init();
    }

    void Start()
    {
//        StartCoroutine(Regeneration()); //Старт регенерации хитов, маны и усталости
    }

    #region Health/Mana/Concentration Regeneration

//    IEnumerator Regeneration()
//    {
//        while (regenOn)
//        {
//            HealthCur += HealthRegen * 0.05f;
//            yield return new WaitForSeconds(0.05f);
//        }
//    }

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

    public void Dead()
    {
        deadScript.Dead(gameObject);
    }

    #endregion
}

//Класс здоровья
public class Health : MonoBehaviour
{
    private UnitStats stats;

    [SerializeField]
    private float healthCur = 1f;

    public float HealthCur
    {
        get { return healthCur; }
        set
        {
            if (value <= 0)
            {
                healthCur = 0;
                stats.Dead();
            }
            else if (value > HealthMax) healthCur = HealthMax;
            else healthCur = value;
        }
    }

    [SerializeField]
    private float healthMax = 1f;

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

    public bool regenOn = true; //включает / выключает регенрацию
    
    public void Init()
    {
        
    }

    private void Start()
    {
        gameObject.GetComponent<UnitAttributes>().HealthCalculate();
    }

    private void OnEnable()
    {
        stats = gameObject.GetComponent<UnitStats>();
//        gameObject.GetComponent<UnitAttributes>().HealthCalculate();
        StartCoroutine(Regeneration());
    }

    IEnumerator Regeneration()
    {
        while (true)
        {
            if (regenOn)
            {
                HealthCur += HealthRegen * 0.05f;
                yield return new WaitForSeconds(0.05f);
            }
            else
            {
                yield return new WaitForSeconds(0.05f);
            }
        }
    }  
}