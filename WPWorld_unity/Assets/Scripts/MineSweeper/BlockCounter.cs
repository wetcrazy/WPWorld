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
        // Only resets the materials on call
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

            // Block Material.maintexture setter
            var _MatComponent = Arr_Blocks[i].GetComponent<Renderer>().material;
            // On triggered
            if (_ScriptComponent.Get_isDead())
            {            
                // Switch to their textures
                _MatComponent.mainTexture = Find_material((int)_ScriptComponent.Get_BlockType());
                var _CollisionComponent = Arr_Blocks[i].GetComponent<Collider>();
                _CollisionComponent.enabled = false;
            }
            else
            {
                // Default to bricks
                _MatComponent.mainTexture = Find_material(1); // Hardcoded
            }
                
        }
            
        isReset = false;
    }

    // 000000000000000000000000000000000000000000
    //             PRIVATE METHODO
    // 000000000000000000000000000000000000000000

    private void CheckBlocks()
    {
        Arr_Blocks = GameObject.FindGameObjectsWithTag("Blocks");
        BlockCount = Arr_Blocks.Length;
    }

    private Texture Find_material(int _num)
    {
        for (int i = 0; i < Arr_Mat.Length; i++)
        {
            if (i == _num)
            {
                return Arr_Mat[i].mainTexture;
            }
        }
        return null;
    }
}
