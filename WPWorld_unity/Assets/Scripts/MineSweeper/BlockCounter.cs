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
    public enum NormalType
    {
        Zero = 0,
        One,
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Empty,
        Total_NormalType
    }

    public int BlockCount;

    /// <summary>
    /// Need to be in squence with BlockType ENUM
    /// </summary>
    public Material[] Arr_Mat;
    /// <summary>
    /// Need to be in squence with NormalType ENUM
    /// </summary>
    public Material[] Arr_NormalMat;

    private GameObject[] Arr_Blocks;
    public bool isReset = true;
    /// <summary>
    /// Higher the number, the lesser chance for the bomb to spawn
    /// </summary>
    public int BombSpawnRate;

    private void Awake()
    {
        CheckBlocks();
    }

    private void Update()
    {
        // Block Set up
        for (int i = 0; i < Arr_Blocks.Length; i++)
        {
            // Block parameter 
            var _ScriptComponent = Arr_Blocks[i].GetComponent<BlockPara>();

            // Setup the playing field
            while (_ScriptComponent.Get_BlockType() == BlockType.Zero)
            {
                var _typeRNG = Random.Range(1, (int)BlockType.Total_BlockType);
                if ((BlockType)_typeRNG == BlockType.Bomb)
                {
                    var _crtlRNG = Random.Range(0, BombSpawnRate); // Further rng it
                    if (_crtlRNG == BombSpawnRate - 1) // apply the bomb
                    {
                        _ScriptComponent.Set_BlockType((BlockType)_typeRNG);
                    }
                }
                else
                {
                    _ScriptComponent.Set_BlockType((BlockType)_typeRNG);
                }             
            }

            // Block Material.maintexture setter
            var _MatComponent = Arr_Blocks[i].GetComponent<Renderer>().material;

            //_ScriptComponent.Set_isTriggered(true);

            // On triggered
            if (_ScriptComponent.Get_isTriggered())
            {
                // If they are normal blocks
                if (_ScriptComponent.Get_BlockType() == BlockType.Normal)
                {
                    //  If they are not numberised
                    if (_ScriptComponent.Get_NormalType() == NormalType.Empty)
                    {
                        // Mine sweeper logic 
                        Collider[] _col = Physics.OverlapSphere(Arr_Blocks[i].transform.position, transform.localScale.x / 10);
                        int _bombCount = 0;

                        // Check for surrounding bombs
                        for (int j = 0; j < _col.Length; j++)
                        {
                            // Dont check player
                            if (_col[j].gameObject.tag == "Blocks")
                            {
                                // Check for bombs
                                var _blockPara = _col[j].gameObject.GetComponent<BlockPara>();

                                if (_blockPara.Get_BlockType() == BlockType.Bomb)
                                {
                                    _bombCount++;
                                }
                            }
                        }

                        // Replace the textures to the amount of bombs found around it
                        _ScriptComponent.Set_NormalType((NormalType)_bombCount);
                        _MatComponent.mainTexture = Find_Normalmaterial((int)_ScriptComponent.Get_NormalType());
                    }
                }
                else
                {
                    // Switch to their textures
                    _MatComponent.mainTexture = Find_material((int)_ScriptComponent.Get_BlockType());
                }
            }
            else if (_MatComponent.mainTexture != Find_material(0)) // Set into blank blocks
            {
                //Default to bricks
                _MatComponent.mainTexture = Find_material(0); // Hardcoded
            }
        }
    }

    // 000000000000000000000000000000000000000000
    //             PRIVATE METHODO
    // 000000000000000000000000000000000000000000

    /// <summary>
    /// Checks all the blocks in the scene
    /// </summary>
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

    private Texture Find_Normalmaterial(int _num)
    {
        for (int i = 0; i < Arr_NormalMat.Length; i++)
        {
            if (i == _num)
            {
                return Arr_NormalMat[i].mainTexture;
            }
        }
        return null;
    }
}