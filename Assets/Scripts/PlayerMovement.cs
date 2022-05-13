using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {

    // The movement speed of the player
    public float movementSpeed;
    // The jump height of the player
    public float jumpHeight;
    // The ray released when the player jumps (used to dictate a single jump)
    public GameObject ray;
    // The distance between ray and target
    public float rayDistance;
    // If the player is walking
    public bool walking;
    // The players rigid body 2d component
    private Rigidbody2D rb;
    // The players sprite renderer component
    private SpriteRenderer sr;
    // The players animator component
    public Animator anim;
    // The average gravity scale after the player performs a jump
    public float gravityScale;
    // The g
    public float lowGravScale;
    public bool doorCheck;
    public GameObject finishMenu;
    public GameObject doorWall;
    public Animator parentAnim;
    public GameObject deathScreen;
    public int coins;
    public Text coinAmount;
    private float x;
    public bool jumped;
    public Tilemap levelMap;
    public Color blueTile;
    public Color redTile;
    public Color greenTile;
    public Color belowPlayerColor;
    public Vector3Int belowPlayerTilePos;
    public GameObject player;
    public bool startAnim;
    public Vector3 startPos;
    public bool needPlayerColorChangeWhite;
    public bool needPlayerColorChangeColor;
    public bool needTileColorChangeColor;
    public bool needTileColorChangeWhite;
    public GameObject particleSys;
    public Color initialColor;
    public bool reverse;
    public Animator cam;
    public DataKeep dk;
    public GameObject dataKeep;
    public GameObject objectHeld;
    public int currentHeld;
    public GameObject[] holdableObjects;
    public Vector2[] rightHoldPos;
    public Vector2[] leftHoldPos;
    // Use this for initialization
    void Start () {
        if (GameObject.Find("DataKeep") == null)
        {
            GameObject obj =Instantiate(dataKeep);
            obj.name = "DataKeep";
            dk = GameObject.Find("DataKeep").GetComponent<DataKeep>();
        }
        else
        {
            dk = GameObject.Find("DataKeep").GetComponent<DataKeep>();
        }
        cam = GameObject.Find("Background").GetComponent<Animator>();
        startAnim = true;
        player = gameObject.transform.parent.gameObject;
        startPos = gameObject.transform.position;
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        doorCheck = false;
        if (particleSys != null)
        {
            particleSys.GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingLayerName = "foreground";
            particleSys.GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingOrder = 2;
        }

    }

    // Update is called once per frame
    void Update() {
        if(Input.GetAxis("Mouse ScrollWheel") != 0 || Input.GetKeyDown(KeyCode.Q))
        {
            if(currentHeld < holdableObjects.Length - 1)
            {
                currentHeld++;
            }
            else
            {
                currentHeld = 0;
            }
            objectHeld = holdableObjects[currentHeld];
            if(currentHeld == 0)
            {
                holdableObjects[0].SetActive(true);
                holdableObjects[1].SetActive(false);
                holdableObjects[2].SetActive(false);
            }
            if (currentHeld == 1)
            {
                holdableObjects[1].SetActive(true);
                holdableObjects[0].SetActive(false);
                holdableObjects[2].SetActive(false);
            }
            if (currentHeld == 2)
            {
                holdableObjects[2].SetActive(true);
                holdableObjects[1].SetActive(false);
                holdableObjects[0].SetActive(false);
            }
        }
        if (!startAnim)
        {
            if (Input.GetKey(KeyCode.A))
            {
                XN1();
            }
            if (Input.GetKey(KeyCode.D))
            {
                X1();
            }
            if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
            {
                X0();
            }
            if (Input.GetKeyDown(KeyCode.Space) && anim.GetBool("Grounded") == true)
            {
                Jump();
                jumped = true;
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                jumped = false;
            }
        }
        RaycastHit2D groundCheck = Physics2D.Raycast(ray.transform.position, Vector2.down, rayDistance);
        if (groundCheck.collider != null)
        {
            anim.SetBool("Grounded", true);
        }
        else
        {
            anim.SetBool("Grounded", false);
        }
        if (!Input.GetKey(KeyCode.LeftShift))
        {
            rb.velocity = new Vector2(x * movementSpeed, rb.velocity.y);
        }
        if (rb.velocity.x > 0)
        {
            if (reverse)
            {
                sr.flipX = true;
                objectHeld.transform.localPosition = leftHoldPos[currentHeld];
            }
            else
            {
                sr.flipX = false;
                objectHeld.transform.localPosition = rightHoldPos[currentHeld];
            }
            
        }
        if (rb.velocity.x < 0)
        {
            if (reverse)
            {
                sr.flipX = false;
            }
            else
            {
                sr.flipX = true;
            }
            
        }
        if (x != 0)
        {
            anim.SetBool("Walking", true);
        }
        else
        {
            anim.SetBool("Walking", false);
        }
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (gravityScale - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !jumped)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowGravScale - 1) * Time.deltaTime;
        }

        if (doorCheck && Input.GetKeyDown(KeyCode.W) && anim.GetBool("Grounded") == true && !reverse)
        {
            StartCoroutine(LevelFinish());
        }
        if (Input.GetKeyDown(KeyCode.W) && !Input.GetKey(KeyCode.Space) && levelMap.GetTile(belowPlayerTilePos) != null)
        {
            SwapColor();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        CheckTileBelow();
        if(belowPlayerColor == blueTile)
        {
            jumpHeight = 15;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                cam.SetInteger("Flash", 1);
                StartCoroutine(ResetFlash());
            }
        }
        if (belowPlayerColor == redTile || belowPlayerColor == new Color(0.59f,0,0))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                cam.SetInteger("Flash", 2);
                StartCoroutine(ResetFlash());
                if (!reverse)
                {
                    anim.SetBool("Flip", true);
                    rb.gravityScale = -5;
                    jumpHeight = 10;
                    StartCoroutine(UpsideDown());
                }
                if (reverse)
                {
                    anim.SetBool("Realign", true);
                    rb.gravityScale = 1.25f;
                    reverse = false;
                    StartCoroutine(Realign());
                }
            }
        }
        if(belowPlayerColor == Color.white && !reverse)
        {
            jumpHeight = 10;
        }
        if (belowPlayerColor == Color.white && reverse)
        {
            jumpHeight = 10;
        }
        if (jumped)
        {
            anim.SetBool("Jump", true);
        }
        else
        {
            anim.SetBool("Jump", false);
        }
        if (needPlayerColorChangeWhite)
        {
            sr.color = Color.Lerp(sr.color, Color.white, 0.3f);
        }
        if(needPlayerColorChangeColor)
        {
            sr.color = Color.Lerp(sr.color, initialColor, 0.3f);
        }
        if (needTileColorChangeColor)
        {
            levelMap.SetColor(belowPlayerTilePos, Color.Lerp(belowPlayerColor, initialColor, 0.3f));
        }
        if (needTileColorChangeWhite)
        {
            levelMap.SetColor(belowPlayerTilePos, Color.Lerp(belowPlayerColor, Color.white, 0.3f));
        }
        if (!reverse)
        {
            objectHeld.GetComponent<SpriteRenderer>().flipY = false;
            if (objectHeld.transform.rotation.eulerAngles.z < 180)
            {
                objectHeld.GetComponent<SpriteRenderer>().flipX = true;
                sr.flipX = true;
                objectHeld.transform.localPosition = leftHoldPos[currentHeld];
            }
            if (objectHeld.transform.rotation.eulerAngles.z > 180)
            {
                objectHeld.GetComponent<SpriteRenderer>().flipX = false;
                sr.flipX = false;
                objectHeld.transform.localPosition = rightHoldPos[currentHeld];
            }
        }
        if (reverse)
        {
            objectHeld.GetComponent<SpriteRenderer>().flipY = true;
            if (objectHeld.transform.rotation.eulerAngles.z < 180)
            {
                objectHeld.GetComponent<SpriteRenderer>().flipX = false;
                sr.flipX = true;
                objectHeld.transform.localPosition = leftHoldPos[currentHeld];
            }
            if (objectHeld.transform.rotation.eulerAngles.z > 180)
            {
                objectHeld.GetComponent<SpriteRenderer>().flipX = true;
                sr.flipX = false;
                objectHeld.transform.localPosition = rightHoldPos[currentHeld];
            }
        }
    }

    public IEnumerator ResetFlash()
    {
        yield return new WaitForSeconds(.333f);
        cam.SetInteger("Flash", 0);
    }

    public void LateUpdate()
    {
        if (reverse)
        {
            gameObject.transform.localRotation = new Quaternion(0, 0, 180, 0);
        }
        if(gameObject.transform.rotation == new Quaternion(0,0,180,0) && rb.gravityScale == 1.25f)
        {
            gameObject.transform.localRotation = new Quaternion(0, 0, 0, 0);
        }
    }

    public IEnumerator UpsideDown()
    {
        yield return new WaitForSeconds(.17f);
        anim.SetBool("Flip", false);
        reverse = true;
    }

    public IEnumerator Realign()
    {
        yield return new WaitForSeconds(.17f);
        anim.SetBool("Realign", false);
    }

    public void CheckTileBelow()
    {
        if (!reverse)
        {
            belowPlayerTilePos = levelMap.LocalToCell(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - .75f, 0));
        }
        if(reverse)
        {
            belowPlayerTilePos = levelMap.LocalToCell(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + .75f, 0));
        }
        belowPlayerColor = levelMap.GetColor(belowPlayerTilePos);
        if(belowPlayerColor != Color.white)
        {
            levelMap.SetTileFlags(belowPlayerTilePos, TileFlags.None);
            particleSys.GetComponent<ParticleSystem>().startColor = new Color(belowPlayerColor.r, belowPlayerColor.g, belowPlayerColor.b, .5f);
            particleSys.SetActive(true);
        }
        else if(particleSys != null)
        {
            particleSys.SetActive(false);
        }
    }

    public void StartAnimFin()
    {
        startAnim = false;
    }

    public void SwapColor()
    {
        if(sr.color == Color.white)
        {
            if (belowPlayerColor == blueTile)
            {
                initialColor = blueTile;
                StartCoroutine(ColorSwapToPlayer());
            }
            if(belowPlayerColor == redTile)
            {
                initialColor = redTile;
                StartCoroutine(ColorSwapToPlayer());
            }
            if(belowPlayerColor == greenTile)
            {
                initialColor = greenTile;
                StartCoroutine(ColorSwapToPlayer());
            }
        } else if(belowPlayerColor == Color.white)
        {
            StartCoroutine(ColorSwapToTile());
        }

    }

    public IEnumerator ColorSwapToPlayer()
    {
        needPlayerColorChangeColor = true;
        needTileColorChangeWhite = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        yield return new WaitForSeconds(0.25f);
        rb.constraints = RigidbodyConstraints2D.None;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        needPlayerColorChangeColor = false;
        needTileColorChangeWhite = false;
        sr.color = initialColor;
        levelMap.SetColor(belowPlayerTilePos, Color.white);
    }

    public IEnumerator ColorSwapToTile()
    {
        needTileColorChangeColor = true;
        needPlayerColorChangeWhite = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        yield return new WaitForSeconds(0.25f);
        rb.constraints = RigidbodyConstraints2D.None;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        needTileColorChangeColor = false;
        needPlayerColorChangeWhite = false;
        sr.color = Color.white;
        levelMap.SetColor(belowPlayerTilePos, initialColor);
    }

    public void X1()
    {
        x = 1;
    }

    public void XN1()
    {
        x = -1;
    }

    public void X0()
    {
        x = 0;
    }

    public void Jump()
    {
        rb.AddForce(this.transform.up * jumpHeight, ForceMode2D.Impulse);
    }

    public void JumpHeld()
    {
        jumped = true;
    }

    public void JumpReleased()
    {
        jumped = false;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Obst" || collision.transform.tag == "Enemy")
        {
            if(GetComponent<Dash>().invinsible == false)
            {
                StartCoroutine(Death());
            }
            if(GetComponent<Dash>().invinsible == true && collision.transform.tag == "Enemy")
            {
                collision.gameObject.GetComponent<Enemy>().Hit(GetComponent<Dash>().dmg, GetComponent<Dash>().knockback, gameObject);
            }
        }
    }

    public IEnumerator Death()
    {
        gameObject.GetComponent<Collider2D>().enabled = false;
        dk.deaths += 1;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        //deathScreen.SetActive(true);
        anim.SetBool("Death", true);
        yield return new WaitForSeconds(.5f);
        GameObject newPlayer = Instantiate(player);
        newPlayer.transform.GetChild(0).GetComponent<Collider2D>().enabled = true;
        newPlayer.transform.GetChild(0).transform.position = startPos;
        newPlayer.transform.GetChild(0).GetComponent<PlayerMovement>().reverse = false;
        newPlayer.transform.GetChild(0).GetComponent<Rigidbody2D>().gravityScale = 1.25f;
        newPlayer.transform.GetChild(0).GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        newPlayer.transform.GetChild(0).GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        yield return new WaitForSeconds(.25f);
        Destroy(gameObject.transform.parent.gameObject);
    }

    public IEnumerator LevelFinish()
    {
        rb.constraints = RigidbodyConstraints2D.None;
        rb.constraints = RigidbodyConstraints2D.FreezePosition;
        if(coins != 1)
        {
            coinAmount.text = "You got " + coins + " points!";
        } else
        {
            coinAmount.text = "You got " + coins + " point!";
        }

        //doorWall.SetActive(true);
        sr.flipX = false;
        parentAnim.enabled = true;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        anim.enabled = false;
        parentAnim.SetBool("Goal", true);
        yield return new WaitForSeconds(2);
        finishMenu.SetActive(true);
    }
}
