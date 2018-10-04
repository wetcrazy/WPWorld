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

    [Range(1, 10)]
    [SerializeField]
    private int BombSpawnRate = 1;
    private AnchorPointType AnchorNumber;
    private LevelType Level;

    private void Awake()
    {
        Set_NextAnchorNumber(AnchorPointType.ANCHOR_ONE); // Start with the first       
        BuildStage();
    }

    private void Update()
    {
        Check_PlayerPosition();
        Triggered_Render();
    }

    // 0000000000000000000000000000000000000000000
    //              PRIVATE METHOD
    // 0000000000000000000000000000000000000000000

    // Sets the preloaded stage sizes from prefabs
    private void Set_NextStageSize(GameObject _prefab)
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

            while(_childScript.m_BlockType == BlockType.EMPTY)
            {
                var _RNG = Random.Range(1, (int)BlockType.TOTAL_BLOCKTYPE);

                if ((BlockType)_RNG == BlockType.BOMB) // BOMB
                {
                    var _crtlRNG = Random.Range(0, BombSpawnRate); // Further rng it
                    if (_crtlRNG == BombSpawnRate - 1)
                    {
                        _childScript.m_BlockType = (BlockType)_RNG; // Apply the bomb block
                    }
                }
                else
                {
                    _childScript.m_BlockType = (BlockType)_RNG; // Apply normal block
                }               
            }

            _childMat.mainTexture = List_BlockMat[1];

            var _AnchorScript = List_Anchors[(int)AnchorNumber].GetComponent<AnchorPoint>();
            _AnchorScript.mList_Blocks.Add(_child.gameObject);
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
    
    private void BuildStage()
    {
        Set_NextStageSize(List_StageSizesPrefab[0]); // Testing purposes

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
        for(int i=0;i<List_Anchors.Capacity-1;i++)
        {
            if(List_Anchors[i] == _closestAnchor)
            {
                _playerScript.m_PlayerAnchorPosition = (AnchorPointType)i;
                break;
            }
        }
    }
    // 000000000000000000000000000000000000000000
    //              PUBLIC METHOD
    // 000000000000000000000000000000000000000000

    // Set the level
    public void Set_DungeonSweeperLevel(int _levelType)
    {
        //Level = _levelType;
    }

    public void SpawnNext()
    {
        if((int)AnchorNumber == 4)
        {
            Set_NextAnchorNumber(AnchorPointType.ANCHOR_ONE);
        }
        else
        {
            Set_NextAnchorNumber(AnchorNumber + 1);
        }
       
        BuildStage();
    }
}