using System.Collections;
using UnityEngine;

public class UnitOnMouseOver : MonoBehaviour
{
    void OnMouseEnter()
    {
        GameController.instance.GetComponent<GameController>().healthBarEnemy.SetActive(true);
        GameController.instance.GetComponent<GameController>().healthBarEnemy.GetComponent<HealthBarEnemyScript>().target = transform.root.gameObject;
        GameController.instance.GetComponent<GameController>().healthBarEnemy.GetComponent<HealthBarEnemyScript>().SetTargetName();
    }

    void OnMouseExit()
    {
        GameController.instance.GetComponent<GameController>().healthBarEnemy.GetComponent<HealthBarEnemyScript>().target = null;
        StartCoroutine(HideHitBar());
    }

    IEnumerator HideHitBar()
    {
        yield return new WaitForSeconds(1);
    }
}