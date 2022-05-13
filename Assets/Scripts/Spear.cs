using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : MonoBehaviour {

    public float dmg;
    public float knockback;
    public bool thrust;
    public float thrustLength;
    public float thrustTime;
    public PlayerMovement player;
    public AmmoBar ab;
    public float ammoAmount;
    public Vector2 thrustPos1;
    public Vector2 thrustPos2;
    private float currentTime;
    public bool returnThrust;

    // Use this for initialization
    void Start()
    {
        gameObject.GetComponent<Collider2D>().enabled = false;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        Vector2 dir = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);
        if (player.reverse == false)
        {
            transform.up = dir;
        }
        if (player.reverse == true)
        {
            transform.up = -dir;
        }
        if (Input.GetKeyDown(KeyCode.Mouse0) && ab.isHolding == false)
        {
            if (ab.ammo >= ammoAmount)
            {
                StartCoroutine(ThrustSpear());
            }
        }
        if (thrust)
        {
            currentTime += Time.deltaTime;
            float amount = currentTime / (thrustTime / 2);
            gameObject.transform.localPosition = Vector2.Lerp(thrustPos1, thrustPos2, amount);
        }
    }

    public IEnumerator ThrustSpear()
    {
        gameObject.GetComponent<Collider2D>().enabled = true;
        ab.ammo -= ammoAmount;
        thrustPos1 = gameObject.transform.localPosition;
        thrustPos2 = gameObject.transform.localPosition + (gameObject.transform.up * thrustLength);
        currentTime = 0;
        thrust = true;
        yield return new WaitForSeconds(thrustTime / 2);
        gameObject.GetComponent<Collider2D>().enabled = false;
        thrust = false;
        gameObject.transform.localPosition = thrustPos2;
        thrustPos2 = thrustPos1;
        thrustPos1 = gameObject.transform.localPosition;
        currentTime = 0;
        thrust = true;
        yield return new WaitForSeconds(thrustTime / 2);
        thrust = false;
        gameObject.transform.localPosition = thrustPos2;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Enemy>().Hit(dmg, knockback, gameObject);
        }
    }
}
