using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxScript : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        FromProject(other);
    }

    //В коллайдер прилетает снаряд
    private void FromProject(Collider other)
    {
        if (transform.gameObject.name == "HitBox" &&             //Прилетело в коллайдер хит бокса, а не в другие какие нибудь. (Может быть агроколлайдер и тд)
            transform.root.gameObject.GetComponent<GameObjectTags>().dead == false &&        //Не мертвый?
            other.transform.root.gameObject.GetComponent<GameObjectTags>().projectile == true &&        //Является снарядом
            other.transform.root.gameObject != transform.root.gameObject &&  /*Не сам*/
            other.transform.root.gameObject.tag != transform.root.gameObject.tag &&  /*Не союзники (обьекты с одинаковыми тегами - фракциями)*/
            other.transform.root.gameObject.GetComponent<ProjectileScript>().ownerProjectile != transform.root.gameObject)      /*Не свой снаряд*/
        {
            other.transform.root.gameObject.GetComponent<ProjectileScript>().OnImpact(transform.root.gameObject);
        }
    }
}
