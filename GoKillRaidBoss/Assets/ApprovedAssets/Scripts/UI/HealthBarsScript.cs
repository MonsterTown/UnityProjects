using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthBarsScript : MonoBehaviour
{
    public GameObject player;

    string barName;
    float HealthCur;
    float HealthMax;


    void Start()
    {
        barName = transform.name;
    }

    void Update()
    {
        if (barName == "HealthCur")
        {
            HealthCur = player.GetComponent<UnitStats>().HealthCur;
            HealthMax = player.GetComponent<UnitStats>().HealthMax;
            GetComponent<Image>().fillAmount = HealthCur / HealthMax;
        }
    }
}
