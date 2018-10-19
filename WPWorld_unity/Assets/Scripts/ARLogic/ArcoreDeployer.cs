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
        SCREEN_SELECTION_STAGE,
        SCREEN_GAME_MOVEANCHOR,
        SCREEN_GAME,

        SCREEN_TOTAL
    }
    STATE_SCREEN ScreenState;

    // Game Objects
    [SerializeField]
    Camera MainCamera;
    [SerializeField]
    GameObject MainCameraRef;
    [SerializeField]
    GameObject[] Arr_LevelsOBJ;
    [SerializeField]
    GameObject AnchorRef;

    private List<DetectedPlane> List_AllPlanes = new List<DetectedPlane>();
    private GameObject GameObjPrefab = null;
    private bool isSpawned = false;
    private Vector3 FirstTouchWorldPoint = new Vector3();
    
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
    GameObject StageSelect;
    [SerializeField]
    GameObject StageSelectBtn;

    [SerializeField]
    Text DebugText;

    private GameObject[] SplashScreenObjects;
    private GameObject[] SelectionScreen_PlanetsObjects;
    private GameObject[] SelectionScreen_StageObjects;
    private GameObject[] GameMoveAnchorObjects;
    private GameObject[] GameScreenObjects;

    //UI Logic Variables
    [SerializeField]
    float WorldRotationSpeed = 10;
    [SerializeField]
    float DistanceBetweenStageSelectButtons = 10;
    [SerializeField]
    int World01NumStages_PuzzleMaze = 3;
    [SerializeField]
    int World02NumStages_Tetris = 3;
    [SerializeField]
    int World03NumStages_Platformer = 3;
    [SerializeField]
    int World04NumStages_DungeonSweeper = 1;
    [SerializeField]
    int World05NumStages_AsteroidRun = 1;

    private void Start()
    {
        SplashScreenObjects = GameObject.FindGameObjectsWithTag("SplashScreen");
        SelectionScreen_PlanetsObjects = GameObject.FindGameObjectsWithTag("SelectionScreen_Planets");
        SelectionScreen_StageObjects = GameObject.FindGameObjectsWithTag("SelectionScreen_Stage");
        GameMoveAnchorObjects = GameObject.FindGameObjectsWithTag("GameMoveAnchorScreen");
        GameScreenObjects = GameObject.FindGameObjectsWithTag("GameScreen");

        ExitSelectionScreen_Planet();
        ExitSelectionScreen_Stage();
        ExitGameMoveAnchor();
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
            case STATE_SCREEN.SCREEN_GAME_MOVEANCHOR:
                {
                    GameMoveAnchorUpdate();
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

    //-----SPLASH SCREEN FUNCTIONS-----//
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
    //-----------------------------------------------------------------//

    //-----SELECTION SCREEN UNIVERSE FUNCTIONS-----//
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
            Spawner(Input.GetTouch(0), UniverseObj);
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
    //-----------------------------------------------------------------//

    //-----SELECTION SCREEN PLANET FUNCTIONS-----//
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
    //-----------------------------------------------------------------//


    //-----SELECTION SCREEN STAGE FUNCTIONS-----//
    public void ToSelectionScreen_Stage()
    {
        int NumOfStages = 0;

        switch (CurrentWorldName.text)
        {
            case "Puzzle Maze World":
                {
                    NumOfStages = World01NumStages_PuzzleMaze;
                    break;
                }
            case "Platformer World":
                {
                    NumOfStages = World03NumStages_Platformer;
                    break;
                }
            case "DungeonSweeper World":
                {
                    NumOfStages = World04NumStages_DungeonSweeper;
                    break;
                }

            case "Asteroid World":
                {
                    NumOfStages = World05NumStages_AsteroidRun;
                    break;
                }
            default:
                break;
        }

        GameObject FirstStageSelectBtn = Instantiate(StageSelectBtn, StageSelect.transform, false);
        FirstStageSelectBtn.GetComponent<RectTransform>().localPosition = new Vector3(0, (NumOfStages - 1) * (DistanceBetweenStageSelectButtons * 0.5f), 0);
        FirstStageSelectBtn.name = "Stage01";
        FirstStageSelectBtn.transform.GetChild(0).GetComponent<Text>().text = "Stage 01";
        FirstStageSelectBtn.GetComponent<Button>().onClick.AddListener(delegate { ExitSelectionScreen_Stage(true); });
        FirstStageSelectBtn.GetComponent<Button>().onClick.AddListener(delegate { SelectStage(FirstStageSelectBtn.name); });
        FirstStageSelectBtn.GetComponent<Button>().onClick.AddListener(delegate { ToGameMoveAnchor(); });
        //FirstStageSelectBtn.GetComponent<Button>().onClick.AddListener(delegate { ToGameScreen(); });
        Vector3 localPos = FirstStageSelectBtn.GetComponent<RectTransform>().localPosition;

        if (NumOfStages > 1)
        {
            for (int i = 1; i < NumOfStages; ++i)
            {
                GameObject theStageSelectBtn = Instantiate(StageSelectBtn, StageSelect.transform, false);
                localPos.y -= (DistanceBetweenStageSelectButtons);
                theStageSelectBtn.GetComponent<RectTransform>().localPosition = localPos;
                theStageSelectBtn.name = "Stage0" + (i + 1).ToString();
                theStageSelectBtn.GetComponent<Button>().onClick.AddListener(delegate { ExitSelectionScreen_Stage(true); });
                theStageSelectBtn.GetComponent<Button>().onClick.AddListener(delegate { SelectStage(theStageSelectBtn.name); });
                theStageSelectBtn.GetComponent<Button>().onClick.AddListener(delegate { ToGameScreen(); });
                theStageSelectBtn.transform.GetChild(0).GetComponent<Text>().text = "Stage 0" + (i + 1).ToString();
            }
        }

        foreach (GameObject obj in SelectionScreen_StageObjects)
        {
            obj.SetActive(true);
        }

        ScreenState = STATE_SCREEN.SCREEN_SELECTION_STAGE;
    }

    public void SelectStage(string StageNum)
    {
        string WorldNum = "";

        switch (CurrentWorldName.text)
        {
            case "Puzzle Maze World":
                {
                    WorldNum = "World01";
                    break;
                }
            case "Platformer World":
                {
                    WorldNum = "World03";
                    break;
                }
            case "DungeonSweeper World":
                {
                    WorldNum = "World04";
                    break;
                }
            
            case "Asteroid World":
                {
                    WorldNum = "World05";
                    break;
                }
            default:
                break;
        }
        
        SetNextObject(WorldNum + '_' + StageNum);
    }

    public void ExitSelectionScreen_Stage(bool DestroyUniverse = false)
    {
        if (DestroyUniverse)
        {
            DestroyCurrentLevel();
        }

        for (int i = 1; i < StageSelect.transform.childCount; ++i)
        {
            Destroy(StageSelect.transform.GetChild(i).gameObject);
        }

        foreach (GameObject obj in SelectionScreen_StageObjects)
        {
            obj.SetActive(false);
        }
    }
    //-----------------------------------------------------------------//

    //-----GAME MOVE ANCHOR FUNCTIONS-----//
    public void ToGameMoveAnchor()
    {
        foreach (GameObject obj in GameMoveAnchorObjects)
        {
            obj.SetActive(true);
        }

        AnchorRef.GetComponent<MeshRenderer>().enabled = false;
        ScreenState = STATE_SCREEN.SCREEN_GAME_MOVEANCHOR;
        isSpawned = false;
    }

    public void GameMoveAnchorUpdate()
    {
        MainCameraRef.transform.position = MainCamera.transform.position;
        MainCameraRef.transform.forward = new Vector3(MainCamera.transform.forward.x, MainCamera.transform.position.y, MainCamera.transform.forward.z);

        if(_GroundObject != null)
        {
            AnchorRef.transform.position = _GroundObject.transform.position;
            UpdateOffSet();
        }

        if (!isSpawned && Input.touchCount > 0)
        {
            Spawner(Input.GetTouch(0), AnchorRef);
            _GroundObject.GetComponent<MeshRenderer>().enabled = true;
            _GroundObject.transform.localScale = new Vector3(0.5f, 0.1f, 0.5f);
        }

        foreach (DetectedPlane thePlane in List_AllPlanes)
        {
            if (thePlane.TrackingState == TrackingState.Stopped)
            {
                ExitGameMoveAnchor();
                ToSelectionScreen_Stage();
                break;
            }
        }
    }

    public void MoveAnchorForward()
    {
        _GroundObject.transform.position += MainCameraRef.transform.forward * Time.deltaTime;
    }

    public void MoveAnchorBackward()
    {
        _GroundObject.transform.position -= MainCameraRef.transform.forward * Time.deltaTime;
    }

    public void MoveAnchorLeftward()
    {
        _GroundObject.transform.position -= MainCameraRef.transform.right * Time.deltaTime;
    }

    public void MoveAnchorRightward()
    {
        _GroundObject.transform.position += MainCameraRef.transform.right * Time.deltaTime;
    }

    public void MoveAnchorUpward()
    {
        _GroundObject.transform.position += MainCameraRef.transform.up * Time.deltaTime;
    }

    public void MoveAnchorDownward()
    {
        _GroundObject.transform.position -= MainCameraRef.transform.up * Time.deltaTime;
    }

    public void ExitGameMoveAnchor()
    {
        if (_GroundObject != null)
        {
            Destroy(_GroundObject);
            _GroundObject = null;
        }

        foreach (GameObject obj in GameMoveAnchorObjects)
        {
            obj.SetActive(false);
        }
    }
    //-----------------------------------------------------------------//

    //-----GAME SCREEN FUNCTIONS-----//
    public void ToGameScreen()
    {
        foreach (GameObject obj in GameScreenObjects)
        {
            obj.SetActive(true);
        }

        ScreenState = STATE_SCREEN.SCREEN_GAME;
        isSpawned = false;

        SpawnLevel(Input.GetTouch(0));
    }

    private void GameScreenUpdate()
    {
        // Gets all Planes that are track and put it into the list
        //Session.GetTrackables(List_AllPlanes);

        //if (!isSpawned && Input.touchCount > 0)
        //{
        //    SpawnLevel(Input.GetTouch(0));
        //}

        foreach (DetectedPlane thePlane in List_AllPlanes)
        {
            if (thePlane.TrackingState == TrackingState.Stopped)
            {
                ExitGameScreen();
                ToSelectionScreen_Planet();
                break;
            }
        }

       UpdateOffSet();
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
    //-----------------------------------------------------------------//

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

        if(FirstTouchWorldPoint !=null)
        {
            FirstTouchWorldPoint = new Vector3();
        }
    }

    public void RestartLevel()
    {
        _GroundObject.SendMessage("Reset_Level");
    }

    // Add a new Object using point on screen and ARCore
    private void Spawner(Touch _touch, GameObject SpawnObject)
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
                _GroundObject = Instantiate(SpawnObject, _hit.Pose.position, _hit.Pose.rotation);

                // Get the position in the world space
                FirstTouchWorldPoint = _hit.Pose.position;

                // Create an anchor for ARCore to track the point of the real world
                _anchor = _hit.Trackable.CreateAnchor(_hit.Pose);

                // Make the ground object the child of the anchor
                _GroundObject.transform.parent = _anchor.transform;
                isSpawned = true;
            }
        }
    }

    private void SpawnLevel(Touch _touch)
    {
        //// Raycast from point on screen to real world
        //TrackableHit _hit;
        //var _raycastFilter = TrackableHitFlags.PlaneWithinPolygon | TrackableHitFlags.FeaturePointWithSurfaceNormal;
        //if (Frame.Raycast(_touch.position.x, _touch.position.y, _raycastFilter, out _hit))
        //{
        //    // Check if it the raycast is hitting the back of the plane 
        //    if ((_hit.Trackable is DetectedPlane) && Vector3.Dot(MainCamera.transform.position - _hit.Pose.position, _hit.Pose.rotation * Vector3.up) < 0)
        //    {
        //        Debug.Log("Hit at back of the current DetectedPlane");
        //    }
        //    else
        //    {
        //        // Instantiate the object at where it is hit
        //        _GroundObject = Instantiate(GameObjPrefab, AnchorRef.transform.position, AnchorRef.transform.rotation);

        //        // Get the position in the world space
        //        FirstTouchWorldPoint = _hit.Pose.position;

        //        // Create an anchor for ARCore to track the point of the real world
        //       var _anchor = _hit.Trackable.CreateAnchor(_hit.Pose);

        //        // Make the ground object the child of the anchor
        //        _GroundObject.transform.parent = _anchor.transform;
        //        isSpawned = true;
        //    }
        //}

        

        _GroundObject = Instantiate(GameObjPrefab, AnchorRef.transform.position, AnchorRef.transform.rotation, _anchor.transform);
    }

        //Reference to the clone of GameObjPrefab
        GameObject _GroundObject = null;
    Anchor _anchor;

    // oooooooooooooooooooooooooooooooooooooooo
    //            <Public Stuff> 
    // oooooooooooooooooooooooooooooooooooooooo


    // Sets the next game object (Works like a scene manager)
    private void SetNextObject(string StageName)
    {
        foreach (GameObject PrefabLevel in Arr_LevelsOBJ)
        {
            if (StageName == PrefabLevel.name)
            {
                DestroyCurrentLevel();
                GameObjPrefab = PrefabLevel;
                break;
            }
        }
    }

    // Shifts the object back if there is an offset
    private void UpdateOffSet()
    {
        if(_GroundObject.transform.position != FirstTouchWorldPoint)
        {
            Vector3.Lerp(_GroundObject.transform.position, FirstTouchWorldPoint, 0.1f);
        }
    }
}