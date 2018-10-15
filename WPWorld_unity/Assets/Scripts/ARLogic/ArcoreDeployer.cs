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
        SCREEN_SELECTION_UNIVERSE,
        SCREEN_SELECTION_PLANET,
        SCREEN_GAME,

        SCREEN_TOTAL
    }
    STATE_SCREEN ScreenState;

    // Game Objects
    public Camera MainCamera;
    public GameObject[] Arr_LevelsOBJ;

    private List<DetectedPlane> List_AllPlanes = new List<DetectedPlane>();
    private GameObject GameObjPrefab = null;
    private bool isSpawned = false;

    Vector3 CameraOriginalPos;

    // UI Objects
    //[SerializeField]
    [SerializeField]
    GameObject UniverseObj;
    [SerializeField]
    GameObject SelectedWorld;
    [SerializeField]
    Text CurrentWorldName;
    [SerializeField]
    Button WorldSelectBtn;

    [SerializeField]
    Text DebugText;

    private GameObject[] SplashScreenObjects;
    private GameObject[] SelectionScreen_PlanetsObjects;
    private GameObject[] GameScreenObjects;

    //UI Logic Variables
    [SerializeField]
    float WorldRotationSpeed = 10;
    
    private void Start()
    {
        SplashScreenObjects = GameObject.FindGameObjectsWithTag("SplashScreen");
        SelectionScreen_PlanetsObjects = GameObject.FindGameObjectsWithTag("SelectionScreen_Planets");
        GameScreenObjects = GameObject.FindGameObjectsWithTag("GameScreen");

        ExitSelectionScreen_Planet();
        ExitGameScreen();
        ToSplashScreen();

        Image WorldSelectButtonImage = WorldSelectBtn.GetComponent<Image>();
        Color NewColor = WorldSelectButtonImage.color;
        NewColor.a = 0;
        WorldSelectButtonImage.color = NewColor;
    }

    private void Update()
    {
        switch (ScreenState)
        {
            case STATE_SCREEN.SCREEN_SPLASH:
                {
                    SplashScreenUpdate();
                    break;
                }
            case STATE_SCREEN.SCREEN_SELECTION_UNIVERSE:
                {
                    SelectionScreenUpdate_Universe();
                    break;
                }
            case STATE_SCREEN.SCREEN_SELECTION_PLANET:
                {
                    SelectionScreenUpdate_Planet();
                    break;
                }
            case STATE_SCREEN.SCREEN_GAME:
                {
                    GameScreenUpdate();
                    break;
                }
            default:
                break;
        }
    }

    private void ToSplashScreen()
    {
        ScreenState = STATE_SCREEN.SCREEN_SPLASH;

        foreach (GameObject obj in SplashScreenObjects)
        {
            obj.SetActive(true);
        }
    }

    public void SplashScreenUpdate()
    {
        if (Input.touchCount > 0)
        {
            ExitSplashScreen();
            ToSelectionScreen_Universe();

            // Gets all Planes that are track and put it into the list
            Session.GetTrackables(List_AllPlanes);
        }
    }

    public void ExitSplashScreen()
    {
        foreach (GameObject obj in SplashScreenObjects)
        {
            obj.SetActive(false);
        }
    }

    public void ToSelectionScreen_Universe()
    {
        if (_GroundObject != null)
        {
            _GroundObject.SetActive(true);
        }

        ScreenState = STATE_SCREEN.SCREEN_SELECTION_UNIVERSE;
    }

    private void SelectionScreenUpdate_Universe()
    {
        if (!isSpawned && Input.touchCount > 0)
        {
            GameObjPrefab = UniverseObj;
            Spawner(Input.GetTouch(0));
        }
        else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Touch theTouch = Input.GetTouch(0);

            Vector3 touchPosFar = new Vector3(theTouch.position.x, theTouch.position.y, MainCamera.farClipPlane);
            Vector3 touchPosNear = new Vector3(theTouch.position.x, theTouch.position.y, MainCamera.nearClipPlane);

            Vector3 touchPosF = MainCamera.ScreenToWorldPoint(touchPosFar);
            Vector3 touchPosN = MainCamera.ScreenToWorldPoint(touchPosNear);

            RaycastHit hit;

            if (Physics.Raycast(touchPosN, touchPosF - touchPosN, out hit))
            {
                if (hit.transform.gameObject.tag == "Planet")
                {
                    CurrentWorldName.text = hit.transform.gameObject.name;
                    SelectedWorld.GetComponent<MeshRenderer>().material = hit.transform.gameObject.GetComponent<MeshRenderer>().material;

                    ExitSelectionScreen_Universe();
                    ToSelectionScreen_Planet();
                }
            }
        }
        else if (isSpawned)
        {
            for (int i = 0; i < UniverseObj.transform.childCount; ++i)
            {
                _GroundObject.transform.GetChild(i).transform.Rotate(gameObject.transform.up, WorldRotationSpeed * Time.deltaTime);
            }
        }

        for (int i = 0; i < List_AllPlanes.Count; i++)
        {
            //If tracking has stopped, return to selection screen
            if (List_AllPlanes[i].TrackingState == TrackingState.Stopped)
            {
                DestroyCurrentLevel();
            }
        }
    }

    public void ExitSelectionScreen_Universe(bool DestroyUniverse = false)
    {
        if (DestroyUniverse)
        {
            DestroyCurrentLevel();
        }
        else if (_GroundObject != null)
        {
            _GroundObject.SetActive(false);
        }
    }

    public void ToSelectionScreen_Planet()
    {
        foreach (GameObject obj in SelectionScreen_PlanetsObjects)
        {
            obj.SetActive(true);
        }

        ScreenState = STATE_SCREEN.SCREEN_SELECTION_PLANET;
    }

    private void SelectionScreenUpdate_Planet()
    {
        SelectedWorld.transform.Rotate(gameObject.transform.up, WorldRotationSpeed * Time.deltaTime);
    }

    public void ExitSelectionScreen_Planet(bool DestroyUniverse = false)
    {
        if (DestroyUniverse)
        {
            DestroyCurrentLevel();
        }

        foreach (GameObject obj in SelectionScreen_PlanetsObjects)
        {
            obj.SetActive(false);
        }
    }

    public void ToGameScreen()
    {
        foreach (GameObject obj in GameScreenObjects)
        {
            obj.SetActive(true);
        }

        ScreenState = STATE_SCREEN.SCREEN_GAME;
        isSpawned = false;

        SetNextObject();
    }

    private void GameScreenUpdate()
    {
        // Gets all Planes that are track and put it into the list
        //Session.GetTrackables(List_AllPlanes);

        if (!isSpawned && Input.touchCount > 0)
        {
            Spawner(Input.GetTouch(0));
        }

        foreach (DetectedPlane thePlane in List_AllPlanes)
        {
            if (thePlane.TrackingState == TrackingState.Stopped)
            {
                ExitGameScreen();
                ToSelectionScreen_Planet();
                break;
            }
        }
    }

    public void ExitGameScreen()
    {
        DestroyCurrentLevel();
        isSpawned = false;

        foreach (GameObject obj in GameScreenObjects)
        {
            obj.SetActive(false);
        }
    }

    private void DestroyCurrentLevel()
    {
        if (_GroundObject != null)
        {
            Destroy(_GroundObject);
            _GroundObject = null;
        }

        if (GameObjPrefab != null)
        {
            GameObjPrefab = null;
        }
    }

    public void RestartLevel()
    {
        //DestroyCurrentLevel();
        //SetNextObject();
        //Spawner(RememberedTouch);
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
                // Instantiate the object at where it is hit
                _GroundObject = Instantiate(GameObjPrefab, _hit.Pose.position, _hit.Pose.rotation, transform.parent);

                // Create an anchor for ARCore to track the point of the real world
                var _anchor = _hit.Trackable.CreateAnchor(_hit.Pose);

                // Make the ground object the child of the anchor
                _GroundObject.transform.parent = _anchor.transform;
                isSpawned = true;
            }
        }
    }

    //Reference to the clone of GameObjPrefab
    GameObject _GroundObject;

    // oooooooooooooooooooooooooooooooooooooooo
    //            <Public Stuff> 
    // oooooooooooooooooooooooooooooooooooooooo


    // Sets the next game object (Works like a scene manager)
    private void SetNextObject()
    {
        //Temporary level select hardcode method
        string _ObjName = "";

        switch (CurrentWorldName.text)
        {
            case "3DPuzzle World":
                {
                    _ObjName = "World01_Stage01";
                    break;
                }
            case "DungeonSweeper World":
                {
                    _ObjName = "DungeonSweeper";
                    break;
                }
            case "Platformer World":
                {
                    _ObjName = "Level 1";
                    break;
                }
            case "Asteroid World":
                {
                    _ObjName = "World05_Stage01";
                    break;
                }
            default:
                break;
        }

        foreach (GameObject PrefabLevel in Arr_LevelsOBJ)
        {
            if (_ObjName == PrefabLevel.name)
            {
                DestroyCurrentLevel();
                GameObjPrefab = PrefabLevel;
                break;
            }
        }
    }
}