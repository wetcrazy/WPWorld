using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCounter : MonoBehaviour
{
    public enum BlockType
    {
        Zero = 0,
        Normal,
        Bomb,
        Total_BlockType
    };

    public int BlockCount;

    /// <summary>
    /// Need to be in squence with Block Para ENUM
    /// </summary>
    public Material[] Arr_Mat;

    private GameObject[] Arr_Blocks;
    public bool isReset = true;

    private void Awake()
    {
        CheckBlocks();
    }

    private void Update()
    {
        if (!isReset)
        {
            return;
        }

        // Block Setting
        for (int i = 0; i < Arr_Blocks.Length; i++)
        {
            // Block type setter
            var _ScriptComponent = Arr_Blocks[i].GetComponent<BlockPara>();
            if(_ScriptComponent.Get_BlockType() == BlockType.Zero) 
            {
                var _typeRNG = Random.Range(1, (int)BlockType.Total_BlockType);
                _ScriptComponent.Set_BlockType((BlockType)_typeRNG);
            }

            // Block Material setter
            //var _MatComponent = Arr_Blocks[i].GetComponent<Material>();
            //_MatComponent = Find_material(_ScriptComponent.Get_BlockType());
        }
    }

    // 000000000000000000000000000000000000000000
    //             PRIVATE METHODO
    // 000000000000000000000000000000000000000000

    private void CheckBlocks()
    {
        Arr_Blocks = GameObject.FindGameObjectsWithTag("Blocks");
        BlockCount = Arr_Blocks.Length;
    }

    private Material Find_material(BlockType _type)
    {
        for (int i = 0; i < Arr_Mat.Length; i++)
        {
            if (Arr_Mat[i].name == _type.ToString())
            {
                return Arr_Mat[i];
            }
        }
        return null;
    }
}
