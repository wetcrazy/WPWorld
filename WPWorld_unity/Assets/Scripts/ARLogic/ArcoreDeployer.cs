using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GoogleARCore;

/// <summary>
/// Controls the ARCORE and Scene UI
/// </summary>
public class ArcoreDeployer : MonoBehaviour
{
    enum STATE_SCREEN
    {
        SCREEN_SPLASH,
        SCREEN_SELECTION,
        SCREEN_GAME,
        
        SCREEN_TOTAL
    }
    STATE_SCREEN ScreenState = STATE_SCREEN.SCREEN_SPLASH;

    // Game Objects
    public Camera MainCamera;
    public GameObject[] Arr_LevelsOBJ;
  
    public Text[] Arr_DEBUGGER; // Debugger

    private List<DetectedPlane> List_AllPlanes = new List<DetectedPlane>();
    private GameObject GameObjPrefab;
    private bool isSpawned = false;

    // UI Objects
    public Canvas UI_Canvas;
    public GameObject UI_SpashLogoOBJ;
    public Text UI_TrackingText;

    private void Update()
    {
        Touch _touch;

        // Detect 1st Touch (for splash screen)
        if (UI_SpashLogoOBJ.activeSelf)
        {
            if (Input.touchCount > 0 || (_touch = Input.GetTouch(0)).phase == TouchPhase.Moved)
            {
                UI_SpashLogoOBJ.SetActive(false);
                ScreenState = STATE_SCREEN.SCREEN_SELECTION;

                // Gets all Planes that are track and put it into the list
                Session.GetTrackables<DetectedPlane>(List_AllPlanes);
            }
            else
            {
                return;
            }
        }

        switch (ScreenState)
        {
            case STATE_SCREEN.SCREEN_SELECTION:
                {


                    break;
                }
            case STATE_SCREEN.SCREEN_GAME:
                break;
            case STATE_SCREEN.SCREEN_TOTAL:
                break;
            default:
                break;
        }





        

        for (int i = 0; i < List_AllPlanes.Count; i++)
        {
            //When the gameobject appears
            if (List_AllPlanes[i].TrackingState == TrackingState.Tracking)
            {
                UI_TrackingText.enabled = false;
                break;
            }           
            else if (List_AllPlanes[i].TrackingState == TrackingState.Stopped)
            {
                UI_Canvas.enabled = true;
            }
            else
            {
                UI_TrackingText.enabled = true;
            }
        }

        if (Input.touchCount < 1 || (_touch = Input.GetTouch(0)).phase != TouchPhase.Began)
        {
            return;
        }

        if(!isSpawned)
        {
            Spawner(_touch);          
            isSpawned = true;
        }
        else
        {
            //if(CheckGameObjects(GameObjPrefab) == false)
            //{              
            //    isSpawned = false;
            //    UI_Canvas.enabled = true;
            //}        
        }
    }
    
    // Add a new Object using point on screen and ARCore
    private void Spawner(Touch _touch)
    {
        // Raycast from point on screen to real world
        TrackableHit _hit;
        var _raycastFilter = TrackableHitFlags.PlaneWithinPolygon | TrackableHitFlags.FeaturePointWithSurfaceNormal;
        if (Frame.Raycast(_touch.position.x, _touch.position.y, _raycastFilter, out _hit))
        {
            // Check if it the raycast is hitting the back of the plane 
            if ((_hit.Trackable is DetectedPlane) && Vector3.Dot(MainCamera.transform.position - _hit.Pose.position, _hit.Pose.rotation * Vector3.up) < 0)
            {
                Debug.Log("Hit at back of the current DetectedPlane");
            }
            else
            {
                GameObject _prefab = GameObjPrefab;

                // Instantiate the object at where it is hit
                var _GroundObject = Instantiate(_prefab, _hit.Pose.position, _hit.Pose.rotation, transform.parent);

                // Create an anchor for ARCore to track the point of the real world
                var _anchor = _hit.Trackable.CreateAnchor(_hit.Pose);

                // Make the ground object the child of the anchor
                _GroundObject.transform.parent = _anchor.transform;

                UI_Canvas.enabled = false;
            }
        }
    }
   
    // oooooooooooooooooooooooooooooooooooooooo
    //            <Public Stuff> 
    // oooooooooooooooooooooooooooooooooooooooo
  

    // Sets the next game object (Works like a scene manager)
    public void SetNextObject(string _ObjName)
    {
        for (int i = 0; i < Arr_LevelsOBJ.Length; i++)
        {
            if (Arr_LevelsOBJ[i].name == _ObjName)
            {
                GameObjPrefab = Arr_LevelsOBJ[i];
              
            }
        }
    }

   
}
