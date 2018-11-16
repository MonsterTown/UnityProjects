using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[CreateAssetMenu(menuName = "PluggableAI/DeadBoss")]
public class DeadBoss : AbstractDead {
    public override void Dead(GameObject self) {
        DeadAction(self);
    }

    private void DeadAction(GameObject self) {
        self.GetComponent<GameObjectTags>().dead = true;
        self.GetComponent<Animator>().SetTrigger("Dying");

        //Disable scripts
        self.transform.root.gameObject.GetComponent<Rigidbody>().useGravity = false;
        self.transform.root.gameObject.GetComponent<CapsuleCollider>().enabled = false;
        self.transform.root.gameObject.GetComponent<ThirdPersonCharacter>().enabled = false;
        self.transform.root.gameObject.GetComponent<AIPath>().enabled = false;
        self.transform.root.gameObject.GetComponent<UnitStats>().Health.regenOn = false;
        GameObject hitBox = self.transform.root.gameObject.GetComponentInChildren<UnitStats>().hitBox;
        if (hitBox) {
            hitBox.gameObject.SetActive(false);
        } else {
            Debug.Log("NotFound");
        }
    }
}