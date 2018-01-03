using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Idle")]
public class IdleAction : Action {
    public override void Act(StateController controller) {
        Idle(controller);
    }

    private void Idle(StateController controller) {
        //Отдыхаем переключаем анимацию на idle убираем таргеты и тд все обнуляем
        controller.GetComponent<Animator>().SetBool("Move", false);
        controller.GetComponent<Animator>().SetInteger("Attack", 0);
        controller.targetForChaseOrAttack = null;
        controller.GetComponent<AIPath>().target = null;
        controller.GetComponent<AIPath>().canMove = false;
    }
}