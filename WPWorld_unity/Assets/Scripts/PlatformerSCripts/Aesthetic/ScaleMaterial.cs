using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleMaterial : MonoBehaviour {

    private Vector3 ScaleRef;

    private Material MaterialRef;

    // Use this for initialization
    void Start () {
        ScaleRef = transform.localScale * 10;

        MaterialRef = GetComponent<Renderer>().material;
        MaterialRef.mainTextureScale = ScaleRef;
	}
}
