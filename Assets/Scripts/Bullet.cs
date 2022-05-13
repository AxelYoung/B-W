using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    // Amount of damage a bullet does
    public float dmg;
    // Amount of knockback a bullet does
    public float knockback;
    // Direction a bullet should go (based on if player is upside down)
    private Vector3 dir;

	// Use this for initialization
	void Start () {
        // Start the destroy after time coroutine
        StartCoroutine(DestroyAfterTime());
        // If the player is not reversed
        if (GameObject.Find("Player").GetComponent<PlayerMovement>().reverse == false)
        {
            // Bullet inherently goes up
            dir = transform.up;
        }
        // If the player is reversed
        if (GameObject.Find("Player").GetComponent<PlayerMovement>().reverse == true)
        {
            // Bullet inherently goes down
            dir = -transform.up;
        }
    }
	
	// Update is called once per frame
	void Update () {

        // Bullet transform is equal to previous bullet transform plus the direction it needs to go times a time delta and a speed boost constant of 10
        transform.position += dir * Time.deltaTime * 10;
    }

    // Checks if something has collided with the bullet
    public void OnTriggerEnter2D(Collider2D collision)
    {
        // If the collision is a platform or obstacle (part of the arena)
        if(collision.tag == "Plat" || collision.tag == "Obst")
        {
            // Destroy the gameobject
            Destroy(gameObject);
        }
        // If the collision is the enemy
        if(collision.tag == "Enemy")
        {
            // Run the hit command on enemy side with dmg, knockback, and gameobject doing damage passed on
            collision.gameObject.GetComponent<Enemy>().Hit(dmg, knockback, gameObject);
            // Destroy the gameobject
            Destroy(gameObject);
        }
    }

    // Destroys gameobject after a few seconds to avoid clutter and lag
    public IEnumerator DestroyAfterTime()
    {
        // Wait five seconds
        yield return new WaitForSeconds(5);
        // Destroy gameobject
        Destroy(gameObject);
    }
}
