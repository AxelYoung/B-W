using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dynamite : MonoBehaviour
{

    public GameObject throwableDynamite;
    public bool canThrow;
    public float throwRate;
    public PlayerMovement player;
    public AmmoBar ab;
    public float ammoAmount;
    public float throwForce;
    public float damage;
    public float knockback;

    // Use this for initialization
    void Start()
    {
        canThrow = true;
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
        if (Input.GetKey(KeyCode.Mouse0) && ab.isHolding == false)
        {
            if (canThrow && ab.ammo >= ammoAmount)
            {
                StartCoroutine(Throw());
            }
        }
    }

    public IEnumerator Throw()
    {
        ab.ammo -= ammoAmount;
        GameObject currentDynamite = Instantiate(throwableDynamite, gameObject.transform.position, transform.rotation);
        currentDynamite.GetComponent<ThrowableDynamite>().throwForce = throwForce;
        currentDynamite.GetComponent<ThrowableDynamite>().forcePos = gameObject.transform.up;
        currentDynamite.GetComponent<ThrowableDynamite>().damage = damage;
        currentDynamite.GetComponent<ThrowableDynamite>().knockback = knockback;
        canThrow = false;
        yield return new WaitForSeconds(throwRate);
        canThrow = true;
    }
}
