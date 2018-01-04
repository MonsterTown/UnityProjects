using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/SetClosestEnemy")]
public class SetClosestEnemyAction : Action {
    public override void Act(StateController controller) {
        SetClosestEnemy(controller);
    }

    private void SetClosestEnemy(StateController controller) {

        controller.targetForChaseOrAttack = ClosestUnit(controller, controller.enemies);
    }

    GameObject ClosestUnit(StateController controller, List<GameObject> enemies) {
        GameObject closestUnit = null;
        float distance = float.MaxValue;
        foreach (var item in enemies) {
            if (item != null && Vector3.Distance(item.transform.position, controller.transform.position) < distance) {
                distance = Vector3.Distance(item.transform.position, controller.transform.position);
                closestUnit = item.transform.gameObject;
            }
        }

        return closestUnit;
    }
}