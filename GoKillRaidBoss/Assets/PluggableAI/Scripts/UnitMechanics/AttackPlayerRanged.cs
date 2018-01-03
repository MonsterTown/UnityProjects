using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "PluggableAI/Player/AttackPlayerRanged")]
public class AttackPlayerRanged : AbstractAttack {

    //создание префаба стрелы при выстреле
    public Rigidbody rocket;
    public GameObject onImpactEffect;
    public float speed = 50f;

    public override void Attack(GameObject self) {
        AttackAction(self);
    }

    private void AttackAction(GameObject self) {

        Animator animator = self.GetComponent<Animator>();

        float attackDamage = self.transform.root.gameObject.GetComponent<UnitStats>().attackDamage;
        float minDamage = self.transform.root.gameObject.GetComponent<UnitStats>().minDamage;
        float maxDamage = self.transform.root.gameObject.GetComponent<UnitStats>().maxDamage;

        float damage = attackDamage * UnityEngine.Random.Range(minDamage, maxDamage);

        RaycastHit hit;
        int layerMask = LayerMask.GetMask("Default"); //Рейкаст только в слой  Default (только)
        Vector3 direction;


        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, layerMask)) {
            direction = hit.point - self.transform.position;

            Debug.DrawRay(self.transform.position, direction, Color.white);



            Vector3 direct = new Vector3(hit.point.x, self.transform.position.y, hit.point.z);
            self.transform.LookAt(direct);
            self.transform.Rotate(Vector3.up * 0.5f);

            //Выстрел стрелы
            Vector3 pos = new Vector3(self.transform.position.x, self.transform.position.y + 3, self.transform.position.z - 0.5f);
            Rigidbody rocketClone = (Rigidbody)Instantiate(rocket, pos, self.transform.rotation);
            rocketClone.GetComponent<ProjectileScript>().ownerProjectile = self; //Устанавливает в поле снаряда владельца того кто выпустил снаряд.
            rocketClone.GetComponent<ProjectileScript>().onImpactEffect = onImpactEffect; //Устанавливает визуальный эффект после попадания в цель
            rocketClone.GetComponent<ProjectileScript>().damage = damage;  //Передача стреле урон
            rocketClone.velocity = self.transform.forward * speed + self.transform.up * 7f; // Стрела летит вперед со скоростью 50 и чуть вверх скоростью 7

        }
    }

}
