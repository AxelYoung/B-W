using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBar : MonoBehaviour {

    // The amount of ammo available to the player
    public float ammo;
    // The amount of ammo the player can have at any time
    public float ammoMax;
    // The speed of the reloading process
    public float reloadSpeed;
    // The amount of ammo reloaded during the reloading process
    public float reloadAmount;
    // If the player is holding the key to reload
    public bool isHolding;

    //The reloading process
    public IEnumerator Reload()
    {
        // Wait for the amount of time to reload
        yield return new WaitForSeconds(reloadSpeed);
        if (isHolding)
        {
            // If ammo is less than ammo max amount (there is something to reload)
            if (ammo < ammoMax)
            {
                // Add the amount to reload to the ammo amount
                ammo += reloadAmount;
            }
            // Start the reloading process over again
            StartCoroutine(Reload());
        }
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());
            isHolding = true;
        }
        if (Input.GetKeyUp(KeyCode.R))
        {
            isHolding = false;
        }
        // The scale of the ammo bar is equal to the fraction of ammo available over ammo max, or the percentage, over a constant of 1 unit y axis.
        gameObject.transform.localScale = new Vector2(ammo / ammoMax, 1);
	}
}
