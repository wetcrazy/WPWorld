using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinBehaviour : MonoBehaviour {

    [SerializeField]
    private float MovementSpeed;

    [SerializeField]
    private float RotationSpeed;

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        Vector3 MoveDir = transform.forward * MovementSpeed * Time.deltaTime;
        Vector3 RotateVec = transform.localEulerAngles;

        RotateVec.y += RotationSpeed * Time.deltaTime;

        transform.localEulerAngles = RotateVec;
        transform.localPosition += MoveDir;
	}
}
