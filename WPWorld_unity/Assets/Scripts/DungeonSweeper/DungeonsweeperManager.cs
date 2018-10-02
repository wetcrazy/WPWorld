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

    [Tooltip("The list of main anchor point to spawn the stage")]
    public GameObject[] Arr_Anchors;
    [Tooltip("The list of main block textures")]
    public List<Texture> List_BlockMat;
    [Tooltip("The list of numbered textures")]
    public List<Texture> List_NumberBlockMat;
    [Tooltip("The list of Stage Sizes")]
    public List<GameObject> List_StageSizesPrefab;

    private AnchorPointType AnchorNumber;
    private LevelType Level;

    private void Awake()
    {
        Set_NextAnchorNumber(AnchorPointType.ANCHOR_ONE); // Start with the first       
        Set_NextStageSize(List_StageSizesPrefab[0]); // Testing purposes

        GridSetUp();
    }

    private void Update()
    {
        Triggered_Render();
    }

    // 0000000000000000000000000000000000000000000
    //              PRIVATE METHOD
    // 0000000000000000000000000000000000000000000

    // Sets the preloaded stage sizes from prefabs
    private void Set_NextStageSize(GameObject _prefab)
    {
        Instantiate(_prefab, Arr_Anchors[(int)AnchorNumber].transform.position, Quaternion.identity, Arr_Anchors[(int)AnchorNumber].transform);
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
        var _allchildblocks = Arr_Anchors[(int)AnchorNumber].GetComponentsInChildren<Transform>();

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
                _childScript.m_BlockType = (BlockType)_RNG;
            }

            _childMat.mainTexture = List_BlockMat[1];

            var _AnchorScript = Arr_Anchors[(int)AnchorNumber].GetComponent<AnchorPoint>();
            _AnchorScript.mList_Blocks.Add(_child.gameObject);
        }
    }

    // Renders when the player triggers it (Script activade from player)
    private void Triggered_Render()
    {
        var _AnchorScript = Arr_Anchors[(int)AnchorNumber].GetComponent<AnchorPoint>();
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

                _objMat.mainTexture = List_BlockMat[(int)_objScript.m_BlockType];
            }

        }
    }

    // 000000000000000000000000000000000000000000
    //              PUBLIC METHOD
    // 000000000000000000000000000000000000000000

    // Set the level
    public void Set_DungeonSweeperLevel(LevelType _levelType)
    {
        Level = _levelType;
    }

    

}