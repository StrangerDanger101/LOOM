using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyMovement : MonoBehaviour {

    public int enemyDamage;
    public float enemySpeed;

    private GameObject targetList;
    private GameObject target;

    private Rigidbody rigidBody;

    [SerializeField]
    private bool canAttack;
    [SerializeField]
    private float attackRange;
    [SerializeField]
    private float attackDuration;

    void Start() {
        targetList = GameObject.FindWithTag("Plants");
        rigidBody = GetComponent<Rigidbody>();
        target = FindNearestTarget();

        RotateToTarget();
    }

    //moves towards the tree and attacks the player if needed
    void Update() {
        rigidBody.velocity = transform.forward * enemySpeed * Time.deltaTime;

        if(CanAttack()) {
            AttackPlayer();
        }
    }

    //gets all children in the targetList gameObject
    private Transform[] GetChildren() {
        Transform[] childList = new Transform[targetList.transform.childCount];

        for(int i = 0; i < childList.Length; i++) {
            childList[i] = targetList.transform.GetChild(i);
        }

        return childList;
    }

    //finds the nearest tree
    private GameObject FindNearestTarget() {
        Transform[] possibleTargets = GetChildren();
        GameObject nearestTarget = null;

        float highestDistance = 9999f;

        for (int i = 0; i < possibleTargets.Length; i++) {
            Transform currentTarget = possibleTargets[i];

            float distance = Vector3.Distance(transform.position, currentTarget.position);
        
            if(distance < highestDistance) {
                nearestTarget = currentTarget.gameObject;

                highestDistance = distance;
            } 
        }

        Debug.Log(nearestTarget.transform.position);
        return nearestTarget;
    }

    //rotates facing towards the tree
    private void RotateToTarget() {
        Vector3 direction = target.transform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction, transform.up);

        transform.rotation = rotation;
        Debug.Log(rotation);
    }

    //if collides with teh tree than call AttackOnPlant and destroy this object
    private void OnTriggerEnter(Collider collision) {
        GameObject collided = collision.gameObject; 

        if(collided.CompareTag("Plant")) {
            GameSystem.AttackOnPlant(enemyDamage);

            Destroy(gameObject);
        }
    }

    //attacks the player if they have the ability and they are close enough
    private bool CanAttack() {
        Vector3 playerPos = GameSystem.GetPlayer().transform.position;

        if(Vector3.Distance(transform.position, playerPos) <= attackRange) {
            return true;
        }

        return false;
    }

    private void AttackPlayer() {
        GameSystem.GetPlayer().GetComponent<AttackedHandler>().OnAttack(attackDuration, transform.GetChild(0).GetComponent<EnemySpriteHandler>().attackSprite);

        Destroy(gameObject);
    }
}
