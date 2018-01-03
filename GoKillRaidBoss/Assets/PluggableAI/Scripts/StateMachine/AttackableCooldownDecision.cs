using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/AttackableCooldown")]
public class AttackableCooldownDecision : Decision {

    public override bool Decide(StateController controller) {
        bool isAttackClipOver = AttackableCooldown(controller);
        return isAttackClipOver;
    }

    private bool AttackableCooldown(StateController controller) {

        float attackClipLenght = controller.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0].clip.length;

        if (!controller.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Attack")) {  //Играется ли сейчас клипы из стейта "Attack" ?
            return true;
        }
        return false;
    }
}