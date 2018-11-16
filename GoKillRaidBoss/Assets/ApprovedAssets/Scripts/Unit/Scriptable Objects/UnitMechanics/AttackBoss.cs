using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "PluggableAI/AttackBoss")]
public class AttackBoss : AbstractAttack {
    public float cleaveRange = 6;

    public override void Attack(GameObject self) {
        AttackAction(self);
    }

    private void AttackAction(GameObject self) {
        float attackDamage = self.transform.root.gameObject.GetComponent<UnitStats>().attackDamage;
        float minDamage = self.transform.root.gameObject.GetComponent<UnitStats>().minDamage;
        float maxDamage = self.transform.root.gameObject.GetComponent<UnitStats>().maxDamage;

        float damage = attackDamage * UnityEngine.Random.Range(minDamage, maxDamage);

        List<GameObject> enemiesAroundList = self.GetComponentInChildren<StateController>().enemies;

        foreach (var item in enemiesAroundList) {
            if (Vector3.Distance(item.transform.position, self.transform.position) < cleaveRange && isTargetFront(item.transform, self)) //собирает цели в радиусе 10
            {
                // item.GetComponent<UnitStats>().TakingDamage(self, damage);
                item.GetComponent<UnitStats>().TakingDamage.DoDamage(self, damage);
            }
        }
    }

    bool isTargetFront(Transform target, GameObject self) {
        Vector3 forward = self.transform.TransformDirection(Vector3.forward);
        Vector3 toTarget = target.position - self.transform.position;
        float angle = Vector3.Dot(forward, toTarget);
        if (angle > 0) {
            return true;
        }

        return false;
    }
}