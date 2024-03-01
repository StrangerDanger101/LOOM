using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpriteHandler : MonoBehaviour {

    [SerializeField]
    private Sprite backSprite;
    [SerializeField]
    private Sprite frontSprite;
    [SerializeField]
    private Sprite leftSprite;
    [SerializeField]
    private Sprite rightSprite;

    public Sprite attackSprite;


    void Update() {
        FacePlayer();
    }

    void FacePlayer() {
        //enemy sprite faces the player
        Vector3 direction = GameSystem.GetPlayer().transform.position - transform.position;

        transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
    }

    public void ChangeDirection(string direction) {
        //changes sprite based on the direction the player is facing
        switch(direction) {
            case "Front":
                GetComponent<SpriteRenderer>().sprite = frontSprite;
                break;
            case "Left":
                GetComponent<SpriteRenderer>().sprite = leftSprite;
                break;
            case "Right":
                GetComponent<SpriteRenderer>().sprite = rightSprite;
                break;
            case "Back":
                GetComponent<SpriteRenderer>().sprite = backSprite;
                break;
        }
    }
}
