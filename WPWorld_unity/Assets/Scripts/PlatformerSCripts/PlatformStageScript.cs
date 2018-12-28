using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformStageScript : MonoBehaviour {

    [SerializeField]
    private string BGM;

    private SoundSystem SoundSystemRef;
	private PlayerMovement PlayerRef;

    private void Awake()
    {

    }

    private void Start()
    {
        SoundSystemRef = GameObject.FindGameObjectWithTag("SoundSystem").GetComponent<SoundSystem>();
		PlayerRef = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        if(BGM != "")
            SoundSystemRef.PlayBGM(BGM);

        Debug.Log(transform.eulerAngles.y);

        if(transform.eulerAngles.y >= 0 && transform.eulerAngles.y < 45)
        {
            Debug.Log("Go to Zero");
            transform.eulerAngles = Vector3.zero;
        }

        if (transform.eulerAngles.y >= 45 && transform.eulerAngles.y < 135)
        {
            Debug.Log("Go to 90");
            transform.eulerAngles = new Vector3(0, 90, 0);
        }

        if (transform.eulerAngles.y >= 135 && transform.eulerAngles.y < 225)
        {
            Debug.Log("Go to 180");
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

        if (transform.eulerAngles.y >= 225 && transform.eulerAngles.y < 315)
        {
            Debug.Log("Go to 270");
            transform.eulerAngles = new Vector3(0, 270, 0);
        }

        if (transform.eulerAngles.y >= 315 && transform.eulerAngles.y < 360)
        {
            Debug.Log("Go to Zero");
            transform.eulerAngles = Vector3.zero;
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
