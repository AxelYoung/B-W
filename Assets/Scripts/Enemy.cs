using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float health;
    public float maxHealth;
    public Animator anim;
    public GameObject player;
    public Vector2 pos;
    public Vector2 playerPos;
    public Rigidbody2D rb;
    public int x;
    public float movementSpeed;
    public float jumpHeight;
    public GameObject ray;
    public float rayDistance;
    public bool grounded;
    public float sight;
    public bool canJump;
    public bool canMove;
    public GameObject healthBar;

	// Use this for initialization
	void Start () {
        maxHealth = health;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        canJump = true;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        player = GameObject.Find("Player");
        pos = gameObject.transform.position;
        playerPos = player.transform.position;
        RaycastHit2D groundCheck = Physics2D.Raycast(ray.transform.position, Vector2.down, rayDistance);
        if (groundCheck.collider != null)
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }
        CheckPlayerLocation();
		if(health <= 0)
        {
            Destroy(gameObject);
        }
        healthBar.transform.localScale = new Vector2(1 * (health / maxHealth), 1);
	}

    public void Hit(float dmg, float knockback, GameObject knockbackPos)
    {
        KnockbackEnemy(knockback, knockbackPos);
        StartCoroutine(HitTime(dmg));
    }

    public void KnockbackEnemy(float knockback, GameObject knockbackPos)
    {
        if (grounded)
        {
            x = 0;
            rb.AddForce(knockbackPos.transform.up * knockback * 50);
            print("Grounded Knockback");
        }
        if (!grounded)
        {
            x = 0;
            rb.AddForce(knockbackPos.transform.up * knockback * 50);
            print("Grounded Knockback");
        }

    }

    public IEnumerator HitTime(float dmg)
    {
        anim.SetBool("Hit", true);
        health -= dmg;
        yield return new WaitForSeconds(0.05f);
        anim.SetBool("Hit", false);
        yield return new WaitForSeconds(2.95f);
    }

    public void CheckPlayerLocation()
    {
        if (Vector2.Distance(pos, playerPos) < sight && canMove)
        {
            MoveTowardsPlayer();
        }
        else
        {
            x = 0;
            rb.velocity = new Vector2(x * movementSpeed, rb.velocity.y);
        }
    }

    public void MoveTowardsPlayer()
    {
        if (playerPos.x < pos.x)
        {
            x = -1;
        }
        if (playerPos.x > pos.x)
        {
            x = 1;
        }
        rb.AddForce(Vector2.right * x * movementSpeed);
        StartCoroutine(CheckPosSame());
    }

    public IEnumerator CheckPosSame()
    {
        Vector2 oldPos = gameObject.transform.position;
        yield return new WaitForSeconds(0.1f);
        if (pos.normalized == oldPos.normalized && grounded && canJump)
        {
            print("Jumped");
            rb.AddForce(this.transform.up * jumpHeight, ForceMode2D.Impulse);
            canJump = false;
            StartCoroutine(Jumped());
        }
    }

    public IEnumerator Jumped()
    {
        yield return new WaitForSeconds(0.1f);
        RaycastHit2D groundCheck = Physics2D.Raycast(ray.transform.position, Vector2.down, rayDistance);
        if (groundCheck.collider != null)
        {
            canJump = true;
        }
        else
        {
            canJump = false;
            StartCoroutine(Jumped());
        }
    }
}
