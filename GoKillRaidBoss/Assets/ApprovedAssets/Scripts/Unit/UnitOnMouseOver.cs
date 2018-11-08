using System.Collections;
using UnityEngine;

public class UnitOnMouseOver : MonoBehaviour
{
    void OnMouseEnter()
    {
        GameController.instance.GetComponent<GameController>().healthBarEnemy.SetActive(true);
        GameController.instance.GetComponent<GameController>().healthBarEnemy.GetComponent<HealthBarEnemyScript>().SetTarget(transform.root.gameObject);
    }

    void OnMouseExit()
    {
        GameController.instance.GetComponent<GameController>().healthBarEnemy.GetComponent<HealthBarEnemyScript>().SetTarget(null);
        StartCoroutine(HideHitBar());
    }

    IEnumerator HideHitBar()
    {
        yield return new WaitForSeconds(1);
        //TODO: плавное затухание бара врага
    }
}