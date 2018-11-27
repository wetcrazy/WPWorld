using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBaseMovement : MonoBehaviour
{
    public GameObject blockObj;

    [SerializeField]
    private Vector3 respawnPoint;
    [SerializeField]
    private Vector3 movementDir;

    private Rigidbody rb;
    Joystick joystickControl;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
