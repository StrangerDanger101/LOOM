using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackedHandler : MonoBehaviour {
    [SerializeField]
    private GameObject flashbangObject;
    [SerializeField]
    private AudioSource attackSound;

    public void OnAttack(float duration, Sprite attackSprite) {
        //creates the attack sprite and plays the SFX
        flashbangObject.GetComponent<Image>().sprite = attackSprite;
        flashbangObject.SetActive(true);

        attackSound.Play();

        StopAllCoroutines();

        StartCoroutine(AttackEffect(duration));
    }

    IEnumerator AttackEffect(float duration) {
        //slowly makes the attack sprite more transperant over time
        float alpha = 1.0f;

        for(int i = 0; i < 10; i++) { 

            flashbangObject.GetComponent<Image>().color = new Color(255, 255, 255, alpha);
            alpha -= 0.3f;

            yield return new WaitForSeconds(duration / 3);
        }

        flashbangObject.SetActive(false);
        flashbangObject.GetComponent<Image>().color = new Color(255, 255, 255, 1);
    }
}
