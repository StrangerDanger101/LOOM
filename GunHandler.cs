using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunHandler : MonoBehaviour {
    
    public GameObject projectile;
    public GameObject wasteFolder;

    public GameObject camObj;
    private Camera cam;

    [SerializeField]
    private int maxAmmo;
    private int currentAmmo;

    [SerializeField]
    private float debounce;
    private bool canReload;

    [SerializeField]
    private Sprite fireSprite;
    [SerializeField]
    private Sprite stillSprite;



    void Start() {
        cam = camObj.GetComponent<Camera>();
        currentAmmo = maxAmmo;
        canReload = true;
    }

    void Update() {
        //if there is ammo and the player has pressed the left mouse button
        if (currentAmmo > 0 && Input.GetMouseButtonDown(0)) {
            OnMouseClick();
        }

        //if the player can reload and has pressed "R"
        if (Input.GetKeyDown(KeyCode.R) && canReload) {
            Reload();
        }
    }

    void OnMouseClick() {
        //changes the sprite of the gun
        StartCoroutine(ChangeSprite());
        
        //finds the direction the player is facing and fires a projectile towards that direction
        Vector3 direction = cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 10f));

        Quaternion rotation = Quaternion.LookRotation(direction - camObj.transform.position, Vector3.up);

        Instantiate(projectile, camObj.transform.position, camObj.transform.rotation, wasteFolder.transform);

        //uses up 1 ammo
        currentAmmo--;

        GameSystem.UpdateAmmo(currentAmmo);
    }

    void Reload() {
        //reloads the ammo back to normal
        currentAmmo = maxAmmo;
        GameSystem.UpdateAmmo(currentAmmo);

        canReload = false;

        StartCoroutine(Cooldown());
    }

    IEnumerator ChangeSprite() {
        GameSystem.FireGunModel(fireSprite);

        yield return new WaitForSeconds(0.1f);

        GameSystem.FireGunModel(stillSprite);
    }

    IEnumerator Cooldown() {
        yield return new WaitForSeconds(debounce);

        canReload = true;
    }
}
