using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/AttackableDistance")]
public class AttackableDistanceDecision : Decision {
    public override bool Decide(StateController controller) {
        bool targetInRangeToAttack = AttackableDistance(controller);
        return targetInRangeToAttack;
    }

    private bool AttackableDistance(StateController controller) {
        if (controller.targetForChaseOrAttack != null
            && Vector3.Distance(controller.targetForChaseOrAttack.transform.position, controller.transform.position) <= controller.GetComponent<UnitStats>().attackRange) {
            return true;
        }

        return false;
    }
}