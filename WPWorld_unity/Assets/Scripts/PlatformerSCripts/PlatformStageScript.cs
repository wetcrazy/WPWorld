using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformStageScript : MonoBehaviour {

    [SerializeField]
    private AudioClip BGM;

    private void Awake()
    {
<<<<<<< HEAD
        transform.position = new Vector3(Camera.main.transform.position.x, transform.position.y, Camera.main.transform.position.z);
=======
        //transform.eulerAngles = Vector3.zero;

        //Vector3 NewPos = Camera.main.transform.position;
        //NewPos.y = transform.position.y;

        //transform.position = NewPos;
>>>>>>> 57542a85fb71e372953892f625b685cd6663fff6
    }

    private void Start()
    {
        transform.position = new Vector3(Camera.main.transform.position.x, transform.position.y, Camera.main.transform.position.z);

        if (BGM != null && GameObject.Find("Sound System") != null)
        {
            GameObject.Find("Sound System").GetComponent<SoundSystem>().PlayBGM(BGM);
        }
    }

    private void Update()
    {
        transform.eulerAngles = Vector3.zero;
<<<<<<< HEAD
=======

        transform.position = new Vector3(Camera.main.transform.position.x, transform.position.y, Camera.main.transform.position.z);
>>>>>>> 57542a85fb71e372953892f625b685cd6663fff6
    }
}
