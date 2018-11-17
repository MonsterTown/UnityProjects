using System.Collections;
using UnityEngine;

//Класс здоровья
public class Health : MonoBehaviour, Observer {
    private UnitStats stats;

    [SerializeField] private float healthCur = 1f;

    public float HealthCur {
        get { return healthCur; }
        set {
            if (value <= 0) {
                healthCur = 0;
                stats.Dead();
            } else if (value > HealthMax) healthCur = HealthMax;
            else healthCur = value;
        }
    }

    [SerializeField] private float healthMax = 1f;

    public float HealthMax {
        get { return healthMax; }
        set {
            if (value <= 0) {
                healthMax = 0;
                healthCur = 0;
            } else {
                float temp = healthCur / healthMax;
                healthMax = value;
                healthCur = healthMax * temp;
            }
        }
    }

    [SerializeField] private float healthRegen = 0.1f;

    public float HealthRegen {
        get { return healthRegen; }
        set { healthRegen = value; }
    }

    public bool regenOn = true; //включает / выключает регенрацию

    private void OnEnable() {
        stats = gameObject.GetComponent<UnitStats>();
        StartCoroutine(Regeneration());
    }

    IEnumerator Regeneration() {
        while (true) {
            if (regenOn) {
                HealthCur += HealthRegen * 0.05f;
                yield return new WaitForSeconds(0.05f);
            } else {
                yield return new WaitForSeconds(0.05f);
            }
        }
    }

    public void ObserverUpdate() {
         GetComponent<UnitStats>().UnitAttributes.HealthCalculate();
    }
}