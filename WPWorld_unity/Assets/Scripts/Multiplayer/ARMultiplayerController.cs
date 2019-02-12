using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GoogleARCore;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

/// <summary>
/// Controls the ARCORE
/// </summary>
public class ARMultiplayerController : MonoBehaviour, IOnEventCallback
{
    enum STATE_SCREEN
    {
        SCREEN_GAME_MOVEANCHOR,
        SCREEN_GAME,

        SCREEN_NONE
    }
    STATE_SCREEN ScreenState = STATE_SCREEN.SCREEN_NONE;

    [SerializeField]
    bool SinglePlayer = false;
    public static bool isSinglePlayer;
    public static bool isPlayerSpawned = false;

    //----GAME OBJECTS----//
    [Header("Game Objects")]
    [SerializeField]
    Camera MainCamera;
    [SerializeField]
    GameObject PlayerObjectPrefab;
    [SerializeField]
    Text DebugText;
    [SerializeField]
    Text DebugText2;
    [SerializeField]
    Text DebugText3;

    [Header("Move Anchor Screen Objects")]
    [SerializeField]
    GameObject MoveAnchorControlsUI;
    [SerializeField]
    GameObject MainCameraRef;
    [SerializeField]
    GameObject AnchorRef;
    [SerializeField]
    GameObject SpawnPlayersButton;

    [Header("Game Screen Objects")]
    [SerializeField]
    GameObject LevelObject;

    SoundSystem soundSystem = null;

    //Reference to the clone of GameObjPrefab
    public static GameObject _GroundObject = null;
    public static Vector3 SpawnPoint;
    public static GameObject LevelForwardAnchor;

    PhotonView photonView;
    GameObject[] LevelSpawnPoints;
    Anchor _anchor;
    
    bool isSpawned = false;
    public bool isWon;
    int NumOfPlayersReady = 0;

    Vector3 FirstTouchWorldPoint = new Vector3();
    List<DetectedPlane> List_AllPlanes = new List<DetectedPlane>();
    Dictionary<int, GameObject> PlayerGoDict = new Dictionary<int, GameObject>();
   

    private void Start()
    {
        isSinglePlayer = SinglePlayer;

        if(!PhotonNetwork.IsConnected)
        {
            isSinglePlayer = true;
        }

        //Define the game object references       
        //soundSystem = GameObject.FindGameObjectWithTag("SoundSystem").GetComponent<SoundSystem>();
        photonView = PhotonView.Get(this);

        SpawnPlayersButton.SetActive(false);
        ////Initialise Screens
        ToGameMoveAnchor();
    }

    private void Update()
    {
        switch (ScreenState)
        {
            case STATE_SCREEN.SCREEN_GAME_MOVEANCHOR:
                {
                    GameMoveAnchorUpdate();
                    break;
                }
            case STATE_SCREEN.SCREEN_GAME:
                {
                    GameScreenUpdate();
                    DebugText2.text = Head.LocalPlayerInstance.transform.localPosition.ToString();
                    break;
                }
            default:
                break;
        }

    }

    //-----GAME MOVE ANCHOR FUNCTIONS-----//
    public void ToGameMoveAnchor()
    {
        //Set all game move anchor screen objects to active
        MoveAnchorControlsUI.SetActive(true);
        AnchorRef.SetActive(true);

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

        //If move anchor has not been spawned yet
        if (!isSpawned && Input.touchCount > 0)
        {
            //Spawn the anchor game object
            Spawner(Input.GetTouch(0), AnchorRef);
            _GroundObject.GetComponent<MeshRenderer>().enabled = true;
        }
        else if (_GroundObject != null)
        {
            AnchorRef.transform.position = _GroundObject.transform.position;
            AnchorRef.transform.rotation = _GroundObject.transform.rotation;
            UpdateOffSet();
        }

        foreach (DetectedPlane thePlane in List_AllPlanes)
        {
            if (thePlane.TrackingState == TrackingState.Stopped)
            {
                break;
            }
        }
    }

    //Movement functions for the anchor
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
        if (_GroundObject == null && !isStartFunc)
        {
            return;
        }

