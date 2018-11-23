using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverlayCameraFind : MonoBehaviour {

    private Canvas CanvasRef;

	// Use this for initialization
	void Start () {
        CanvasRef = GetComponent<Canvas>();

        CanvasRef.renderMode = RenderMode.ScreenSpaceCamera;
        CanvasRef.worldCamera = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
	}
}
