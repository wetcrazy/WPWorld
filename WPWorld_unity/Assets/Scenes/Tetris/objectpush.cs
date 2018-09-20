using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectpush : MonoBehaviour
{


    bool letmepush;
    bool stopme;
    private int randnum;
    // Use this for initialization
    void Start()
    {
        letmepush = false;
        stopme = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (letmepush)
        {
            switch (randnum)
            {

                case 1:
                    this.gameObject.transform.Translate(Vector3.forward * 0.005f, Space.World);
                    break;
                case 2:
                    this.gameObject.transform.Translate(Vector3.back * 0.005f, Space.World);
                    break;
                case 3:
                    this.gameObject.transform.Translate(Vector3.left * 0.005f, Space.World);
                    break;
                case 4:
                    this.gameObject.transform.Translate(Vector3.right * 0.005f, Space.World);
                    break;
                case 5:
                    this.gameObject.transform.Translate(Vector3.down * 0.005f, Space.World);
                    break;
            }
        }
        else if (!stopme)
        {
            this.gameObject.transform.Translate(Vector3.down * 0.01f, Space.World);

        }

    }

    public void pushmealr()
    {
        letmepush = true;
        randnum = Random.Range(1, 6);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Killbox"))
        {
            this.gameObject.SetActive(false);
        }
        else if (!letmepush && other.gameObject.CompareTag("Block"))
        {
            stopme = true;
        }
        else if (letmepush && other.gameObject.CompareTag("Block"))
        {
            letmepush = false;
        }
    }

}
