using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformStageScript : MonoBehaviour {

    [SerializeField]
    private AudioClip BGM;

    private void Awake()
    {
        transform.eulerAngles = Vector3.zero;

        Vector3 NewPos = Camera.main.transform.position;
        NewPos.y = transform.position.y;

        transform.position = NewPos;
    }

    private void Start()
    {
        if (BGM != null && GameObject.Find("Sound System") != null)
        {
            GameObject.Find("Sound System").GetComponent<SoundSystem>().PlayBGM(BGM);
        }
    }
}
