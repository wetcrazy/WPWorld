using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthPopup : MonoBehaviour {

    private Image ImageRef;

    private Color ColorRef;

	// Use this for initialization
	void Start () {
        ImageRef = GetComponent<Image>();

        ColorRef = ImageRef.color;
        ColorRef.a = 0;
        ImageRef.color = ColorRef;
	}
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i - 1).GetComponent<Image>().color = ColorRef;
        }
    }
}
