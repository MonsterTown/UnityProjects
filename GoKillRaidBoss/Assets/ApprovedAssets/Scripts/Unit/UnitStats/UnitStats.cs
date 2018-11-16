using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStats : MonoBehaviour {
    //Компоненты
    [HideInInspector] public Animator animator;
    [HideInInspector] public GameObjectTags gameObjectTags; //ссылка на свои теги
    public GameObject hitBox;                               //ссылка на дочерний хитбокс
    public AbstractDead deadScript;
    public AbstractAttack attackScript;
    [HideInInspector] public TakingDamage TakingDamage;
    [HideInInspector] public AttackersList AttackersList;
    [SerializeField] public Health Health;

    #region Fields

    //Attack
    [Space(1, order = 0)] [Header("Attack", order = 1)] [Space(4, order = 2)]
    public float lookRange;

    public float attackRange;
    public float attackDamage;
    public float minDamage = 0.8f;
    public float maxDamage = 1.2f;

    //Defence
    public float armor;

    #endregion

    //Init
    void Awake() {
        animator = GetComponent<Animator>();
        gameObjectTags = transform.root.gameObject.GetComponent<GameObjectTags>();
        TakingDamage = new TakingDamage(transform.root.gameObject);
        AttackersList = transform.root.gameObject.AddComponent<AttackersList>();
        AttackersList.Init(transform.root.gameObject);
        Health = transform.root.gameObject.AddComponent<Health>();
    }

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
            transform.LookAt(GetComponent<StateController>().targetForChaseOrAttack.transform); //поворот к цели во время атаки
        }
    }

    #endregion

    #region Dead

    public void Dead() {
        deadScript.Dead(gameObject);
    }

    #endregion
}