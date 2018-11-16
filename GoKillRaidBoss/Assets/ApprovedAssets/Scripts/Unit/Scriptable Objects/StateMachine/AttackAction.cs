using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Attack")]
public class AttackAction : Action {
    public override void Act(StateController controller) {
        Attack(controller);
    }

    private void Attack(StateController controller) {
        Animator animator = controller.GetComponent<Animator>();

        animator.SetBool("Move", false);
        controller.GetComponent<AIDestinationSetter>().target = null;
        controller.GetComponent<AIPath>().canMove = false;

        animator.SetInteger("Attack", Random.Range(1, 8));
    }
}