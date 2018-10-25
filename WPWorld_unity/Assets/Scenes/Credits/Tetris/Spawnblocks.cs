using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnblocks : MonoBehaviour
{

    public GameObject block;
    // Use this for initialization
    private int randx;
    private int randy;
    private int randz;
    void Start()
    {
        StartCoroutine(Example());
    }

    IEnumerator Example()
    {
        print(Time.time);
        randrot();
        Instantiate(block, this.gameObject.transform.position, this.gameObject.transform.rotation);

        //new GameObject = Instantiate(block);
        yield return new WaitForSecondsRealtime(2);
        print(Time.time);
        StartCoroutine(Example());
    }
    // Update is called once per frame
    void Update()
    {

        // StartCoroutine(Example());
        //Instantiate(block);
    }
    void randrot()
    {
        randx = Random.Range(0, 4);
        randy = Random.Range(0, 4);
        randz = Random.Range(0, 4);
        this.gameObject.transform.Rotate(randx * 90, randy * 90, randz * 90);
    }
}
