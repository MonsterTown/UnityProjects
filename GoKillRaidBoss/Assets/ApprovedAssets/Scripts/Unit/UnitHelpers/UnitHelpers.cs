using UnityEngine;

//Класс-хелпер для статических методов для юнита.
public class UnitHelpers : MonoBehaviour
{
    //Нахождения ближайшего врага в радиусе
    public static GameObject FindClosestEnemyUnit(GameObject player, float distance) {
        
        //Нахождения ближайшего врага в радиусе
        GameObject closestEnemyUnit = null;

        Collider[] hitColliders = Physics.OverlapSphere(player.transform.position, distance);

        int i = 0;
        while (i < hitColliders.Length) {

            if (hitColliders[i].gameObject.transform.root.gameObject != player.transform.root.gameObject        /*Не сам*/
                && hitColliders[i].gameObject.GetComponent<GameObjectTags>()                     //Есть компонент?
                && hitColliders[i].gameObject.GetComponent<GameObjectTags>().unit == true        //Является юнитом
                && hitColliders[i].gameObject.GetComponent<GameObjectTags>().dead == false       //не мертвый
                && hitColliders[i].gameObject.GetComponent<GameObjectTags>().fraction != player.GetComponent<GameObjectTags>().fraction) {  //*Не союзники (обьекты с одинаковыми тегами - фракциями)*/ 

                if (closestEnemyUnit == null) {
                    closestEnemyUnit = hitColliders[i].transform.gameObject;
                }

                float distance1 = Vector3.Distance(player.transform.position, hitColliders[i].transform.position);
                float distance2 = Vector3.Distance(player.transform.position, closestEnemyUnit.transform.position);

                if (distance1 < distance2) {
                    closestEnemyUnit = hitColliders[i].transform.gameObject;
                }
            }

            i++;
        }
        return closestEnemyUnit;
    }
}