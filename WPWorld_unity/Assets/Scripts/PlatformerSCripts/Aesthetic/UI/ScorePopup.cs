using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePopup : MonoBehaviour {

    private TextMesh TextRef;

	// Use this for initialization
	void Start () {
        TextRef = GetComponent<TextMesh>();
	}
	
	// Update is called once per frame
	void Update () {
		if(TextRef.color.a <= 0)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Color ColorRef = TextRef.color;
            ColorRef.a -= Time.deltaTime;
            TextRef.color = ColorRef;

            GetComponent<Rigidbody>().AddForce(transform.up * 0.2f * Time.deltaTime, ForceMode.VelocityChange);

            transform.LookAt(2 * transform.position - Camera.main.transform.position);
        }
	}
}
