using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonsweeperManager : MonoBehaviour
{
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

    public enum MainAnchorType
    {
        EMPTY = 0,
        ANCHOR_ONE = 1,
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
    public GameObject[] Arr_Centres;
    [Tooltip("The list of main block textures")]
    public List<Material> List_BlockMat;
    [Tooltip("The list of numbered textures")]
    public List<Material> List_NumberBlockMat;
  
    private MainAnchorType AnchorNumber;
    private LevelType Level;

    private void Awake()
    {
        AnchorNumber = MainAnchorType.ANCHOR_ONE; // Start with the first
    }

    // 0000000000000000000000000000000000000000000
    //              PRIVATE METHOD
    // 0000000000000000000000000000000000000000000

    // Sets the preloaded stage sizes from reasources
    private void Set_StageSize(string _sizeName)
    {
        var _stage = Resources.Load<GameObject>("Minesweeper/" + _sizeName);
        Instantiate(_stage, Arr_Centres[(int)AnchorNumber].transform.position, Quaternion.identity, Arr_Centres[(int)AnchorNumber].transform);
    }

    private void Set_AnchorPoint(MainAnchorType _anchor)
    {
        
    }


    // 000000000000000000000000000000000000000000
    //              PUBLIC METHOD
    // 000000000000000000000000000000000000000000

    // Block Material
    public List<Material> Get_BlockMat()
    {
        return List_BlockMat;
    }

    // Number Material
    public List<Material> Get_NumberBlockMat()
    {
        return List_NumberBlockMat;
    }

    // Set the level
    public void Set_Level(LevelType _levelType)
    {
        Level = _levelType;
    }
}