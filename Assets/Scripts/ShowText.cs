using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowText : MonoBehaviour {

    public GameObject txt;

    private void Start()
    {
        txt.SetActive(false);
        txt.GetComponentInChildren<PerCharacterController>().enabled = false;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            txt.SetActive(true);
            txt.GetComponentInChildren<PerCharacterController>().enabled = true;
            txt.GetComponentInChildren<PerCharacterController>().ShowText();
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            txt.SetActive(false);
            txt.GetComponentInChildren<PerCharacterController>().enabled = false;
        }
    }
}
