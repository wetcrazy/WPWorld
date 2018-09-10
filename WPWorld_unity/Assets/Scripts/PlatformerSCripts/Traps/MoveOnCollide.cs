using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnCollide : MonoBehaviour {

    [SerializeField]
    private Vector3 MovePosition;
    [SerializeField]
    private float MoveSpeed;

    private bool IsMoving = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(IsMoving)
        {
            if(Vector3.Distance(this.transform.position, MovePosition) > 0.1f)
            {
                this.transform.position = Vector3.Lerp(this.transform.position, MovePosition, Time.deltaTime * MoveSpeed);
            }
            else
            {
                this.transform.position = MovePosition;
            }
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            IsMoving = true;
        }
    }
}
