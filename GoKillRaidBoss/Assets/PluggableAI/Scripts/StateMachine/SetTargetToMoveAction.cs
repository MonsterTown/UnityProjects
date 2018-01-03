using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/SetTargetToMove")]
public class SetTargetToMoveAction : Action {
    public override void Act(StateController controller) {
        SetTargetToMove(controller);
    }

    private void SetTargetToMove(StateController controller) {

        Animator animator = controller.GetComponent<UnitStats>().animator;

        animator.SetInteger("Attack", 0);
        if (animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Idle") {                   //Ждет пока закончится клип атаки
            controller.transform.root.gameObject.GetComponent<AIPath>().target = controller.targetForChaseOrAttack.transform;
            controller.transform.root.gameObject.GetComponent<AIPath>().canMove = true;
            animator.SetBool("Move", true);
        }
    }
}