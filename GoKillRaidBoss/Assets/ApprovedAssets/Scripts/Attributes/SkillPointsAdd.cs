using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SkillPointsAdd : MonoBehaviour {

    //При клике добавляется очко в аттрибут (прокачка аттрибутов)

    public GameObject player;

    public AudioSource audioSource;
    public AudioClip[] sounds;

    void Start() {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(OnClick);
    }

    void OnClick() {

        if (player.GetComponent<UnitStats>().skillPointsValue > 0) {
            if (gameObject.name == "+ButtonStrength") {
                float value = 1;
                player.GetComponent<UnitStats>().Strength.BaseValue += value;
                player.GetComponent<UnitStats>().skillPointsValue -= value;
                audioSource.PlayOneShot(sounds[Random.Range(0, sounds.Length)]);
            }
            if (gameObject.name == "+ButtonAgility") {
                float value = 1;
                player.GetComponent<UnitStats>().Agility.BaseValue += value;
                player.GetComponent<UnitStats>().skillPointsValue -= value;
                audioSource.PlayOneShot(sounds[Random.Range(0, sounds.Length)]);
            }
            if (gameObject.name == "+ButtonIntelligence") {
                float value = 1;
                player.GetComponent<UnitStats>().Intelligence.BaseValue += value;
                player.GetComponent<UnitStats>().skillPointsValue -= value;
                audioSource.PlayOneShot(sounds[Random.Range(0, sounds.Length)]);
            }
            if (gameObject.name == "+ButtonSpirit") {
                float value = 1;
                player.GetComponent<UnitStats>().Spirit.BaseValue += value;
                player.GetComponent<UnitStats>().skillPointsValue -= value;
                audioSource.PlayOneShot(sounds[Random.Range(0, sounds.Length)]);
            }
            if (gameObject.name == "+ButtonVitality") {
                float value = 1;
                player.GetComponent<UnitStats>().Vitality.BaseValue += value;
                player.GetComponent<UnitStats>().skillPointsValue -= value;
                audioSource.PlayOneShot(sounds[Random.Range(0, sounds.Length)]);
            }
        }
    }
}