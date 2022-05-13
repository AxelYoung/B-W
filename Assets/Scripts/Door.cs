using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour {

    public GameObject doorWall;
    public int nextLvl;
    private PlayerMovement pm;
    public Sprite[] bkgRand;
    public GameObject bkg;
    public bool off;

    public void Start()
    {
    }

    private void Update()
    {
        bkg = GameObject.Find("Background");
        if (bkg != null & !off)
        {
            bkg.GetComponent<SpriteRenderer>().sprite = bkgRand[Random.Range(0, bkgRand.Length)];
            off = true;
        }
        if (pm != null && pm.finishMenu.activeInHierarchy && Input.GetKeyDown(KeyCode.W))
        {
            NextLevel();
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            pm = collision.GetComponent<PlayerMovement>();
            pm.doorCheck = true;
            pm.doorWall = doorWall;

        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            pm.doorCheck = false;
        }
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(nextLvl);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
