using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[CreateAssetMenu(menuName = "PluggableAI/Player/DeadPlayer")]
public class DeadPlayer : AbstractDead {


    public override void Dead(GameObject self) {
        DeadPlayerAction(self);
    }

    private void DeadPlayerAction(GameObject self) {  
        self.GetComponent<GameObjectTags>().dead = true;
        self.GetComponent<Animator>().SetTrigger("Dying");
        self.transform.root.gameObject.GetComponent<UnitStats>().Health.regenOn = false; //выключает регенерацию хп

        //Disable scripts      
        self.transform.root.gameObject.GetComponent<CharacterController>().enabled = false;
        self.transform.root.gameObject.GetComponent<PlayerController>().enabled = false;
        self.transform.root.gameObject.GetComponent<CharacterMechanics>().enabled = false;

        GameObject hitBox = self.transform.root.gameObject.GetComponentInChildren<UnitStats>().hitBox;
        if (hitBox) {
            hitBox.gameObject.SetActive(false);
        } else {
            Debug.Log("NotFound");
        }
    }
}
