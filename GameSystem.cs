using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSystem : MonoBehaviour {

    private static int health = 100;
    private static int penaltiesRemaining = 3;

    private static GameObject healthDisplay;
    private static GameObject penaltyDisplay;
    private static GameObject ammoDisplay;
    private static GameObject timerDisplay;
    private static GameObject gunDisplay;

    private static AudioSource treeAttackSound;
    private static AudioSource fireSound;
    private static AudioSource reloadSound;
    
    void Start() {
        healthDisplay = GameObject.FindGameObjectWithTag("HealthDisplay");
        penaltyDisplay = GameObject.FindGameObjectWithTag("PenaltyDisplay");
        ammoDisplay = GameObject.FindGameObjectWithTag("AmmoDisplay");
        timerDisplay = GameObject.FindGameObjectWithTag("TimerDisplay");
        gunDisplay = GameObject.FindGameObjectWithTag("GunDisplay");
        treeAttackSound = GameObject.FindGameObjectWithTag("TreeAttack").GetComponent<AudioSource>();
        fireSound = GameObject.FindGameObjectWithTag("Fire").GetComponent<AudioSource>();
        reloadSound = GameObject.FindGameObjectWithTag("Reload").GetComponent<AudioSource>();
    }

    public static void AttackOnPlant(int enemyDamage) {
        //decreases the game health by the enemy damage
        health -= enemyDamage;
        treeAttackSound.Play();

        //if game health getes to 0 then lose
        if(health <= 0) {
            LoseGame();
        }

        healthDisplay.GetComponent<TextMeshProUGUI>().text = health.ToString();
    }

    public static void PenaltyAttack() {
        penaltiesRemaining--;

        //if penalties get to 0 then lose 
        if(penaltiesRemaining <= 0) {
            LoseGame();
        }

        penaltyDisplay.GetComponent<TextMeshProUGUI>().text = penaltiesRemaining.ToString();
    }

    public static void UpdateAmmo(int newAmmo) {
        reloadSound.Play();
        ammoDisplay.GetComponent<TextMeshProUGUI>().text = newAmmo.ToString();
    }

    public static void UpdateTimer(int newTime) {
        timerDisplay.GetComponent<TextMeshProUGUI>().text = newTime.ToString();
    }

    public static void FireGunModel(Sprite sprite) {
        fireSound.Play();
        gunDisplay.GetComponent<Image>().sprite = sprite;
    }

    public static GameObject GetPlayer() {
        return GameObject.FindGameObjectWithTag("Player");
    }

    public static void WinGame() {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        SceneManager.LoadScene("WinScreen");
    }

    public static void LoseGame() {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        SceneManager.LoadScene("LoseScreen");
    }
}
