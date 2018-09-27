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
    STATE_SCREEN ScreenState;

    // Game Objects
    public Camera MainCamera;
    public GameObject[] Arr_LevelsOBJ;
  
    public Text[] Arr_DEBUGGER; // Debugger

    private List<DetectedPlane> List_AllPlanes = new List<DetectedPlane>();
    private GameObject GameObjPrefab = null;
    private bool isSpawned = false;

    // UI Objects
    //public Canvas UI_Canvas;
    public GameObject UI_SpashLogoOBJ;
    public Text UI_TrackingText;

    //Screens
    [SerializeField]
    GameObject SelectionScreen;
    [SerializeField]
    GameObject SplashScreen;
    [SerializeField]
    GameObject GameScreen;

    private void Awake()
    {
        SelectionScreen.SetActive(false);
        GameScreen.SetActive(false);
        ScreenState = STATE_SCREEN.SCREEN_SPLASH;
    }

    private void Update()
    {   
        switch (ScreenState)
        {
            case STATE_SCREEN.SCREEN_SPLASH:
                {
                    if (!SplashScreen.activeSelf)
                    {
                        SplashScreen.SetActive(true);
                        GameScreen.SetActive(false);
                        SelectionScreen.SetActive(false);
                    }

                    if (Input.touchCount > 0 || Input.GetTouch(0).phase == TouchPhase.Moved)
                    {
                        ScreenState = STATE_SCREEN.SCREEN_SELECTION;

                        // Gets all Planes that are track and put it into the list
                        Session.GetTrackables<DetectedPlane>(List_AllPlanes);
                        break;
                    }
                    else
                    {
                        return;
                    }
                }
            case STATE_SCREEN.SCREEN_SELECTION:
                {
                    if(!SelectionScreen.activeSelf)
                    {
                        SelectionScreen.SetActive(true);
                        SplashScreen.SetActive(false);
                        GameScreen.SetActive(false);
                    }

                    break;
                }
            case STATE_SCREEN.SCREEN_GAME:
                {
                    if (!GameScreen.activeSelf)
                    {
                        GameScreen.SetActive(true);
                        SplashScreen.SetActive(false);
                        SelectionScreen.SetActive(false);
                    }

                    if (!isSpawned && Input.touchCount > 0)
                    {
                        Spawner(Input.GetTouch(0));
                        isSpawned = true;
                    }
                    else if(GameObjPrefab == null)
                    {
                        isSpawned = false;
                        DestroyCurrentLevel();
                        ScreenState = STATE_SCREEN.SCREEN_SELECTION;
                        break;
                    }

                    for (int i = 0; i < List_AllPlanes.Count; i++)
                    {
                        //If tracking has stopped, return to selection screen
                        if (List_AllPlanes[i].TrackingState == TrackingState.Stopped)
                        {
                            DestroyCurrentLevel();
                            ScreenState = STATE_SCREEN.SCREEN_SELECTION;
                            break;
                        }
                    }
                    break;
                }
            default:
                break;
        }
        
        //for (int i = 0; i < List_AllPlanes.Count; i++)
        //{
        //    //When the gameobject appears
        //    if (List_AllPlanes[i].TrackingState == TrackingState.Tracking)
        //    {
        //        UI_TrackingText.enabled = false;
        //        break;
        //    }           
        //    else if (List_AllPlanes[i].TrackingState == TrackingState.Stopped)
        //    {
        //        //UI_Canvas.enabled = true;
        //    }
        //    else
        //    {
        //        UI_TrackingText.enabled = true;
        //    }
        //}

        //if (Input.touchCount < 1 || (_touch = Input.GetTouch(0)).phase != TouchPhase.Began)
        //{
        //    return;
        //}
    }
    
    public void DestroyCurrentLevel()
    {
        Destroy(GameObjPrefab);
        GameObjPrefab = null;
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
                
                ScreenState = STATE_SCREEN.SCREEN_GAME;
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
