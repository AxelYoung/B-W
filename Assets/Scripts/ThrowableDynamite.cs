using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableDynamite : MonoBehaviour {

    public Vector2 forcePos;
    public float throwForce;
    public Sprite explosion;
    public Collider2D initCol;
    public Collider2D explosionCol;
    public float damage;
    public float knockback;

	// Use this for initialization
	void Start () {
        gameObject.GetComponent<Rigidbody2D>().AddForce(forcePos * throwForce);
        gameObject.GetComponent<Rigidbody2D>().AddTorque(throwForce);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Plat" || collision.tag == "Enemy" || collision.tag == "Bullet")
        {
            StartCoroutine(Explode());
        }
        if(collision.tag == "Enemy")
        {
            collision.GetComponent<Enemy>().Hit(damage, knockback, gameObject);
        }
    }

    public IEnumerator Explode()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
        gameObject.transform.localScale = new Vector2(5,5);
        gameObject.transform.eulerAngles = new Vector3(0,0,0);
        gameObject.GetComponent<Rigidbody2D>().freezeRotation = true;
        gameObject.GetComponent<SpriteRenderer>().sprite = explosion;
        initCol.enabled = false;
        explosionCol.enabled = true;
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
