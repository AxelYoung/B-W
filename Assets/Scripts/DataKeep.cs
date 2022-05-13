using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataKeep : MonoBehaviour {

    public int coins;
    public int deaths;
    public Text coinTxt;
    public Text deathTxt;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update () {
        coinTxt = GameObject.Find("CoinsTxt").GetComponent<Text>();
        deathTxt = GameObject.Find("DeathsTxt").GetComponent<Text>();
        
        coinTxt.text = "x" + coins;
        deathTxt.text = "x" + deaths;
	}
}
