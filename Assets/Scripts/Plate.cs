using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : MonoBehaviour {

    public Sprite off;
    public Sprite on;
    public GameObject target;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" || collision.tag == "Box")
        {
            GetComponent<SpriteRenderer>().sprite = on;
            target.GetComponent<Laser>().off = true;
        }
    }


    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" || collision.tag == "Box")
        {
            GetComponent<SpriteRenderer>().sprite = on;
            target.GetComponent<Laser>().off = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player" || collision.tag == "Box")
        {
            GetComponent<SpriteRenderer>().sprite = off;
            target.GetComponent<Laser>().off = false;
        }
    }
}
