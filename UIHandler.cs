using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIHandler : MonoBehaviour {
    public void OnStartButtonPress() {
        SceneManager.LoadScene("Gameplay");
    }

    public void OnTutorialButtonPress() {
        transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(true);
    }

    public void OnTutorialExit() {
        transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(false);
    }

    public void OnBackPress() {
        SceneManager.LoadScene("MainMenu");
    }
}
