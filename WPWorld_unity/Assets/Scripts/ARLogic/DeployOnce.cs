﻿using System.Collections;
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
    /// A GameObject with Ui atttached
    /// </summary>
    public GameObject CheckSpawnUI;

    /// <summary>
    /// Check if the app is closing due to ARCore
    /// </summary>
    private bool _isQuit = false;

    /// <summary>
    /// A list of planes ARCore
    /// </summary>
    private List<DetectedPlane> _AllPlanes = new List<DetectedPlane>();

    /// <summary>
    /// A list of gameobjects
    /// </summary>
    private GameObject _prefab;

    ///<summary>
    /// For Ground spawn
    ///</summary>
    private bool _isSpawn = false;

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        // Tracks if there is a plane to spawn
        Session.GetTrackables<DetectedPlane>(_AllPlanes);
        bool _isTracked = true;
        for (int i = 0; i < _AllPlanes.Count; i++)
        {
            if (_AllPlanes[i].TrackingState == TrackingState.Tracking)
            {
                _isTracked = false;
                break;
            }
        }

        CheckSpawnUI.SetActive(_isTracked);

        // Check player touch, if no touch just leave
        Touch _touch;
        if (Input.touchCount < 1 || (_touch = Input.GetTouch(0)).phase != TouchPhase.Began)
        {
            return;
        }


        //RayCast from the player touch to the real world to find detected planes
        TrackableHit _hit;
        TrackableHitFlags _raycastFilter = TrackableHitFlags.PlaneWithinPolygon | TrackableHitFlags.FeaturePointWithSurfaceNormal;

        // Draw a line out from the player touch postion to the surface of the real world
        if (Frame.Raycast(_touch.position.x, _touch.position.y, _raycastFilter, out _hit))
        {
            // Check if it the raycast is hitting the back of the plane 
            if ((_hit.Trackable is DetectedPlane) && Vector3.Dot(MainCamera.transform.position - _hit.Pose.position, _hit.Pose.rotation * Vector3.up) < 0)
            {
                Debug.Log("Hit at back of the current DetectedPlane");
            }
            else if( !_isSpawn)
            {
                // Its a ground plane soo doesnt matter if its a point or a plane
                _prefab = GroundPlanePrefab;

                // Instantiate the object at where it is hit
                var GroundObject = Instantiate(_prefab, _hit.Pose.position, _hit.Pose.rotation);

                // Create an anchor for ARCore to track the point of the real world
                var _anchor = _hit.Trackable.CreateAnchor(_hit.Pose);

                // Make the ground object the child of the anchor
                GroundObject.transform.parent = _anchor.transform;

                _isSpawn = true;
            }
        }

        if(_isSpawn)
        {
            if(_prefab.activeSelf)
            {
                _isSpawn = false;
            }
        }
    }
}
