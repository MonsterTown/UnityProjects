using UnityEngine;

public class GameSettingsScript : MonoBehaviour {

    public static GameObject healthBarEnemy;
    public static HealthBarEnemyScript healthBarEnemyScript;

    void Start () {
        healthBarEnemy = GameObject.Find("HealthBarEnemy");
        healthBarEnemyScript = healthBarEnemy.GetComponent<HealthBarEnemyScript>();
    }
}
