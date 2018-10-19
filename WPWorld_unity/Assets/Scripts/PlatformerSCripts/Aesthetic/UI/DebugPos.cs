using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugPos : MonoBehaviour {

    [SerializeField]
    private GameObject ObjectToDebug;

    private Text TextRef;

	// Use this for initialization
	void Start () {
        TextRef = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        TextRef.text = "X: " + ObjectToDebug.transform.position.x.ToString("F2") + "\nZ: " + ObjectToDebug.transform.position.z.ToString("F2");
	}
}
