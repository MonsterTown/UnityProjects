using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthBarEnemyScript : MonoBehaviour
{
    public GameObject target;
    public string targetName;

    private Text textField;

    float HealthCur;
    float HealthMax;

    private void Start()
    {
        textField = GetComponentInChildren<Text>();
    }

    void Update()
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

    public void SetTargetName()
    {
        textField.text = target.name;
    }
}
