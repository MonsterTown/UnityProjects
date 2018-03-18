using UnityEngine;

public class GameController : MonoBehaviour {

    public static GameObject instance;

    //UI Элементы
    public GameObject healthBarEnemy;
    public GameObject menuCharacter;
    public MobileControllerCharacterMenuButton guiCharMenuButton;
    public MobileControllerCharacterMenuButtonClose guiCharMenuButtonClose;

    void Awake () {
        instance = this.gameObject; //Ссылка на обьект GameSettings статическая
    }

    void Update() {

        ButtonCharacterMenu();
    }

    public void ButtonCharacterMenu() {

        if (Input.GetKeyDown(KeyCode.I )|| guiCharMenuButton.trigger) {

            Debug.Log(guiCharMenuButton.trigger + "  " + guiCharMenuButtonClose.input);

            if (menuCharacter.activeSelf) {
                menuCharacter.SetActive(false);
            } else {
                menuCharacter.SetActive(true);
            }
        }

        if (guiCharMenuButtonClose.trigger ) {
            
            menuCharacter.SetActive(false);
        }
    }
}
