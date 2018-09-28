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
    /// <summary>
    /// All the blocks in the scene
    /// </summary>
    private GameObject[] Arr_Blocks;
    public bool isReset = true;
    /// <summary>
    /// Higher the number, the lesser chance for the bomb to spawn
    /// </summary>
    [Range(0,10)]
    public int BombSpawnRate;

    private void Awake()
    {
        CheckBlocks();
        BlockSetup();

        for (int i = 0; i < Arr_Blocks.Length; i++)
        {
            CheckBombs(Arr_Blocks[i]);
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

    private void BlockSetup()
    {
        // Loop all the found blocks in the scene
        for (int i = 0; i < Arr_Blocks.Length; i++)
        {
            // Block parameter 
            var _ScriptComponent = Arr_Blocks[i].GetComponent<BlockPara>();
            // Block Material.maintexture setter
            var _MatComponent = Arr_Blocks[i].GetComponent<Renderer>().material;

            // Setup the playing field
            while (_ScriptComponent.Get_BlockType() == BlockType.Zero)
            {
                var _typeRNG = Random.Range(1, (int)BlockType.Total_BlockType);
                if ((BlockType)_typeRNG == BlockType.Bomb)
                {
                    var _crtlRNG = Random.Range(0, BombSpawnRate); // Further rng it
                    if (_crtlRNG == BombSpawnRate - 1) 
                    {
                        _ScriptComponent.Set_BlockType((BlockType)_typeRNG); // Apply the bomb block
                    }
                }
                else
                {
                    _ScriptComponent.Set_BlockType((BlockType)_typeRNG); // Apply normal block
                }
            }

            //Default to bricks
            _MatComponent.mainTexture = Find_material(0); // Hardcoded
        }
    }

    // Preload the answers
    private void CheckBombs(GameObject _gameObj)
    {
        Collider[] _col = Physics.OverlapSphere(_gameObj.transform.position, transform.localScale.x / 10);
        var _tempScript = _gameObj.GetComponent<BlockPara>();     
        int _bombCount = 0;

        // Check for surrounding bombs
        for (int j = 0; j < _col.Length; j++)
        {
            // Dont check player
            if (_col[j].gameObject.tag == "Blocks")
            {
                // Check for bombs
                var _tempScript2 = _col[j].gameObject.GetComponent<BlockPara>();

                if (_tempScript2.Get_BlockType() == BlockType.Bomb)
                {
                    _bombCount++;
                }              
            }
        }

        // Replace the textures to the amount of bombs found around it
        _tempScript.Set_NormalType((NormalType)_bombCount);
    }

    // Render the material Super dumb way of doing it (Recursive Function)
    private void RenderMaterial(GameObject _gameObj)
    {
        // A list of rays being casted
        List<RaycastHit> _listRays = new List<RaycastHit>();

        // Check the surroundings of the blocks except down and up
        RaycastHit _ray;
        if (Physics.Raycast(_gameObj.transform.position, gameObject.transform.forward, out _ray)) // Forward
        {
            _listRays.Add(_ray);
        }
        if (Physics.Raycast(_gameObj.transform.position, -gameObject.transform.forward, out _ray)) // Backward
        {
            _listRays.Add(_ray);
        }
        if (Physics.Raycast(_gameObj.transform.position, gameObject.transform.right, out _ray)) // Right
        {
            _listRays.Add(_ray);
        }
        if (Physics.Raycast(_gameObj.transform.position, -gameObject.transform.right, out _ray)) // Left
        {
            _listRays.Add(_ray);
        }
        
        // Loop through all directions from above to open up the block's material
        foreach(RaycastHit _hit in _listRays)
        {
            var _tempScript = _hit.transform.gameObject.GetComponent<BlockPara>();
                    
            // Safety check to avoid setting the material again
            if(_tempScript.Get_isTriggered())
            {
                continue;
            }

            var _tempMat = _hit.transform.gameObject.GetComponent<Renderer>().material;
            _tempScript.Set_isTriggered(true);

            // Recursive Attivade here only when detected more zero blocks
            if (_tempScript.Get_NormalType() == NormalType.Zero )
            { 
                _tempMat.mainTexture = Find_Normalmaterial((int)_tempScript.Get_NormalType());
                RenderMaterial(_hit.transform.gameObject);
            }
            else
            {
                _tempMat.mainTexture = Find_Normalmaterial((int)_tempScript.Get_NormalType());
            }
        }
    }


    // 000000000000000000000000000000000000000000
    //            PUBLIC METHODO
    // 000000000000000000000000000000000000000000

    public void WhenTriggered(GameObject _gameObj)
    {
        var _tempScript = _gameObj.GetComponent<BlockPara>();
        var _tempMat = _gameObj.GetComponent<Renderer>().material;

        if (_tempScript.Get_isTriggered())
        {           
            return;
        }

        //_tempScript.Set_isTriggered(true);       

        // If they are normal blocks
        if (_tempScript.Get_BlockType() == BlockType.Normal)
        {
            // Set their own texture first
            _tempMat.mainTexture = Find_Normalmaterial((int)_tempScript.Get_NormalType());

            // Start Opening up all the blank blocks
            if (_tempScript.Get_NormalType() == NormalType.Zero)
            {
                RenderMaterial(_gameObj); // Function to load the actual materials
            }           
        }
        else
        {
            // Switch to their textures
            _tempMat.mainTexture = Find_material((int)_tempScript.Get_BlockType());
        }
    }
}