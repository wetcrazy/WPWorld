using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformStageScript : MonoBehaviour {

    [SerializeField]
    private AudioClip BGM;

    [SerializeField]
    private bool SpawnOnPlayer;

    private void Awake()
    {
        if(SpawnOnPlayer)
            transform.position = new Vector3(Camera.main.transform.position.x, transform.position.y, Camera.main.transform.position.z);
    }

    private void Start()
    {
        if (SpawnOnPlayer)
            transform.position = new Vector3(Camera.main.transform.position.x, transform.position.y, Camera.main.transform.position.z);

        //if (BGM != null && GameObject.Find("Sound System") != null)
        //{
        //    GameObject.Find("Sound System").GetComponent<SoundSystem>().PlayBGM(BGM);
        //}
    }

    private void Update()
    {
        transform.eulerAngles = Vector3.zero;
    }
}
