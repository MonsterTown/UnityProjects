using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxScript : MonoBehaviour {
    //Название обьекта хитбокса
    private const string HitBoxName = "HitBox";

    private void OnTriggerEnter(Collider other) {
        FromProject(other);
    }

    //В коллайдер прилетает снаряд
    private void FromProject(Collider other) {
        GameObject projectile = other.transform.root.gameObject;
        GameObjectTags projectileTags = projectile.GetComponent<GameObjectTags>();
        ProjectileScript projectileScript = projectile.GetComponent<ProjectileScript>();

        if (
            transform.gameObject.name == HitBoxName &&                                //Прилетело в коллайдер хит бокса, а не в другие какие нибудь. (Может быть агроколлайдер и тд) выаываывф афыва ва
            transform.root.gameObject.GetComponent<GameObjectTags>().dead == false && //Не мертвый?
            projectileTags.projectile &&                                              //Является снарядом
            projectile != transform.root.gameObject &&                                /*Не сам*/
            !projectile.CompareTag(transform.root.gameObject.tag) &&                  /*Не союзники (обьекты с одинаковыми тегами - фракциями)*/
            projectileScript.ownerProjectile != transform.root.gameObject) {          /*Не свой снаряд*/
            projectileScript.OnImpact(transform.root.gameObject);
        }
    }
}

