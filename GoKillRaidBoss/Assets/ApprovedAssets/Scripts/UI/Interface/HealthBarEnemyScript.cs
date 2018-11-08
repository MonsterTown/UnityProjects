﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//Показывает полоску жизни врага если есть цель
public class HealthBarEnemyScript : MonoBehaviour
{
    private GameObject target;
    private Text textField;

    float HealthCur;
    float HealthMax;

    private void Start()
    {
        textField = GetComponentInChildren<Text>();
    }

    void Update()
    {
        DisplayBar();
    }

    private void DisplayBar()
    {
        if (target != null)
        {
            HealthCur = target.GetComponent<UnitStats>().HealthCur;
            HealthMax = target.GetComponent<UnitStats>().HealthMax;
            GetComponent<Image>().fillAmount = HealthCur / HealthMax;
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public void SetTarget(GameObject target)
    {
        this.target = target;
        textField.text = target.name;
    }
}