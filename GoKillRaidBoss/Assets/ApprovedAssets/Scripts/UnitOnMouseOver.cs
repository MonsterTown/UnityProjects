using System.Collections;
using UnityEngine;

public class UnitOnMouseOver : MonoBehaviour
{
    void OnMouseEnter()
    {
        GameSettingsScript.healthBarEnemy.SetActive(true);
        GameSettingsScript.healthBarEnemyScript.target = transform.root.gameObject;
        GameSettingsScript.healthBarEnemyScript.SetTargetName();
    }

    void OnMouseExit()
    {
        GameSettingsScript.healthBarEnemyScript.target = null;
        StartCoroutine(HideHitBar());
    }

    IEnumerator HideHitBar()
    {
        yield return new WaitForSeconds(1);
    }
}