using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "PluggableAI/Player/AttackPlayerRanged")]
public class AttackPlayerRanged : AbstractAttack {
    private PlayerController playerController;

    //создание префаба стрелы при выстреле
    public Rigidbody rocket;
    public GameObject onImpactEffect;
    public float speed = 50f;

    public override void Attack(GameObject self) {
        AttackAction(self);
    }

    private void AttackAction(GameObject self) { //Attack With No Target (GroundAttack)

        float attackDamage = self.transform.root.gameObject.GetComponent<UnitStats>().attackDamage;
        float minDamage = self.transform.root.gameObject.GetComponent<UnitStats>().minDamage;
        float maxDamage = self.transform.root.gameObject.GetComponent<UnitStats>().maxDamage;
        float damage = attackDamage * UnityEngine.Random.Range(minDamage, maxDamage);
        float secondVector = 1f;
        float distance = 0f;
        float size = 35f;

        RaycastHit hit;
        int layerMaskGround = LayerMask.GetMask("Default"); //Рейкаст только в слой  Default (только)

        if (targetAttack) { //Если есть цель, эта переменная в родительском классе, а туда она пападает из PlayerControll
            Vector3 direct = new Vector3(targetAttack.transform.position.x, self.transform.position.y, targetAttack.transform.position.z);
            self.transform.LookAt(direct);
            self.transform.Rotate(Vector3.up * 0.5f);

            secondVector = (targetAttack.transform.position.y - targetAttack.transform.position.y) / (Vector3.Distance(targetAttack.transform.position, self.transform.position) / speed);
            distance = Vector3.Distance(targetAttack.transform.position, self.transform.position);
            size = targetAttack.GetComponent<CapsuleCollider>().height;
        } else if (self.GetComponent<PlayerController>().guiAttackButton.input) { } else {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, layerMaskGround)) { //если под мышкой земля то стрела летит прямо)
                Vector3 direct = new Vector3(hit.point.x, self.transform.position.y, hit.point.z);
                self.transform.LookAt(direct);
                self.transform.Rotate(Vector3.up * 0.5f);
            }
        }

        //Выстрел стрелы
        Vector3 pos = new Vector3(self.transform.position.x, self.transform.position.y + 3, self.transform.position.z - 0.5f);
        Rigidbody rocketClone = (Rigidbody) Instantiate(rocket, pos, self.transform.rotation);
        rocketClone.GetComponent<ProjectileScript>().ownerProjectile = self;          //Устанавливает в поле снаряда владельца того кто выпустил снаряд.
        rocketClone.GetComponent<ProjectileScript>().onImpactEffect = onImpactEffect; //Устанавливает визуальный эффект после попадания в цель
        rocketClone.GetComponent<ProjectileScript>().damage = damage;                 //Передача стреле урон
        rocketClone.velocity = self.transform.forward * speed + self.transform.up * ((secondVector + (0.1f * distance)) * size) * 0.2f +
                               self.transform.right * -0.5f; // Стрела летит вперед*50 + параболическая формула + поправка то что стрела отправляется чуть правее (не с центра модельки)
    }
}