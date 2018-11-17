using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SkillPointsAdd : MonoBehaviour {
    //При клике добавляется очко в аттрибут (прокачка аттрибутов)

    public GameObject player;
    public UnitStats playerStats;
    public AudioSource audioSource;
    public AudioClip[] sounds;

    void Start() {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(OnClick);
        playerStats = player.GetComponent<UnitStats>();
    }

    void OnClick() {
        if (playerStats.UnitAttributes.skillPointsValue > 0) {
            if (gameObject.name == "+ButtonStrength") {
                float value = 1;
                playerStats.UnitAttributes.skillPointsValue -= value;
                playerStats.UnitAttributes.Strength.BaseValue += value;
                audioSource.PlayOneShot(sounds[Random.Range(0, sounds.Length)]);
            }

            if (gameObject.name == "+ButtonAgility") {
                float value = 1;
                playerStats.UnitAttributes.skillPointsValue -= value;
                playerStats.UnitAttributes.Agility.BaseValue += value;
                audioSource.PlayOneShot(sounds[Random.Range(0, sounds.Length)]);
            }

            if (gameObject.name == "+ButtonIntelligence") {
                float value = 1;
                playerStats.UnitAttributes.skillPointsValue -= value;
                playerStats.UnitAttributes.Intelligence.BaseValue += value;
                audioSource.PlayOneShot(sounds[Random.Range(0, sounds.Length)]);
            }

            if (gameObject.name == "+ButtonSpirit") {
                float value = 1;
                playerStats.UnitAttributes.skillPointsValue -= value;
                playerStats.UnitAttributes.Spirit.BaseValue += value;
                audioSource.PlayOneShot(sounds[Random.Range(0, sounds.Length)]);
            }

            if (gameObject.name == "+ButtonVitality") {
                float value = 1;
                playerStats.UnitAttributes.skillPointsValue -= value;
                playerStats.UnitAttributes.Vitality.BaseValue += value;
                audioSource.PlayOneShot(sounds[Random.Range(0, sounds.Length)]);
            }
        }
    }
}