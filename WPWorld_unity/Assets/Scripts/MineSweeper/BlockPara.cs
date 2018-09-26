using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPara : MonoBehaviour
{
    public BlockCounter.BlockType Block_Type;
    private bool isTriggered = false;

    private void Awake()
    {                  
        // Default this Object to BlockType Zero
        Block_Type = BlockCounter.BlockType.Zero;
    }
 

    // 000000000000000000000000000000000000000000
    //              PUBLIC METHODO
    // 000000000000000000000000000000000000000000

    public bool Get_isTriggered()
    {
        return isTriggered;
    }
    public void Set_isTriggered(bool _bool)
    {
        isTriggered = _bool;
    }  

    public BlockCounter.BlockType Get_BlockType()
    {
        return Block_Type;
    }
    public void Set_BlockType(BlockCounter.BlockType _blocktype)
    {
        Block_Type = _blocktype;
    }
}
