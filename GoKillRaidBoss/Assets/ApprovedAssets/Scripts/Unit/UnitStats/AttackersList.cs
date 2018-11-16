using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Когда юнит получает урон он добавляется в список тех кто его атакнул, если врагов во круге нет, он пойдет искать обидчика в течении 10 сек.
public class AttackersList : MonoBehaviour {
    public List<GameObject> attackers = new List<GameObject>();

    private GameObjectTags selfTags;

    public void Init(GameObject self) {
        selfTags = self.GetComponent<GameObjectTags>();
    }

    public void AddAttackerToEnemyList(GameObject attacker) {
        if (!attackers.Contains(attacker)                                                        //Еще нет в листе?
            && attacker.gameObject.transform.root.gameObject != transform.root.gameObject        /*Не сам*/
            && attacker.gameObject.GetComponent<GameObjectTags>()                                //Есть компонент? 
            && attacker.gameObject.GetComponent<GameObjectTags>().unit == true                   //Является юнитом 
            && attacker.gameObject.GetComponent<GameObjectTags>().dead == false                  //не мертвый 
            && attacker.gameObject.GetComponent<GameObjectTags>().fraction != selfTags.fraction) //*Не союзники (обьекты с одинаковыми тегами - фракциями)
        {
            attackers.Add(attacker);

            StartCoroutine(RemoveAttackerFromList(attacker));
        }
    }

    IEnumerator RemoveAttackerFromList(GameObject attacker) {
        yield return new WaitForSeconds(10f);
        attackers.Remove(attacker);
    }
}