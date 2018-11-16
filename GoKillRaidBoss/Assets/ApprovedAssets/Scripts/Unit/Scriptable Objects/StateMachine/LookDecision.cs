using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/Look")]
public class LookDecision : Decision {
    public override bool Decide(StateController controller) {
        bool targetVisible = Look(controller);
        return targetVisible;
    }

    private bool Look(StateController controller) {
        controller.enemies.Clear(); //Очистка списка врагов в контроллере каждый фрейм

        Collider[] hitColliders = Physics.OverlapSphere(controller.transform.position, controller.GetComponent<UnitStats>().lookRange);

        int i = 0;
        while (i < hitColliders.Length) {
            if (hitColliders[i].gameObject.transform.root.gameObject != controller.transform.root.gameObject                             /*Не сам*/
                && hitColliders[i].gameObject.GetComponent<GameObjectTags>()                                                             //Есть компонент?
                && hitColliders[i].gameObject.GetComponent<GameObjectTags>().unit == true                                                //Является юнитом
                && hitColliders[i].gameObject.GetComponent<GameObjectTags>().dead == false                                               //не мертвый
                && hitColliders[i].gameObject.GetComponent<GameObjectTags>().fraction != controller.unitStats.gameObjectTags.fraction) { //*Не союзники (обьекты с одинаковыми тегами - фракциями)*/ 

                controller.enemies.Add(hitColliders[i].transform.gameObject); //заполнение списка врагов в контроллере каждый фрейм
            }

            i++;
        }

        if (controller.GetComponent<UnitStats>().AttackersList.attackers != null) {
            foreach (GameObject item in controller.GetComponent<UnitStats>().AttackersList.attackers) {
                if (!controller.enemies.Contains(item)                              //Еще нет в листе?
                    && item.gameObject.GetComponent<GameObjectTags>()               //Есть компонент?
                    && item.gameObject.GetComponent<GameObjectTags>().unit == true  //Является юнитом
                    && item.gameObject.GetComponent<GameObjectTags>().dead == false //не мертвый
                    && item.gameObject.GetComponent<GameObjectTags>().fraction != controller.unitStats.gameObjectTags.fraction) {
                    //*Не союзники (обьекты с одинаковыми тегами - фракциями)*/ 

                    controller.enemies.Add(item); //заполнение списка врагов в контроллере каждый фрейм
                }
            }
        }

        if (controller.enemies.Count != 0) {
            return true;
        }

        return false;
    }
}