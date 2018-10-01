using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Block Parameters
/// </summary>
public class Blocks : MonoBehaviour
{
    public enum BlockType
    {
        EMPTY = 0,
        NORMAL,
        BOMB,
        TOTAL_BLOCKTYPE
    }

    [SerializeField]
    private bool isTriggered { get; set; }
    [SerializeField]
    private BlockType m_BlockType { get; set; }
    [SerializeField]
    private DungeonsweeperManager.BlockNumberType m_BlockNumber { get; set; }

    private void Awake()
    {
        isTriggered = false;
        m_BlockType = BlockType.EMPTY;
        m_BlockNumber = DungeonsweeperManager.BlockNumberType.EMPTY;
    }
 
}