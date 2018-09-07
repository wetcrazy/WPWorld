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
    /// A GameObject with Ui atttached
    /// </summary>
    public GameObject CheckSpawnUI;

    /// <summary>
    /// A list of planes ARCore
    /// </summary>
    private List<DetectedPlane> AllPlanes = new List<DetectedPlane>();

    /// <summary>
    /// Summoning prefab
    /// </summary>
    private GameObject prefab;

    /// <summary>
    /// Checking for prefab summoned
    /// </summary>
    private bool isPrefabSpawned = false;

    void Update()
    {
        // Tracks if there is a plane to spawn
        Session.GetTrackables<DetectedPlane>(AllPlanes);
        bool _isTracked = true;
        for (int i = 0; i < AllPlanes.Count; i++)
        {
            if (AllPlanes[i].TrackingState == TrackingState.Tracking)
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
            if(!isPrefabSpawned)
            {
                // Check if it the raycast is hitting the back of the plane 
                if ((_hit.Trackable is DetectedPlane) && Vector3.Dot(MainCamera.transform.position - _hit.Pose.position, _hit.Pose.rotation * Vector3.up) < 0)
                {
                    Debug.Log("Hit at back of the current DetectedPlane");
                }
                else
                {                   
                    // Its a ground plane soo doesnt matter if its a point or a plane
                    GameObject _prefab = GroundPlanePrefab;

                    // Instantiate the object at where it is hit
                    var _GroundObject = Instantiate(_prefab, _hit.Pose.position, _hit.Pose.rotation);
             

                    // Create an anchor for ARCore to track the point of the real world
                    var _anchor = _hit.Trackable.CreateAnchor(_hit.Pose);

                    // Make the ground object the child of the anchor
                    _GroundObject.transform.parent = _anchor.transform;

                    prefab = _prefab;
                    isPrefabSpawned = true;
                }
            }        
        }

        if (!prefab.activeSelf || prefab == null)
        {
            isPrefabSpawned = false;
        }
    }
}
