using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAngle : MonoBehaviour {

    [SerializeField]
    private GameObject enemyList;

    void Update() {
        UpdateAllAngles();
    }

    //returns all the children in enemyList
    private GameObject[] GetChildren() {
        GameObject[] childList = new GameObject[enemyList.transform.childCount];

        for (int i = 0; i < childList.Length; i++)
        {
            childList[i] = enemyList.transform.GetChild(i).gameObject;
        }

        return childList;
    }

    void UpdateAllAngles() {
        GameObject[] enemyList = GetChildren();

        //loops through all the gameobjects in enemylist
        for(int i = 0; i < enemyList.Length; i++) {
            GameObject enemy = enemyList[i];

            //sets enemy layer to "target"
            enemy.layer = LayerMask.NameToLayer("Target");
            //gets direction of enemy
            Vector3 direction = enemy.transform.position - transform.position;

            int mask = LayerMask.GetMask("Target");

            RaycastHit rayData;

            //fires a raycast to the enemy to find the side of the square hit then changing the enemy sprite according to the side the player faces
            if (Physics.Raycast(transform.position, direction, out rayData, Mathf.Infinity, mask)) {
                Vector3 contactPoint = rayData.transform.InverseTransformPoint(rayData.point).normalized;
   
                float upDir = Vector3.Dot(contactPoint, Vector3.up);
                float frontDir = Vector3.Dot(contactPoint, Vector3.forward);
                float rightDir = Vector3.Dot(contactPoint, Vector3.right);

                float upDist = Mathf.Abs(upDir);
                float frontDist = Mathf.Abs(frontDir);
                float rightDist = Mathf.Abs(rightDir);

                string spriteDir = "";

                if(rightDist < upDist  && frontDist < upDist) {
                    spriteDir = "Front";
                }
                else if (upDist < rightDist && frontDist < rightDist) {
                    if (rightDir > 0) {
                        spriteDir = "Right";
                    }
                    else {
                        spriteDir = "Left";
                    }
                }
                else if (upDist < frontDist && rightDist < frontDist) {
                    if(frontDir > 0) {
                        spriteDir = "Front";
                    }
                    else {
                        spriteDir = "Back";
                    }
                }

                enemy.transform.GetChild(0).GetComponent<EnemySpriteHandler>().ChangeDirection(spriteDir);
            };
        }
    }
}