        //Set all game move anchor screen objects to inactive
        MoveAnchorControlsUI.SetActive(false);
    }
    //-----------------------------------------------------------------//

    //-----GAME SCREEN FUNCTIONS-----//
    public void ToGameScreen()
    {
        //Don't go to game screen if ground object is null
        //if (_GroundObject == null)
        //{
        //    return;
        //}

        Reset_Anchor();
        ScreenState = STATE_SCREEN.SCREEN_GAME;
        //Spawn the level
        SpawnLevel(Input.GetTouch(0));
        AnchorRef.SetActive(false);
    }

    private void GameScreenUpdate()
    {
        UpdateOffSet();
    }

    public void ExitGameScreen()
    {
        DestroyCurrentLevel();
        isSpawned = false;
    }
    //-----------------------------------------------------------------//

    private void DestroyCurrentLevel()
    {
        if (_GroundObject != null)
        {
            Destroy(_GroundObject);
            _GroundObject = null;
        }

        if (LevelObject != null)
        {
            LevelObject = null;
        }

        if (FirstTouchWorldPoint != null)
        {
            FirstTouchWorldPoint = new Vector3();
        }
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
        _GroundObject = Instantiate(LevelObject, AnchorRef.transform.position, AnchorRef.transform.rotation, _anchor.transform);
        _GroundObject.tag = LevelObject.tag;

        LevelSpawnPoints = GameObject.FindGameObjectsWithTag("Respawn");
        LevelForwardAnchor = GameObject.FindGameObjectWithTag("LevelForwardAnchor");
        
        if (!isSinglePlayer)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                if (SceneManagerHelper.ActiveSceneName == "SNAKE2.0")
                {
                    for (int i = 0; i < PhotonNetwork.PlayerList.Length; ++i)
                    {
                        photonView.RPC("ReceiveSpawnPoint", PhotonNetwork.PlayerList[i], LevelSpawnPoints[0].name);
                    }
                }
                else
                {
                    for (int i = 0; i < PhotonNetwork.PlayerList.Length; ++i)
                    {
                        photonView.RPC("ReceiveSpawnPoint", PhotonNetwork.PlayerList[i], LevelSpawnPoints[i].name);
                    }
                }

                AddNumberOfPlayerReady();
            }
            else
            {
                //Tell the host that you have spawned the level
                photonView.RPC("AddNumberOfPlayerReady", RpcTarget.MasterClient);
            }
        }
        else
        {
                ReceiveSpawnPoint(LevelSpawnPoints[0].name);
                SpawnPlayer();
        }
    }

    // Shifts the object back if there is an offset
    private void UpdateOffSet()
    {
        if (_GroundObject.transform.position != FirstTouchWorldPoint)
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

    // Spawn Player button (for host)
    public void SpawnPlayerHost()
    {
        if(isSinglePlayer)
        {
            SpawnPlayer();
            return;
        }

        photonView.RPC("SpawnPlayer", RpcTarget.All);

        SpawnPlayersButton.SetActive(false);
    }

    [PunRPC]
    void ReceiveSpawnPoint(string SpawnPosName)
    {
        foreach (GameObject spawnpoint in LevelSpawnPoints)
        {
            if (spawnpoint.name == SpawnPosName)
            {
                SpawnPoint = spawnpoint.transform.localPosition;
                break;
            }
        }
    }

    [PunRPC]
    void AddNumberOfPlayerReady()
    {
        ++NumOfPlayersReady;
        DebugText.text = "Ready Platers :" + NumOfPlayersReady.ToString();
        if (NumOfPlayersReady == PhotonNetwork.PlayerList.Length)
        {
            SpawnPlayersButton.SetActive(true);
        }
    }

    [PunRPC]
    void SpawnPlayer()
    {
        isPlayerSpawned = true;

        if (SceneManagerHelper.ActiveSceneName == "SNAKE2.0" || isSinglePlayer)
<<<<<<< HEAD
        {       
            Instantiate(PlayerObjectPrefab, _GroundObject.transform.position, Quaternion.identity);    
=======
        {
            Instantiate(PlayerObjectPrefab, Vector3.zero, Quaternion.identity);
>>>>>>> a4cdb6605cb49f80d804a04a2768200d5fdeeeb2
            return;
        }

        PhotonNetwork.Instantiate(PlayerObjectPrefab.name, Vector3.zero, Quaternion.identity, 0);
    }

    public void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    public void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    public void OnEvent(EventData photonEvent)
    {
        if (!PlayerGoDict.ContainsKey(photonEvent.Sender))
        {
            GameObject[] PlayerGoList = GameObject.FindGameObjectsWithTag("Player");

            foreach (GameObject player in PlayerGoList)
            {
                if (player.GetPhotonView().OwnerActorNr == photonEvent.Sender)
                {
                    PlayerGoDict.Add(photonEvent.Sender, player);
                    break;
                }
            }
        }

        switch ((EventCodes.EVENT_CODES)photonEvent.Code)
        {
            case EventCodes.EVENT_CODES.PLAYER_POSITION_UPDATE:
                {
                    Vector3 PlayerLocalPos = (Vector3)photonEvent.CustomData;
                    PlayerGoDict[photonEvent.Sender].transform.localPosition = PlayerLocalPos;

                    break;
                }
            case EventCodes.EVENT_CODES.PLAYER_ROTATION_UPDATE:
                {
                    Quaternion PlayerLocalRot = (Quaternion)photonEvent.CustomData;
                    PlayerGoDict[photonEvent.Sender].transform.localRotation = PlayerLocalRot;

                    break;
                }
            default:
                break;
        }
    }
}