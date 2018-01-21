using UnityEngine;

public class GameController : MonoBehaviour {

    public static GameObject instance;

    //UI Элементы
    public GameObject healthBarEnemy;
    public GameObject menuCharacter;

    void Awake () {
        instance = this.gameObject; //Ссылка на обьект GameSettings статическая
    }

    void Update() {

        if (Input.GetKeyDown(KeyCode.I)) {
            if (menuCharacter.activeSelf) {
                menuCharacter.SetActive(false);
            } else {
                menuCharacter.SetActive(true);
            }
        }
    }
}
