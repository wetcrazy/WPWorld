using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformStageScript : MonoBehaviour {

    [SerializeField]
    private string BGM;

    [SerializeField]
    private bool SpawnOnPlayer;

    private SoundSystem SoundSystemRef;

    private void Awake()
    {
        if(SpawnOnPlayer)
            transform.position = new Vector3(Camera.main.transform.position.x, transform.position.y, Camera.main.transform.position.z);
    }

    private void Start()
    {
        if (SpawnOnPlayer)
            transform.position = new Vector3(Camera.main.transform.position.x, transform.position.y, Camera.main.transform.position.z);
        SoundSystemRef = GameObject.FindGameObjectWithTag("SoundSystem").GetComponent<SoundSystem>();
        if(BGM != "")
            SoundSystemRef.PlayBGM(BGM);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.H))
        {
            SoundSystemRef.PlayBGM(BGM);
        }
        transform.eulerAngles = Vector3.zero;
    }
}
