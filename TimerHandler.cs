using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerHandler : MonoBehaviour {
    [SerializeField]
    private int timeLeft;

    void Start() {
        StartCoroutine(Timer());
    }

    IEnumerator Timer() {
        //decreases the amount of time left by one second until max seconds and then game is won
        while(timeLeft > 0) {
            yield return new WaitForSeconds(1);

            timeLeft--;

            GameSystem.UpdateTimer(timeLeft);
        }

        GameSystem.WinGame();
    }
}
