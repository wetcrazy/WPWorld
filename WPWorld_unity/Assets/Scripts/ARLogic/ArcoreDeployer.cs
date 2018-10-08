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
    [SerializeField]
    GameObject[] SelectionLevels;
    [SerializeField]
    GameObject CurrentWorldName;
    [SerializeField]
    Button WorldSelectBtn;
    [SerializeField]
    GameObject ScreenSpaceCanvas;
    [SerializeField]
    GameObject PauseBar;
    [SerializeField]
    GameObject UniverseObj;

    int CurrentLevelSelection = 0;

    //UI Logic Variables
    [SerializeField]
    float WorldRotationSpeed = 10;

    //Screens
    [SerializeField]
    GameObject SelectionScreen;
    [SerializeField]
    GameObject SplashScreen;
    [SerializeField]
    GameObject GameScreen;

    private void Start()
    {
        SplashScreen.SetActive(true);
        SelectionScreen.SetActive(false);
        GameScreen.SetActive(false);
        ScreenSpaceCanvas.SetActive(false);
        PauseBar.SetActive(false);

        ScreenState = STATE_SCREEN.SCREEN_SPLASH;

        SelectionLevels[0].SetActive(true);
        CurrentWorldName.GetComponent<Text>().text = SelectionLevels[0].name;
        for (int i = 1; i < SelectionLevels.Length; ++i)
        {
            SelectionLevels[i].SetActive(false);
        }

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
                    //if (!SplashScreen.activeSelf)
                    //{
                    //    SplashScreen.SetActive(true);
                    //    GameScreen.SetActive(false);
                    //    SelectionScreen.SetActive(false);
                    //}

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

    private void SplashScreenUpdate()
    {
        if (Input.touchCount > 0)
        {
            ToSelectionScreen_Universe();

            // Gets all Planes that are track and put it into the list
            Session.GetTrackables(List_AllPlanes);
        }
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
                    for (int i = 0; i < SelectionLevels.Length; ++i)
                    {
                        if(SelectionLevels[i].name == hit.transform.gameObject.name)
                        {
                            CurrentLevelSelection = i;
                            ToSelectionScreen_Planet();
                            break;
                        }
                    }
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

    private void SelectionScreenUpdate_Planet()
    {
        SelectionLevels[CurrentLevelSelection].transform.Rotate(gameObject.transform.up, WorldRotationSpeed * Time.deltaTime);
    }
    Touch RememberedTouch;

    private void GameScreenUpdate()
    {
        // Gets all Planes that are track and put it into the list
        //Session.GetTrackables(List_AllPlanes);

        if (!isSpawned && Input.touchCount > 0)
        {
            RememberedTouch = Input.GetTouch(0);
            Spawner(RememberedTouch);
        }
        else if (GameObjPrefab == null)
        {
            isSpawned = false;
            ToSelectionScreen_Planet();
            return;
        }

        foreach (DetectedPlane thePlane in List_AllPlanes)
        {
            if (thePlane.TrackingState == TrackingState.Stopped)
            {
                DestroyCurrentLevel();
                ToSelectionScreen_Universe();
                break;
            }
        }
    }

    public void DestroyCurrentLevel()
    {
        //Destroy(GameObjPrefab);
        Destroy(_GroundObject);
        GameObjPrefab = null;
    }

    public void RestartLevel()
    {
        DestroyCurrentLevel();
        SetNextObject();
        Spawner(RememberedTouch);
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
    public void SetNextObject()
    {
        //Temporary level select hardcode method
        //GameObjPrefab = Arr_LevelsOBJ[CurrentLevelSelection];

        string _ObjName = Arr_LevelsOBJ[CurrentLevelSelection].name;

        foreach (GameObject PrefabLevel in Arr_LevelsOBJ)
        {
            if (_ObjName == PrefabLevel.name)
            {
                DestroyCurrentLevel();
                GameObjPrefab = PrefabLevel;
                ToGameScreen();
                break;
            }
        }
    }

    public void ToSelectionScreen_Universe()
    {
        SplashScreen.SetActive(false);
        SelectionScreen.SetActive(false);
        ScreenSpaceCanvas.SetActive(false);
        SelectionLevels[CurrentLevelSelection].SetActive(false);

        if(_GroundObject != null)
        {
            _GroundObject.SetActive(true);
        }

        ScreenState = STATE_SCREEN.SCREEN_SELECTION_UNIVERSE;
    }

    public void ToSelectionScreen_Planet()
    {
        if (!SelectionScreen.activeSelf)
        {
            SelectionScreen.SetActive(true);
            SplashScreen.SetActive(false);
            GameScreen.SetActive(false);
            ScreenSpaceCanvas.SetActive(true);

            PauseManager PauseBarScript = PauseBar.GetComponent<PauseManager>();
            if (PauseBarScript.isPauseBarOpen)
            {
                PauseBarScript.PauseButtonDown();
            }
            PauseBar.SetActive(false);
        }

        SelectionLevels[CurrentLevelSelection].SetActive(true);
        _GroundObject.SetActive(false);
        ScreenState = STATE_SCREEN.SCREEN_SELECTION_PLANET;
    }

   public void ToGameScreen()
    {
        if (GameObjPrefab != null)
        {
            if (!GameScreen.activeSelf)
            {
                GameScreen.SetActive(true);
                SplashScreen.SetActive(false);
                SelectionScreen.SetActive(false);
                ScreenSpaceCanvas.SetActive(false);
                PauseBar.SetActive(true);
            }
            
            ScreenState = STATE_SCREEN.SCREEN_GAME;
            isSpawned = false;
        }
    }
}