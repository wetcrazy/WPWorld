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
    private Dungeonsweeper2.BlockType blockType;
    [SerializeField]
    private Dungeonsweeper2.BlockNumberType blockNumber;

    private void Awake()
    {
        isTriggered = false;
        blockType = Dungeonsweeper2.BlockType.EMPTY;
        blockNumber = Dungeonsweeper2.BlockNumberType.EMPTY;
    }

    // 000000000000000000000000000000000000000000
    //              PUBLIC METHOD
    // 000000000000000000000000000000000000000000

    public bool m_isTriggered
    {
        set { isTriggered = value; }
        get { return isTriggered; }
    }
   
    public Dungeonsweeper2.BlockType m_BlockType
    {
        set { blockType = value; }
        get { return blockType; }
    }
   
    public Dungeonsweeper2.BlockNumberType m_BlockNumberType
    {
        set { blockNumber = value; }
        get { return blockNumber; }
    }
   
}