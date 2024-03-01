using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileHandler : MonoBehaviour {

    public float speed;

    private Rigidbody rigidBody; 

    void Start() {
        rigidBody = GetComponent<Rigidbody>();

        Destroy(gameObject, 10f);
    }

    //makes the projectile move forward in according to its speed every frame
    void Update() {
        rigidBody.velocity = transform.forward * speed;
    }

    //if collided with a GameObject, destroy this projectile
    private void OnCollisionEnter(Collision collision) {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider collision) {
        GameObject collided = collision.gameObject;

        //if the collision includes an enemy
        if(collided.CompareTag("Enemy")) {
            //if the enemy is a native species
            if(collided.GetComponent<CollisionHandler>().badMove()) {
                //add a penalty counter
                GameSystem.PenaltyAttack();
            }

            //sends collision to CollisionHandler
            collided.GetComponent<CollisionHandler>().CollisionEvent();
        }

        Destroy(gameObject);
    }
}
