using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformStageScript : MonoBehaviour {

    [SerializeField]
    private string BGM;

    private SoundSystem SoundSystemRef;
	private PlayerMovement PlayerRef;

    private void Start()
    {
        SoundSystemRef = GameObject.FindGameObjectWithTag("SoundSystem").GetComponent<SoundSystem>();
		PlayerRef = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        if(BGM != "")
            SoundSystemRef.PlayBGM(BGM);

		if(transform.rotation.x <= 0)
		{

		}
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.H))
        {
            SoundSystemRef.PlayBGM(BGM);
        }
    }
}
