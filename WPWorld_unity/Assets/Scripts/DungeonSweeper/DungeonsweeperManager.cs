using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonsweeperManager : MonoBehaviour
{
    public enum BlockType
    {   
        EMPTY,
        NORMAL,
        BOMB,
        TOTAL_BLOCKTYPE
    }

    public enum BlockNumberType
    {
        ZERO = 0,
        ONE,
        TWO,
        THREE,
        FOUR,
        FIVE,
        SIX,
        SEVEN,
        EIGHT,
        EMPTY,
        TOTAL_BLOCKNUMBERTYPE
    }

    public enum AnchorPointType // Follow the list way of indexing
    {
        ANCHOR_ONE = 0,
        ANCHOR_TWO,
        ANCHOR_THREE,
        ANCHOR_FOUR,
        TOTAL_MAINANCHORTYPE
    }

    public enum LevelType
    {
        EMPTY = 0,
        LEVEL_ONE,
        LEVEL_TWO,
        TOTAL_LEVEL
    }

    public enum HeightType
    {
        ONE = 1,
        TWO,
        THREE,
        TOTAL_HEIGHT,
    }

    [Tooltip("The list of main anchor point to spawn the stage")]
    public List<GameObject> List_Anchors;
    [Tooltip("The list of main block textures")]
    public List<Texture> List_BlockMat;
    [Tooltip("The list of numbered textures")]
    public List<Texture> List_NumberBlockMat;
    [Tooltip("The list of Stage Sizes")]
    public List<GameObject> List_StageSizesPrefab;
    [Tooltip("Current Level")]
    public LevelType Curr_Level;

    public GameObject PlatformManager;

    [Tooltip("0 = No spawn, 1 = More Spawn")]
    [Range(0, 10)]
    [SerializeField]
    private int BombSpawnRate;
    private AnchorPointType AnchorNumber;

    private void Awake()
    {
        Set_NextAnchorNumber(AnchorPointType.ANCHOR_ONE); // Start with the first       
        //BuildStage();
    }

    private void Update()
    {
        var _player = GameObject.FindGameObjectWithTag("Player");
        var _playerScript = _player.GetComponent<DSPlayer>();

        // Always check this first
        Check_PlayerPosition();

        // Levels logic
        switch (Curr_Level)
        {
            case LevelType.LEVEL_ONE:
                if(CheckAnchorEmpty(List_Anchors[(int)AnchorNumber]))
                {
                    BombSpawnRate = 8;
                    BuildStage(List_StageSizesPrefab[1]);
                }              
                break;
            case LevelType.LEVEL_TWO:
                if (CheckAnchorEmpty(List_Anchors[(int)AnchorNumber]))
                {
                    BombSpawnRate = 8;
                    BuildStage(List_StageSizesPrefab[2]);
                }
                var _anchorScript = List_Anchors[(int)AnchorNumber].GetComponent<AnchorPoint>();
                if(_anchorScript.m_isdone)
                {
                    Set_NextAnchorNumber(AnchorNumber + 1);
                    BombSpawnRate = 7;
                    BuildStage(List_StageSizesPrefab[1]);
                }
                break;
        }


        Triggered_Render();     
    }

    // 0000000000000000000000000000000000000000000
    //              PRIVATE METHOD
    // 0000000000000000000000000000000000000000000

    // Sets the preloaded stage sizes from prefabs
    private void Build_NextStageSize(GameObject _prefab)
    {
        Instantiate(_prefab, List_Anchors[(int)AnchorNumber].transform.position, Quaternion.identity, List_Anchors[(int)AnchorNumber].transform);
    }   

    // Sets the next anchor point to spawn the grid
    private void Set_NextAnchorNumber(AnchorPointType _anchor)
    {
        AnchorNumber = _anchor;
    } 

    // Setup the playing field
    private void GridSetUp()
    {
        // Find the children of Anchors
        var _allchildblocks = List_Anchors[(int)AnchorNumber].GetComponentsInChildren<Transform>();

        // Loop all child in the parent
        foreach (Transform _child in _allchildblocks)
        {
            if(_child.gameObject.tag != "Blocks")
            {
                continue;
            }

            var _childScript = _child.gameObject.GetComponent<Blocks>();
            var _childMat = _child.gameObject.GetComponent<Renderer>().material;
            var _AnchorScript = List_Anchors[(int)AnchorNumber].GetComponent<AnchorPoint>();

            while (_childScript.m_BlockType == BlockType.EMPTY)
            {
                var _RNG = Random.Range(1, (int)BlockType.TOTAL_BLOCKTYPE);

                if ((BlockType)_RNG == BlockType.BOMB) // BOMB
                {
                    var _crtlRNG = Random.Range(0, BombSpawnRate); // Further rng it
                    if (_crtlRNG == BombSpawnRate - 1)
                    {
                        _childScript.m_BlockType = (BlockType)_RNG; // Apply the bomb block
                        _AnchorScript.m_numBomb += 1;
                    }
                }
                else
                {
                    _childScript.m_BlockType = (BlockType)_RNG; // Apply normal block
                }               
            }

            _childMat.mainTexture = List_BlockMat[1];
        
            _AnchorScript.mList_Blocks.Add(_child.gameObject);
            _AnchorScript.m_totalBlockCount = _AnchorScript.mList_Blocks.Count;
        }
    }

    // Set number of bombs into blocks
    private void Set_NumberBlocks()
    {
        var _AnchorScript = List_Anchors[(int)AnchorNumber].GetComponent<AnchorPoint>();
        foreach (GameObject _obj in _AnchorScript.mList_Blocks) // All the blocks in current anchor
        {
            if (_obj.tag != "Blocks")
            {
                continue;
            }

            var _objScript = _obj.GetComponent<Blocks>();

            if(_objScript.m_BlockType != BlockType.NORMAL)
            {
                continue;
            }

            Collider[] _arrCol = Physics.OverlapSphere(_obj.transform.position, _obj.transform.localScale.x / 10);
            int _bombCount = 0;

            foreach(Collider _col in _arrCol)
            {
                var _colScript = _col.gameObject.GetComponent<Blocks>();                

                if(_colScript.m_BlockType == BlockType.BOMB)
                {
                    _bombCount += 1;
                }
            }

            _objScript.m_BlockNumberType = (BlockNumberType)_bombCount;
        }
    }

    // Renders triggered blocks
    private void Triggered_Render()
    {
        var _player = GameObject.FindGameObjectWithTag("Player");
        var _playerScript = _player.GetComponent<DSPlayer>();
        var _AnchorScript = List_Anchors[(int)_playerScript.m_PlayerAnchorPosition].GetComponent<AnchorPoint>();
        foreach (GameObject _obj in _AnchorScript.mList_Blocks)
        {
            if (_obj.tag != "Blocks")
            {
                continue;
            }

            var _objScript = _obj.GetComponent<Blocks>();

            if (_objScript.m_isTriggered)
            {
                var _objMat = _obj.GetComponent<Renderer>().material;

                if (_objScript.m_BlockType == BlockType.NORMAL)
                {
                    _objMat.mainTexture = List_NumberBlockMat[(int)_objScript.m_BlockNumberType];
                    if (_objScript.m_BlockNumberType == BlockNumberType.ZERO)
                    {
                        Triggered_Number(_obj);
                    }
                }
                else
                {                 
                    _objMat.mainTexture = List_BlockMat[(int)_objScript.m_BlockType];
                }            
            }
        }
    }

    // Triggers the Numbers for normal blocks
    private void Triggered_Number(GameObject _obj)
    {     
        // A list of rays being casted
        List<RaycastHit> _listRays = new List<RaycastHit>();

        // Check the surroundings of the blocks except down and up
        RaycastHit _ray;
        if (Physics.Raycast(_obj.transform.position, gameObject.transform.forward, out _ray)) // Forward
        {
            _listRays.Add(_ray);
        }
        if (Physics.Raycast(_obj.transform.position, -gameObject.transform.forward, out _ray)) // Backward
        {
            _listRays.Add(_ray);
        }
        if (Physics.Raycast(_obj.transform.position, gameObject.transform.right, out _ray)) // Right
        {
            _listRays.Add(_ray);
        }
        if (Physics.Raycast(_obj.transform.position, -gameObject.transform.right, out _ray)) // Left
        {
            _listRays.Add(_ray);
        }
        /*
        if (Physics.Raycast(_obj.transform.position, gameObject.transform.up, out _ray)) // UP
        {
            _listRays.Add(_ray);
        }
        if (Physics.Raycast(_obj.transform.position, -gameObject.transform.up, out _ray)) // down
        {
            _listRays.Add(_ray);
        }
        */

        // Loop through all directions from above to reveal the block's material
        foreach (RaycastHit _hit in _listRays)
        {           
            if(_hit.transform.gameObject.tag != "Blocks")
            {
                continue;
            }
            if (Vector3.Distance(_obj.transform.position, _hit.transform.position) <= _obj.transform.localScale.x) // Checks the distance
            {
                var _hitScript = _hit.transform.gameObject.GetComponent<Blocks>();

                if(_hitScript.m_isTriggered)
                {
                    continue;
                }

                if(_hitScript.m_BlockType == BlockType.NORMAL)
                {
                    _hitScript.m_isTriggered = true;
                    if(_hitScript.m_BlockNumberType == BlockNumberType.ZERO)
                    {
                        Triggered_Number(_hit.transform.gameObject);
                    }
                }
            }     
        }
    }

    // Builds the stage
    private void BuildStage()
    {
        Build_NextStageSize(List_StageSizesPrefab[0]); // Testing purposes

        GridSetUp();
        Set_NumberBlocks();
    }
    // Builds the stage
    private void BuildStage(GameObject _prefab)
    {
        Build_NextStageSize(_prefab); // Testing purposes

        GridSetUp();
        Set_NumberBlocks();
    }

    private void Check_PlayerPosition()
    {
        var _player = GameObject.FindGameObjectWithTag("Player");
        var _playerScript = _player.GetComponent<DSPlayer>();
        GameObject _closestAnchor = List_Anchors[0]; 

        foreach(GameObject _anchorPos in List_Anchors)
        {                    
            if(Vector3.Distance(_player.transform.position,_anchorPos.transform.position) < Vector3.Distance(_player.transform.position, _closestAnchor.transform.position))
            {
                _closestAnchor = _anchorPos;           
            }
        }   

        _playerScript.m_PlayerAnchorPosition = (AnchorPointType)List_Anchors.IndexOf(_closestAnchor);
    }

    // Delete grid when the platform is done
    private void Detele_Grid()
    {
        foreach(GameObject _anchor in List_Anchors)
        {
            var _anchorScript = _anchor.GetComponent<AnchorPoint>();
            var _anchorchild = _anchor.GetComponentsInChildren<Transform>();
            if(_anchorScript.m_isdone)
            {               
                foreach (Transform _kill in _anchorchild)
                {
                    if(_kill.gameObject.tag == "Anchor")
                    {
                        continue;
                    }
                    Destroy(_kill.gameObject);
                }            
                _anchorScript.mList_Blocks.Clear();
                _anchorScript.Reset_Variables();
            }
        }
    }

    // Delete objects in that anchor
    private void Detele_Grid(GameObject _anchor)
    {
        var _anchorScript = _anchor.GetComponent<AnchorPoint>();
        var _anchorchild = _anchor.GetComponentsInChildren<Transform>();

        foreach (Transform _kill in _anchorchild)
        {
            if (_kill.gameObject.tag == "Anchor")
            {
                continue;
            }
            Destroy(_kill.gameObject);
        }
        _anchorScript.mList_Blocks.Clear();
        _anchorScript.Reset_Variables();
    }

    //Check Anchor
    private bool CheckAnchorEmpty(GameObject _anchor)
    {   
        var _anchorchild = _anchor.GetComponentsInChildren<Transform>();

        foreach (Transform _obj in _anchorchild)
        {
            if (_obj.gameObject.tag == "Blocks")
            {
                return false;                
            }          
        }
        return true;
    }

    // 000000000000000000000000000000000000000000
    //              PUBLIC METHOD
    // 000000000000000000000000000000000000000000

    // Set the level
    public void Set_DungeonSweeperLevel(LevelType _levelType)
    {
        Curr_Level = _levelType;
    }

    public void SpawnNext() // Force spawn a stage
    {
        AnchorPointType _currPoint = AnchorNumber;
        if(AnchorNumber == AnchorPointType.ANCHOR_FOUR)
        {
            Set_NextAnchorNumber(AnchorPointType.ANCHOR_ONE);
        }
        else
        {
            Set_NextAnchorNumber(AnchorNumber + 1);
        }

        // Spawns the platform to link the 2 grid up
        PlatformManager.transform.position = List_Anchors[(int)_currPoint].transform.position + ((List_Anchors[(int)AnchorNumber].transform.position - List_Anchors[(int)_currPoint].transform.position) / 2);       
        var _PlatformMangerScript = PlatformManager.GetComponent<DSPlatformManager>();
        _PlatformMangerScript.SpawnPlatform();

        BuildStage();
    }
}