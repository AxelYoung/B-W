using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    public float dmg;
    public float knockback;
    public float dashLength = 625;
    public float ammoAmount;
    public AmmoBar ab;
    public float length;
    public bool invinsible;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            if (Input.GetKey(KeyCode.LeftShift) && ab.ammo >= ammoAmount)
            {
                ab.ammo -= ammoAmount;
                rb.AddForce(dashLength * new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")));
                StartCoroutine(DashDamage());
            }
        }
    }

    public IEnumerator DashDamage()
    {
        invinsible = true;
        yield return new WaitForSeconds(length);
        invinsible = false;
    }
}
