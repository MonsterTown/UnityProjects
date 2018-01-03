using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorSounds : StateMachineBehaviour {

    public GameObject owner;

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        owner = animator.gameObject;
        HitSound(stateInfo);
        DeathSound(stateInfo);
        AttackSound(stateInfo);
    }

    void HitSound(AnimatorStateInfo stateInfo) {

        if (stateInfo.IsTag("Hit")) {

            owner.GetComponentInChildren<AudioController>().audioController.PlayHitSounds();
        }
    }

    void DeathSound(AnimatorStateInfo stateInfo) {

        if (stateInfo.IsTag("Death")) {

            owner.GetComponentInChildren<AudioController>().audioController.PlayDeathSounds();
        }
    }

    void AttackSound(AnimatorStateInfo stateInfo) {

        if (stateInfo.IsTag("Attack")) {

            owner.GetComponentInChildren<AudioController>().audioController.PlayAttackSounds();
        }
    }
}
