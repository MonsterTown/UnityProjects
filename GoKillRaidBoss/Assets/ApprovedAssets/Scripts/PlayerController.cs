using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    UnitStats unitStats;
    Animator animator;
    
    CharacterMechanics characterMechanics;

    //Инициализация перемененных для кнопок
    public MobileControllerAttack guiAttackButton;
    public MobileControllerRoll guiRollButton;
    public MobileControllerCharacterMenuButton guiMenuButton;

    //Для отслеживания мыши над гуи элементами.
    public GraphicRaycaster gr;
    PointerEventData ped = new PointerEventData(null);
    public List<RaycastResult> results = new List<RaycastResult>();
    bool mouseOverGUI;


    private void Awake() {
        unitStats = GetComponent<UnitStats>();
        animator = GetComponent<Animator>();
        characterMechanics = GetComponent<CharacterMechanics>();
        gr = gr.GetComponent<GraphicRaycaster>();
    }

    void Update() {
        animator.ResetTrigger("Attack");
        PlayerMobileController();
    }

    //For Android

    private void PlayerMobileController() {

        UpdateMouseOverGUI();

        if (Input.GetKeyDown(KeyCode.Space) || guiRollButton.input ) {  //Приказ на кувырок
            characterMechanics.doDive = true;
        }

        if (guiAttackButton.input) {
            OrderAttackFromButton();
            animator.SetTrigger("Attack");
        } else if (Input.GetMouseButtonDown(0) && !mouseOverGUI ){
            GetComponent<UnitStats>().attackScript.targetAttack = null;
            OrderAttackFromDisplayClick();
            animator.SetTrigger("Attack");           
        }

        mouseOverGUI = false;
    }



    public void UpdateMouseOverGUI() {
        ped.position = Input.mousePosition;
        gr.Raycast(ped, results);
        foreach (RaycastResult o in results) {
            if (o.gameObject.name == guiRollButton.name ||
                o.gameObject.name == guiMenuButton.name) {
                mouseOverGUI = true;
                break;
            }
        }
        results.Clear();
    }

    public void OrderAttackFromDisplayClick() {

        RaycastHit hit;
        int layerMaskUnit = LayerMask.GetMask("Unit"); //Рейкаст только в слой  Unit (только)

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, layerMaskUnit)) { //Если под мышкой есть юнит то стрела летит в него

            GetComponent<UnitStats>().attackScript.targetAttack = hit.transform.gameObject;
        } 
    }

    public void OrderAttackFromButton() {

        int layerMaskGround = LayerMask.GetMask("Default"); //Рейкаст только в слой  Default (только)
        int layerMaskUnit = LayerMask.GetMask("Unit"); //Рейкаст только в слой  Unit (только)

        GameObject target = NearestTarget(gameObject, 30);
        GetComponent<UnitStats>().attackScript.targetAttack = target;

        if (target) {
            SetHealthBarEnemy(target);
        }
    }

    private GameObject NearestTarget(GameObject player, float distance) {  //Нахождения ближайшего врага в радиусе

        GameObject nearestTarget = null;

        Collider[] hitColliders = Physics.OverlapSphere(player.transform.position, distance);

        int i = 0;
        while (i < hitColliders.Length) {

            if (hitColliders[i].gameObject.transform.root.gameObject != player.transform.root.gameObject        /*Не сам*/
             && hitColliders[i].gameObject.GetComponent<GameObjectTags>()                     //Есть компонент?
             && hitColliders[i].gameObject.GetComponent<GameObjectTags>().unit == true        //Является юнитом
             && hitColliders[i].gameObject.GetComponent<GameObjectTags>().dead == false       //не мертвый
             && hitColliders[i].gameObject.GetComponent<GameObjectTags>().fraction != player.GetComponent<GameObjectTags>().fraction) {  //*Не союзники (обьекты с одинаковыми тегами - фракциями)*/ 

                if (nearestTarget == null) {
                    nearestTarget = hitColliders[i].transform.gameObject;
                }

                float distance1 = Vector3.Distance(player.transform.position, hitColliders[i].transform.position);
                float distance2 = Vector3.Distance(player.transform.position, nearestTarget.transform.position);

                if (distance1 < distance2) {
                    nearestTarget = hitColliders[i].transform.gameObject;
                }
            }

            i++;
        }   
        return nearestTarget;
    }

    public void SetHealthBarEnemy(GameObject target) {

        GameController.instance.GetComponent<GameController>().healthBarEnemy.SetActive(true);
        GameController.instance.GetComponent<GameController>().healthBarEnemy.GetComponent<HealthBarEnemyScript>().target = target;
        GameController.instance.GetComponent<GameController>().healthBarEnemy.GetComponent<HealthBarEnemyScript>().SetTargetName();
    }
}
