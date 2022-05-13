using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public Transform[] pos;
    public GameObject enemy;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());
    }

    public IEnumerator Spawn()
    {
        yield return new WaitForSeconds(1);
        Instantiate(enemy, pos[Random.Range(0, pos.Length - 1)].position, Quaternion.identity);
        StartCoroutine(Spawn());
    }
}
