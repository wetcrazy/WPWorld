using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IndicatorScript : MonoBehaviour {

    private Image ImageRef;

    private Vector3 ScreenMiddle;

	// Use this for initialization
	void Start () {
        ImageRef = GetComponent<Image>();

        ScreenMiddle = new Vector3(Screen.width / 2, Screen.height / 2, 0);
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 ScreenPoint = Camera.main.WorldToViewportPoint(GameObject.FindGameObjectWithTag("Player").transform.position);

        ImageRef.enabled = false;
        if (ScreenPoint.z < Camera.main.nearClipPlane)
            return;

        if (ScreenPoint.x > 0 && ScreenPoint.x < 1
            && ScreenPoint.y > 0 && ScreenPoint.y < 1)
            return;

        ImageRef.enabled = true;

        Vector3 TargetDir = new Vector3(ScreenPoint.x * ScreenMiddle.x, ScreenPoint.y * ScreenMiddle.y, 0);
    }
}
