using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCape : MonoBehaviour {

    Cloth ClothRef;

    Vector3 ClothAcceleration;

	// Use this for initialization
	void Start () {
        ClothRef = GetComponent<Cloth>();
	}
	
	// Update is called once per frame
	void Update () {
        ClothAcceleration = -transform.parent.transform.forward * 3;

        if(Vector3.Distance(ClothAcceleration, ClothRef.externalAcceleration) < 0.1f)
            ClothRef.externalAcceleration = ClothAcceleration;
        else
            ClothRef.externalAcceleration = Vector3.Lerp(ClothRef.externalAcceleration, ClothAcceleration, Time.deltaTime);
    }
}
