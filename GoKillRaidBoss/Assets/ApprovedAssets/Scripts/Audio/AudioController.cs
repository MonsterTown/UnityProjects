using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {
    public AudioController audioController;

    public AudioSource audioSource;

    public AudioClip[] attackSounds;
    public AudioClip[] hitSounds;
    public AudioClip[] deathSounds;

    private void Awake() {
        if (!audioController) {
            audioController = this;
        }

        if (false) {
            // audioSource.mute = true;
        }
    }

    public void PlayAttackSounds() {
        if (attackSounds.Length > 0) {
            audioSource.PlayOneShot(attackSounds[Random.Range(0, attackSounds.Length)]);
        }
    }

    public void PlayHitSounds() {
        if (hitSounds.Length > 0) {
            audioSource.PlayOneShot(hitSounds[Random.Range(0, hitSounds.Length)]);
        }
    }

    public void PlayDeathSounds() {
        if (deathSounds.Length > 0) {
            audioSource.PlayOneShot(deathSounds[Random.Range(0, deathSounds.Length)]);
        }
    }
}