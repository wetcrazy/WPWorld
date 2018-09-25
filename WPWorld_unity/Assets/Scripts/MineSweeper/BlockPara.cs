using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPara : MonoBehaviour
{
    public BlockCounter.BlockType Block_Type;
    private bool isDead = false;

    private void Awake()
    {                  
        // Default this Object to BlockType Zero
        Block_Type = BlockCounter.BlockType.Zero;
    }
 

    // 000000000000000000000000000000000000000000
    //              PUBLIC METHODO
    // 000000000000000000000000000000000000000000

    public bool Get_isDead()
    {
        return isDead;
    }
    public void Set_isDead(bool _bool)
    {
        isDead = _bool;
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
