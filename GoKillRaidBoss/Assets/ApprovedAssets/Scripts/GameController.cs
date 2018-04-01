using UnityEngine;

public class GameController : MonoBehaviour {

    public static GameObject instance;

    //UI Элементы
    public GameObject healthBarEnemy;
    public GameObject menuCharacter;
    public GameObject playerTabContainer;
    public GameObject abilitiesTabContainer;
    public GameObject inventoryTabContainer;
    public MobileControllerCharacterMenuButton guiCharMenuButton;
    public MobileControllerCharacterMenuButtonClose guiCharMenuButtonClose;
    public MobileControllerCharacterMenuAbilityTab guiCharMenuAbilityTab;
    public MobileControllerCharacterMenuInventoryTab guiCharMenuInventoryTab;
    public MobileControllerCharacterMenuPlayerTab guiCharMenuPlayerTab;

    void Awake () {
        instance = this.gameObject; //Ссылка на обьект GameSettings статическая
    }

    void Update() {

        ButtonCharacterMenu();
        // AbilityTab();
        Tabs();
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

    public void Tabs() {
        if (guiCharMenuAbilityTab.trigger) {
            playerTabContainer.SetActive(false);
            abilitiesTabContainer.SetActive(true);
            inventoryTabContainer.SetActive(false);
        }
        if (guiCharMenuInventoryTab.trigger) {
            playerTabContainer.SetActive(false);
            abilitiesTabContainer.SetActive(false);
            inventoryTabContainer.SetActive(true);
        }
        if (guiCharMenuPlayerTab.trigger) {
            playerTabContainer.SetActive(true);
            abilitiesTabContainer.SetActive(false);
            inventoryTabContainer.SetActive(false);
        }
    }

    //public void AbilityTab() {
    //    if (guiCharMenuAbilityTab.trigger) {

    //        Debug.Log(guiCharMenuAbilityTab.trigger + "  " + guiCharMenuAbilityTab.input);

    //        if (playerTabContainer.activeSelf) {
    //            playerTabContainer.SetActive(false);
    //            abilitiesTabContainer.SetActive(true);
    //            inventoryTabContainer.SetActive(true);
    //        } else {
    //            playerTabContainer.SetActive(true);
    //            abilitiesTabContainer.SetActive(false);
    //            inventoryTabContainer.SetActive(false);
    //        }
    //    }
    //}

    //public void InventoryTab() {
    //    if (guiCharMenuInventoryTab.trigger) {

    //        if (inventoryTabContainer.activeSelf) {
    //            playerTabContainer.SetActive(true);
    //            abilitiesTabContainer.SetActive(true);
    //            inventoryTabContainer.SetActive(false);
    //        } else {
    //            playerTabContainer.SetActive(true);
    //            abilitiesTabContainer.SetActive(false);
    //            inventoryTabContainer.SetActive(false);
    //        }
    //    }
    //}
}
