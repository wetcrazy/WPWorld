using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOnCollide : MonoBehaviour {

    [SerializeField]
    private Vector3 RotateAngle;

    [SerializeField]
    private float RotateSpeed;

    private bool isRotating;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (isRotating)
        {
            if (Vector3.Distance(this.transform.position, RotateAngle) > 0.1f)
            {
                this.transform.eulerAngles = Vector3.Lerp(this.transform.eulerAngles, RotateAngle, Time.deltaTime * RotateSpeed);
            }
            else
            {
                this.transform.position = RotateAngle;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            isRotating = true;
        }
    }
}
