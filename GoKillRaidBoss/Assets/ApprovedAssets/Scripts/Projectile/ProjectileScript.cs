using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour {

    public GameObject ownerProjectile;    //Владелец снаряда
    public GameObject onImpactEffect;     //Эффект взрыва стрелы
    public float damage;

    private void Start()
    {
        StartCoroutine(LifeTime());
    }

    public void OnImpact(GameObject targetHit)  //Вызывается при попадании в хитбокс с триггер коллайдером.
    {
        Vector3 pos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        GameObject gameEffect = Instantiate(onImpactEffect, pos, transform.rotation);
        Destroy(gameEffect, 2);
        Destroy(gameObject);
        //Нанесение урона
        targetHit.GetComponent<UnitStats>().TakingDamage(ownerProjectile, damage);
    }

    IEnumerator LifeTime()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
