using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Block Parameters
/// </summary>
public class Blocks : MonoBehaviour
{ 
    [SerializeField]
    private bool isTriggered;
    [SerializeField]
    private DungeonsweeperManager.BlockType blockType;
    [SerializeField]
    private DungeonsweeperManager.BlockNumberType blockNumber;

    private void Awake()
    {
        isTriggered = false;
        blockType = DungeonsweeperManager.BlockType.EMPTY;
        blockNumber = DungeonsweeperManager.BlockNumberType.EMPTY;
    }

    // 000000000000000000000000000000000000000000
    //              PUBLIC METHOD
    // 000000000000000000000000000000000000000000

    public bool m_isTriggered
    {
        set { isTriggered = value; }
        get { return isTriggered; }
    }
   
    public DungeonsweeperManager.BlockType m_BlockType
    {
        set { blockType = value; }
        get { return blockType; }
    }
   
    public DungeonsweeperManager.BlockNumberType m_BlockNumberType
    {
        set { blockNumber = value; }
        get { return blockNumber; }
    }
   
}