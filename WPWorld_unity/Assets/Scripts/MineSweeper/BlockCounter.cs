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
    [Range(0,10)]
    public int BombSpawnRate;

    private void Awake()
    {
        CheckBlocks();
        BlockSetup();
    }

    private void Update()
    {
        /*
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
        */
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

    // Render the material Super dumb way of doing it
    private void RenderMaterial(GameObject _gameObj)
    {
        List<RaycastHit> _listRays = new List<RaycastHit>();

        RaycastHit _ray;
        if (Physics.Raycast(_gameObj.transform.position, gameObject.transform.forward, out _ray))
        {
            _listRays.Add(_ray);
        }
        if (Physics.Raycast(_gameObj.transform.position, -gameObject.transform.forward, out _ray))
        {
            _listRays.Add(_ray);
        }
        if (Physics.Raycast(_gameObj.transform.position, gameObject.transform.right, out _ray))
        {
            _listRays.Add(_ray);
        }
        if (Physics.Raycast(_gameObj.transform.position, -gameObject.transform.right, out _ray))
        {
            _listRays.Add(_ray);
        }
        
        foreach(RaycastHit _hit in _listRays)
        {
            var _tempScript = _hit.transform.gameObject.GetComponent<BlockPara>();
            var _tempMat = _hit.transform.gameObject.GetComponent<Renderer>().material;

            if(_tempScript.Get_isTriggered())
            {
                continue;
            }
            _tempScript.Set_isTriggered(true);

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
            _tempMat.mainTexture = Find_Normalmaterial((int)_tempScript.Get_NormalType());

            if (_tempScript.Get_NormalType() == NormalType.Zero)
            {
                RenderMaterial(_gameObj);
            }
            /*
            //  If they are not numberised
            if (_tempScript.Get_NormalType() == NormalType.Empty)
            {
                // Mine sweeper logic 

                
                Collider[] _col = Physics.OverlapSphere(_gameObj.transform.position, transform.localScale.x / 10);
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
                        else if (_blockPara.Get_NormalType() == NormalType.Empty)
                        {                            
                            WhenTriggered(_col[j].gameObject);                          
                        }
                    }
                }
              
                // Replace the textures to the amount of bombs found around it
                _tempScript.Set_NormalType((NormalType)_bombCount);
                _tempMat.mainTexture = Find_Normalmaterial((int)_tempScript.Get_NormalType());                      
            }
            */
        }
        else
        {
            // Switch to their textures
            _tempMat.mainTexture = Find_material((int)_tempScript.Get_BlockType());
        }
    }
}