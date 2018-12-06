using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformStageScript : MonoBehaviour {

    [SerializeField]
    private string BGM;

    private SoundSystem SoundSystemRef;

    private void Awake()
    {
        transform.eulerAngles = Vector3.zero;
    }

    private void Start()
    {
        transform.eulerAngles = Vector3.zero;

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
