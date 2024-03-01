using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour {
    [SerializeField]
    private string status;

    private AudioSource goodAttackSound;
    private AudioSource badAttackSound;

    void Start() {
        goodAttackSound = GameObject.FindGameObjectWithTag("Attack").GetComponent<AudioSource>();
        badAttackSound = GameObject.FindGameObjectWithTag("Miss").GetComponent<AudioSource>();
    }

    public void CollisionEvent() {
        Destroy(gameObject);
    }

    //retursn true if native returns false if invasive
    public bool badMove() {
        if(status.Equals("Native")) {
            badAttackSound.Play();
        }
        else {
            goodAttackSound.Play();
        }

        return status.Equals("Native");
    }
}
