using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {

    public bool interval;
    public bool off;
    public float intervalTime;
    public GameObject[] laser;
    private Animator anim;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        anim.SetBool("On", true);
        if (interval)
        {
            StartCoroutine(Timer());
        }
	}

    public void Update()
    {
        if (off)
        {
            Off();
        }
        else
        {
            On();
        }
    }

    public IEnumerator Timer()
    {
        yield return new WaitForSeconds(intervalTime);
        off = true;
        yield return new WaitForSeconds(intervalTime);
        off = false;
        StartCoroutine(Timer());
    }

    public void Off()
    {
        anim.SetBool("On", false);
        for (int i = 0; i < laser.Length; i++)
        {
            laser[i].SetActive(false);
        }
    }

    public void On()
    {
        anim.SetBool("On", true);
        for (int i = 0; i < laser.Length; i++)
        {
            laser[i].SetActive(true);
        }
    }
}
