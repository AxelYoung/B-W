using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.GetComponent<PlayerMovement>().coins += 1;
            GameObject.Find("DataKeep").GetComponent<DataKeep>().coins += 1;
            Destroy(gameObject);
        }
    }
}
