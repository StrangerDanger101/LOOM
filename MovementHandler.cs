using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementHandler : MonoBehaviour {

    private Rigidbody rigidBody;

    public float gravitySpeed;
    public float playerSpeed;

    void Start() {
        rigidBody = GetComponent<Rigidbody>();
    }

    void Update() {
        InputToMovement();
        Gravity();
    }

    void InputToMovement() {
        //turns player input into in game translations
        float xDirection = Input.GetAxis("Horizontal") * Time.fixedDeltaTime * playerSpeed;
        float zDirection = Input.GetAxis("Vertical") * Time.fixedDeltaTime * playerSpeed;

        Vector3 velocity = transform.forward * zDirection;
        velocity += transform.right * xDirection;

        rigidBody.velocity = velocity;
    }

    void Gravity() {
        //moves the player down if the player is not touching a ground
        Ray ray = new Ray(transform.position, -transform.up);

        bool raycast = Physics.Raycast(ray, 1.05f);

        if(raycast) {
            return;
        }
        else {
            transform.Translate(-transform.up*gravitySpeed*Time.deltaTime);
        }
    }
}
