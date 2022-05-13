using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

    public GameObject bullet;
    public GameObject bulletPos;
    public bool canFire;
    public float fireRate;
    public PlayerMovement player;
    public AmmoBar ab;
    public float ammoAmount;

	// Use this for initialization
	void Start () {
        canFire = true;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
	}
	
	// Update is called once per frame
	void Update () {
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
            if (canFire && ab.ammo >= ammoAmount)
            {
                StartCoroutine(ShootBullet());
            }
        }
    }

    public IEnumerator ShootBullet()
    {
        ab.ammo -= ammoAmount;
        Instantiate(bullet, bulletPos.transform.position, transform.rotation);
        canFire = false;
        yield return new WaitForSeconds(fireRate);
        canFire = true;
    }
}
