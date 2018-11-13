using System.Collections;
using UnityEngine;

//Класс получения урона
public class HealthOLD : MonoBehaviour
{
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
//    private float healthMax;
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
//    public bool regenOn = true; //включает / выключает регенрацию
//    
//    public Health(GameObject self)
//    {
//        this.self = self;
//        stats = self.GetComponent<UnitStats>();
//    }
//    
//    IEnumerator Regeneration()
//    {
//        while (regenOn)
//        {
//            HealthCur += HealthRegen * 0.05f;
//            yield return new WaitForSeconds(0.05f);
//        }
//    }
}
