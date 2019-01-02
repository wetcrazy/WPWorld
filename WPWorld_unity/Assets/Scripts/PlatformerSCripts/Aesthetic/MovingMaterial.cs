using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingMaterial : MonoBehaviour {

    [SerializeField]
    private float X_Speed;
    [SerializeField]
    private float Y_Speed;

    [SerializeField]
    private float X_Random;
    [SerializeField]
    private float Y_Random;

    private float X_Movement;
    private float Y_Movement;

    private Material MaterialRef;

	// Use this for initialization
	void Start () {
        MaterialRef = GetComponent<Renderer>().material;
	}
	
	// Update is called once per frame
	void Update () {
        X_Movement = X_Speed + Random.Range(-X_Random, X_Random);
        Y_Movement = Y_Speed + Random.Range(-Y_Random, Y_Random);

        MaterialRef.mainTextureOffset += new Vector2(X_Movement, Y_Movement);
	}
}
