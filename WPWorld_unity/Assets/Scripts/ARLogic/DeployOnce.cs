using System.Collections;
using System.Collections.Generic;
using GoogleARCore;
using UnityEngine;


public class DeployOnce : MonoBehaviour
{
    /// <summary>
    /// The Main Camera hook onto the ARCore
    /// </summary>
    public Camera MainCamera;

    /// <summary>
    /// A prefab to to summmon the ground plane for visualization
    /// </summary>
    public GameObject GroundPlanePrefab;

    /// <summary>
    /// Check if the app is closing due to ARCore
    /// </summary>
    private bool _isQuit = false;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
