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
            }
            else
            {
                return;
            }
        }

        // Tracking for Planes
        Session.GetTrackables<DetectedPlane>(List_AllPlanes);
        for (int i = 0; i < List_AllPlanes.Count; i++)
        {
            if (List_AllPlanes[i].TrackingState == TrackingState.Tracking)
            {
                UI_TrackingText.enabled = false;
                break;
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
            isSpawned = true;   
            Spawner(_touch);
        }
        else
        {
            if(CheckGameObjects(GameObjPrefab) == false)
            {
                isSpawned = false;
            }
        }
     
        /*
        UI_Canvas.enabled = true;

        if (CheckGameObjects(GameObjPrefab) == false)
        {                   
            Spawner(_touch);
            return;
        }
        else
        {          
            UI_Canvas.enabled = false;
        }
        */
        /*
        // Check for NO Touch
        if (Input.touchCount < 1 || (_touch = Input.GetTouch(0)).phase != TouchPhase.Began)
        {
            return;
        }

        // Just incase not enabled
        UI_Canvas.enabled = true;

        if(GameObjPrefab == null)
        {
            SetNextObject("Planets"); // For starting the game
        }
        
        // If the game object is active there is no need to update
        if (CheckGameObjects(GameObjPrefab)) 
        {
            UI_Canvas.enabled = false;
            return;
        }
        else
        {
            Spawner(_touch);
            return;
        }            
        */
    }

    // Checks for the game object game
    private bool CheckGameObjects(GameObject _OBJ)
    {
        var _allOBJ = GameObject.FindGameObjectsWithTag(_OBJ.tag);
        if (_allOBJ.Length >= 1)
        {
            SetDebuggingText(_allOBJ.Length.ToString());
            return true;
        }
        return false;
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
                SetDebuggingText("OBJ= " + GameObjPrefab.name);
            }
        }
    }

    // My Debugging tool for Arcore
    public void SetDebuggingText(string _words)
    {
        for (int i = 0; i < Arr_DEBUGGER.Length; i++)
        {
            if (Arr_DEBUGGER[i].text == "New Text" || Arr_DEBUGGER[i].text == "" || Arr_DEBUGGER[i].text == _words)
            {
                Arr_DEBUGGER[i].text = _words;
            }
        }
    }

   
}
