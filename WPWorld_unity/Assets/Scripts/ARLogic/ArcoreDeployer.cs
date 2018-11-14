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

    //----GAME OBJECTS----//
    [SerializeField]
    Camera MainCamera;
    [SerializeField]
    GameObject MainCameraRef;
    [SerializeField]
    GameObject[] Arr_LevelsOBJ;
    [SerializeField]
    GameObject AnchorRef;

    GameObject GameObjPrefab = null;
    SoundSystem soundSystem = null;
    
    //Reference to the clone of GameObjPrefab
    GameObject _GroundObject = null;
    Anchor _anchor;

    bool isSpawned = false;
    Vector3 FirstTouchWorldPoint = new Vector3();
    List<DetectedPlane> List_AllPlanes = new List<DetectedPlane>();

    //----UI OBJECTS----//
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

    //Arrays that store the individual objects in each screen
    private GameObject[] SplashScreenObjects;
    private GameObject[] SelectionScreen_PlanetsObjects;
    private GameObject[] SelectionScreen_StageObjects;
    private GameObject[] GameMoveAnchorObjects;
    private GameObject[] GameScreenObjects;

    //----UI LOGIC VARIABLES----//
    [SerializeField]
    float WorldRotationSpeed = 10;
    [SerializeField]
    float DistanceBetweenStageSelectButtons = 10;
    [SerializeField]
    int World00NumStages_Tutorial = 1;
    [SerializeField]
    int World01NumStages_PuzzleMaze = 3;
    [SerializeField]
    int World02NumStages_Platformer = 3;
    [SerializeField]
    int World03NumStages_DungeonSweeper = 3;
    [SerializeField]
    int World04NumStages_AsteroidRun = 1;
    [SerializeField]
    int World05NumStages_Credits = 1;

    private void Start()
    {
        //Define the game object references
        SplashScreenObjects = GameObject.FindGameObjectsWithTag("SplashScreen");
        SelectionScreen_PlanetsObjects = GameObject.FindGameObjectsWithTag("SelectionScreen_Planets");
        SelectionScreen_StageObjects = GameObject.FindGameObjectsWithTag("SelectionScreen_Stage");
        GameMoveAnchorObjects = GameObject.FindGameObjectsWithTag("GameMoveAnchorScreen");
        GameScreenObjects = GameObject.FindGameObjectsWithTag("GameScreen");
        soundSystem = GameObject.FindGameObjectWithTag("SoundSystem").GetComponent<SoundSystem>();

        //Initialise the screens
        ExitSelectionScreen_Planet();
        ExitSelectionScreen_Stage();
        ExitGameMoveAnchor(true);
        ExitGameScreen();
        ToSplashScreen();
        
        //Make the world selection button invisible
        Image WorldSelectButtonImage = WorldSelectBtn.GetComponent<Image>();
        Color NewColor = WorldSelectButtonImage.color;
        NewColor.a = 0;
        WorldSelectButtonImage.color = NewColor;
    }

    private void Update()
    {
        //Update the current screen
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
        //Change the screen state to the current one
        ScreenState = STATE_SCREEN.SCREEN_SPLASH;

        //Set all splash screen objects to active
        foreach (GameObject obj in SplashScreenObjects)
        {
            obj.SetActive(true);
        }
    }

    public void SplashScreenUpdate()
    {
        if (Input.touchCount > 0)
        {
            //Change to the universe screen
            ExitSplashScreen();
            ToSelectionScreen_Universe();

            // Gets all Planes that are track and put it into the list
            Session.GetTrackables(List_AllPlanes);
        }
    }

    public void ExitSplashScreen()
    {
        //Set all splash screen objects to inactive
        foreach (GameObject obj in SplashScreenObjects)
        {
            obj.SetActive(false);
        }
    }
    //-----------------------------------------------------------------//

    //-----SELECTION SCREEN UNIVERSE FUNCTIONS-----//
    public void ToSelectionScreen_Universe()
    {
        //Ground object is the object spawned by touch, so check if it is null first
        if (_GroundObject != null)
        {
            _GroundObject.SetActive(true);
        }

        ScreenState = STATE_SCREEN.SCREEN_SELECTION_UNIVERSE;
    }

    private void SelectionScreenUpdate_Universe()
    {
        //Check if universe has been spawned yet
        if (!isSpawned && Input.touchCount > 0)
        {
            //Spawn the universe
            GameObjPrefab = UniverseObj;
            Spawner(Input.GetTouch(0), UniverseObj);
        }
        //Shoot a raycast from the point where the touch occured on the screen and into the 3D space
        else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            //Get the touch information
            Touch theTouch = Input.GetTouch(0);

            //The camera has a farclipplane which is a plane where any game objects beyond this plane will not be rendered to save resources i.e. object culling
            //The nearcliplane is the plane where any objects behind it will not be rendered
            //Create a position vector on each plane by moving the xy touch coordinates from the screen to the planes
            Vector3 touchPosFar = new Vector3(theTouch.position.x, theTouch.position.y, MainCamera.farClipPlane);
            Vector3 touchPosNear = new Vector3(theTouch.position.x, theTouch.position.y, MainCamera.nearClipPlane);

            //Convert the position vectors to world space coordinates
            Vector3 touchPosF = MainCamera.ScreenToWorldPoint(touchPosFar);
            Vector3 touchPosN = MainCamera.ScreenToWorldPoint(touchPosNear);

            //Create the raycast
            RaycastHit hit;

            //Create a line and check if the line collides with a game object
            if (Physics.Raycast(touchPosN, touchPosF - touchPosN, out hit))
            {
                //Checks if the line collides with a planet in the universe
                if (hit.transform.gameObject.tag == "Planet")
                {
                    //Changes the name of the world to what was selected
                    CurrentWorldName.text = hit.transform.gameObject.name;
                    //Change the material of the selected world in the UI to the same as the one that was selected in the universe
                    SelectedWorld.GetComponent<MeshRenderer>().material = hit.transform.gameObject.GetComponent<MeshRenderer>().material;

                    //Change the current screen to the planet selection
                    ExitSelectionScreen_Universe();
                    ToSelectionScreen_Planet();
                }
            }
        }
        else if (isSpawned) //Universe is already spawned
        {
            //Rotate each planet in the universe for visual effect
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
                ExitSelectionScreen_Universe(true);
            }
        }
    }

    public void ExitSelectionScreen_Universe(bool DestroyUniverse = false)
    {
        //If universe is marked to be destroyed, then destroy it
        if (DestroyUniverse)
        {
            DestroyCurrentLevel();
        }
        //If not, just set the universe to inactive
        else if (_GroundObject != null) 
        {
            _GroundObject.SetActive(false);
        }
    }
    //-----------------------------------------------------------------//

    //-----SELECTION SCREEN PLANET FUNCTIONS-----//
    public void ToSelectionScreen_Planet()
    {
        //Set all selection planet screen objects to active
        foreach (GameObject obj in SelectionScreen_PlanetsObjects)
        {
            obj.SetActive(true);
        }

        ScreenState = STATE_SCREEN.SCREEN_SELECTION_PLANET;
    }

    private void SelectionScreenUpdate_Planet()
    {
        //Rotate the selected planet for visual effect
        SelectedWorld.transform.Rotate(gameObject.transform.up, WorldRotationSpeed * Time.deltaTime);
    }

    public void ExitSelectionScreen_Planet(bool DestroyUniverse = false)
    {
        //If marked to destroy universe, destroy the universe
        if (DestroyUniverse)
        {
            DestroyCurrentLevel();
        }

        //Set all selection planet screen objects to inactive
        foreach (GameObject obj in SelectionScreen_PlanetsObjects)
        {
            obj.SetActive(false);
        }
    }
    //-----------------------------------------------------------------//

    //-----SELECTION SCREEN STAGE FUNCTIONS-----//
    public void ToSelectionScreen_Stage(bool checkObject = false)
    {
        //Check if the ground object is null
        if(checkObject)
        {
            if(_GroundObject == null)
            {
                return;
            }
            else
            {
                Reset_Anchor();
            }
        }

        int NumOfStages = 0;
        
        switch (CurrentWorldName.text)
        {
            case "Tutorial World":
                {
                    NumOfStages = World00NumStages_Tutorial;
                    break;
                }
            case "Puzzle Maze World":
                {
                    NumOfStages = World01NumStages_PuzzleMaze;
                    break;
                }
            case "Platformer World":
                {
                    NumOfStages = World02NumStages_Platformer;
                    break;
                }
            case "DungeonSweeper World":
                {
                    NumOfStages = World03NumStages_DungeonSweeper;
                    break;
                }
            case "Asteroid World":
                {
                    NumOfStages = World04NumStages_AsteroidRun;
                    break;
                }
            case "Credits World":
                {
                    NumOfStages = World05NumStages_Credits;
                    break;
                }
            default:
                break;
        }

        //Create the first stage select button
        GameObject FirstStageSelectBtn = Instantiate(StageSelectBtn, StageSelect.transform, false);
        FirstStageSelectBtn.GetComponent<RectTransform>().localPosition = new Vector3(0, (NumOfStages - 1) * (DistanceBetweenStageSelectButtons * 0.5f), 0);
        FirstStageSelectBtn.name = "Stage01";
        FirstStageSelectBtn.transform.GetChild(0).GetComponent<Text>().text = "Stage 01";
        FirstStageSelectBtn.GetComponent<Button>().onClick.AddListener(delegate { ExitSelectionScreen_Stage(true); });
        FirstStageSelectBtn.GetComponent<Button>().onClick.AddListener(delegate { SelectStage(FirstStageSelectBtn.name); });
        FirstStageSelectBtn.GetComponent<Button>().onClick.AddListener(delegate { ToGameMoveAnchor(); });
        Vector3 localPos = FirstStageSelectBtn.GetComponent<RectTransform>().localPosition;

        //If there is more than 1 stage, create more stage select buttons
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
                theStageSelectBtn.GetComponent<Button>().onClick.AddListener(delegate { ToGameMoveAnchor(); });
                theStageSelectBtn.transform.GetChild(0).GetComponent<Text>().text = "Stage 0" + (i + 1).ToString();
            }
        }

        //Set all stage selection screen objects to active
        foreach (GameObject obj in SelectionScreen_StageObjects)
        {
            obj.SetActive(true);
        }

        ScreenState = STATE_SCREEN.SCREEN_SELECTION_STAGE;
    }

    public void SelectStage(string StageNum)
    {
        string WorldNum = "";

        //Get the world number
        switch (CurrentWorldName.text)
        {
            case "Tutorial World":
                {
                    WorldNum = "World00";
                    break;
                }
            case "Puzzle Maze World":
                {
                    WorldNum = "World01";
                    break;
                }
            case "Platformer World":
                {
                    WorldNum = "World02";
                    break;
                }
            case "DungeonSweeper World":
                {
                    WorldNum = "World03";
                    break;
                }
            case "Asteroid World":
                {
                    WorldNum = "World04";
                    break;
                }
            case "Credits World":
                {
                    WorldNum = "World05";
                    break;
                }
            default:
                break;
        }
        
        //Set the next level to be spawned
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

        //Set all stage selection screen objects to inactive
        foreach (GameObject obj in SelectionScreen_StageObjects)
        {
            obj.SetActive(false);
        }
    }
    //-----------------------------------------------------------------//

    //-----GAME MOVE ANCHOR FUNCTIONS-----//
    public void ToGameMoveAnchor()
    {
        //Set all game move anchor screen objects to active
        foreach (GameObject obj in GameMoveAnchorObjects)
        {
            obj.SetActive(true);
        }

        //Set the anchor reference object to be invisible first
        AnchorRef.GetComponent<MeshRenderer>().enabled = false;
        ScreenState = STATE_SCREEN.SCREEN_GAME_MOVEANCHOR;
        isSpawned = false;
    }

    public void GameMoveAnchorUpdate()
    {
        //Set the Main camera refence object to always be in the same pos of the camera
        MainCameraRef.transform.position = MainCamera.transform.position;
        //Set the forward to always be in the direction of camera but never allow the y axis to change to ensure the game anchor object to only move on the xz plane
        MainCameraRef.transform.forward = new Vector3(MainCamera.transform.forward.x, MainCamera.transform.position.y, MainCamera.transform.forward.z);

        if(_GroundObject != null)
        {
            AnchorRef.transform.position = _GroundObject.transform.position;
            UpdateOffSet();
        }

        if (!isSpawned && Input.touchCount > 0)
        {
            //Spawn the anchor game object
            Spawner(Input.GetTouch(0), AnchorRef);
            _GroundObject.GetComponent<MeshRenderer>().enabled = true;
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

    //Reset the anchor game object
    public void Reset_Anchor()
    {
        Destroy(_GroundObject);
        _GroundObject = null;
        isSpawned = false;
    }

    public void ExitGameMoveAnchor(bool isStartFunc = false)
    {
        if(_GroundObject == null && !isStartFunc)
        {
            return;
        }

        //Set all game move anchor screen objects to inactive
        foreach (GameObject obj in GameMoveAnchorObjects)
        {
            obj.SetActive(false);
        }
    }
    //-----------------------------------------------------------------//

    //-----GAME SCREEN FUNCTIONS-----//
    public void ToGameScreen()
    {
        //Don't go to game screen if ground object is null
        if (_GroundObject == null)
        {
            return;
        }

        Reset_Anchor();

        //Set all game screen objects to active
        foreach (GameObject obj in GameScreenObjects)
        {
            obj.SetActive(true);
        }

        ScreenState = STATE_SCREEN.SCREEN_GAME;
        isSpawned = false;

        //Spawn the level
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

        //Set all game move anchor screen objects to inactive
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

        if(FirstTouchWorldPoint != null)
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
        _GroundObject = Instantiate(GameObjPrefab, AnchorRef.transform.position, AnchorRef.transform.rotation, _anchor.transform);
    }

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

    //UI BUTTON SOUNDS
    public void PlayButtonSound()
    {
        soundSystem.PlaySFX("UIButton");
    }

    public void PlayDPadSound()
    {
        soundSystem.PlaySFX("DPadClickSound");
    }
}