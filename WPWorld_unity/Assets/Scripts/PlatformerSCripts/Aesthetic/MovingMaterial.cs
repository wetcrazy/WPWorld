using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingMaterial : MonoBehaviour {

    [SerializeField]
    private float X_Speed;
    [SerializeField]
    private float Y_Speed;

    private Material MaterialRef;

	// Use this for initialization
	void Start () {
        MaterialRef = GetComponent<Renderer>().material;
	}
	
	// Update is called once per frame
	void Update () {
        MaterialRef.mainTextureOffset += new Vector2(X_Speed, Y_Speed);
	}
}
