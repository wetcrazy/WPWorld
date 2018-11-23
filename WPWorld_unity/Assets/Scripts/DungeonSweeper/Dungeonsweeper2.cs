﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dungeonsweeper2 : MonoBehaviour
{
    public enum BlockType
    {
        EMPTY,
        NUMBERED,
        BOMB,
        TOTAL_BLOCKTYPE
    }

    public enum BlockNumberType
    {
        ZERO = 0,
        ONE,
        TWO,
        THREE,
        FOUR,
        FIVE,
        SIX,
        SEVEN,
        EIGHT,
        EMPTY,
        TOTAL_BLOCKNUMBERTYPE
    }

    public enum LevelType
    {
        EMPTY = 0,
        LEVEL_ONE,
        LEVEL_TWO,
        LEVEL_THREE,
        LEVEL_FOUR,
        TOTAL_LEVEL
    }

    [Tooltip("The list of main anchor point to spawn the stage")]
    public List<GameObject> List_Anchors;
    [Tooltip("The list of main block textures")]
    public List<Texture> List_BlockMat;
    [Tooltip("The list of numbered textures")]
    public List<Texture> List_NumberBlockMat;
    [Tooltip("The list of Stage Sizes")]
    public List<GameObject> List_GridSizesPrefab;
    [Tooltip("Current Level")]
    public LevelType Curr_Level;

    // Text Stuff
    public Text RC_Count, RC_Point;

    // Timer Stuff
    public Slider TimerBar;  
    public float currTimer;
    private const float TIMER = 60;

    // Conditions
    public bool is_lose = false;
    public bool isWin = false;

    // Reset Level Stuff
    private float waitTimer = 0.0f;
    private const float MAX_WAITTIMER = 5.0f;

    private void Awake()
    {
        currTimer = TIMER;
        TimerBar.maxValue = TIMER;
    }

    private void Update()
    {
        var _player = GameObject.FindGameObjectWithTag("Player");
        var _playerScript = _player.GetComponent<DSPlayer>();
        
        switch (Curr_Level)
        {
            case LevelType.LEVEL_ONE:
                // 1st anchor
                if(!List_Anchors[0].GetComponent<AnchorPoint>().m_isGridApplied)
                {
                    List_Anchors[0].GetComponent<AnchorPoint>().m_isGridApplied = true;
                    GridSetup(List_GridSizesPrefab[0], 0, 5, 6);
                    AnchorMovement(0);
                }
                // Win conidition
                if(List_Anchors[0].GetComponent<AnchorPoint>().m_isdone)
                {
                    isWin = true;
                }
                break;
            case LevelType.LEVEL_TWO:
                // 1st anchor
                if (!List_Anchors[0].GetComponent<AnchorPoint>().m_isGridApplied)
                {
                    List_Anchors[0].GetComponent<AnchorPoint>().m_isGridApplied = true;
                    GridSetup(List_GridSizesPrefab[1], 0, 5, 6);
                    AnchorMovement(0);
                }
                // 2nd anchor
                if (!List_Anchors[1].GetComponent<AnchorPoint>().m_isGridApplied && List_Anchors[0].GetComponent<AnchorPoint>().m_isdone) // if the previous one is done then build this up
                {
                    List_Anchors[1].GetComponent<AnchorPoint>().m_isGridApplied = true;
                    GridSetup(List_GridSizesPrefab[0], 1, 10, 6);
                    AnchorMovement(1);
                }
                // Win conidition
                if (List_Anchors[1].GetComponent<AnchorPoint>().m_isdone)
                {
                    isWin = true;
                }
                break;
            case LevelType.LEVEL_THREE:
                // 1st anchor
                if (!List_Anchors[0].GetComponent<AnchorPoint>().m_isGridApplied)
                {
                    List_Anchors[0].GetComponent<AnchorPoint>().m_isGridApplied = true;
                    GridSetup(List_GridSizesPrefab[0], 0, 5, 6);
                    AnchorMovement(0);
                }
                // 2nd anchor
                if (!List_Anchors[1].GetComponent<AnchorPoint>().m_isGridApplied && List_Anchors[0].GetComponent<AnchorPoint>().m_isdone) // if the previous one is done then build this up
                {
                    List_Anchors[1].GetComponent<AnchorPoint>().m_isGridApplied = true;
                    GridSetup(List_GridSizesPrefab[1], 1, 10, 6);
                    AnchorMovement(1);
                }
                if (!List_Anchors[2].GetComponent<AnchorPoint>().m_isGridApplied && List_Anchors[1].GetComponent<AnchorPoint>().m_isdone) // if the previous one is done then build this up
                {
                    List_Anchors[2].GetComponent<AnchorPoint>().m_isGridApplied = true;
                    GridSetup(List_GridSizesPrefab[2], 2, 15, 6);
                    AnchorMovement(2);
                }
                // Win conidition
                if (List_Anchors[2].GetComponent<AnchorPoint>().m_isdone)
                {
                    isWin = true;
                }
                break;
            case LevelType.LEVEL_FOUR:
                // 1st anchor
                if (!List_Anchors[0].GetComponent<AnchorPoint>().m_isGridApplied)
                {
                    List_Anchors[0].GetComponent<AnchorPoint>().m_isGridApplied = true;
                    GridSetup(List_GridSizesPrefab[0], 0, 5, 6);
                    AnchorMovement(0);
                }
                // 2nd anchor
                if (!List_Anchors[1].GetComponent<AnchorPoint>().m_isGridApplied && List_Anchors[0].GetComponent<AnchorPoint>().m_isdone) // if the previous one is done then build this up
                {
                    List_Anchors[1].GetComponent<AnchorPoint>().m_isGridApplied = true;
                    GridSetup(List_GridSizesPrefab[1], 1, 10, 6);
                    AnchorMovement(1);
                }
                if (!List_Anchors[2].GetComponent<AnchorPoint>().m_isGridApplied && List_Anchors[1].GetComponent<AnchorPoint>().m_isdone) // if the previous one is done then build this up
                {
                    List_Anchors[2].GetComponent<AnchorPoint>().m_isGridApplied = true;
                    GridSetup(List_GridSizesPrefab[2], 2, 15, 6);
                    AnchorMovement(2);
                }
                if (!List_Anchors[3].GetComponent<AnchorPoint>().m_isGridApplied && List_Anchors[2].GetComponent<AnchorPoint>().m_isdone) // if the previous one is done then build this up
                {
                    List_Anchors[3].GetComponent<AnchorPoint>().m_isGridApplied = true;
                    GridSetup(List_GridSizesPrefab[2], 3, 20, 6);
                    AnchorMovement(3);
                }
                // Win conidition
                if (List_Anchors[3].GetComponent<AnchorPoint>().m_isdone)
                {
                    isWin = true;
                }
                break;
        }


        AnchorMovement();
        Triggered_Material(_player.transform);                
     
        // Closest anchor to the player
        GameObject _closestobj = null;
        for (int i = 0; i < List_Anchors.Count - 1; i++)
        {
            if (i == 0)
            {
                _closestobj = List_Anchors[i];
                continue;
            }

            if (Vector3.Distance(_player.transform.position, _closestobj.transform.position) > Vector3.Distance(_player.transform.position, List_Anchors[i].transform.position))
            {
                _closestobj = List_Anchors[i];
            }
        }
     
        var _anchor = List_Anchors[List_Anchors.IndexOf(_closestobj)];

        // TEXT STUFF
        RC_Point.text = "Empty Spaces Left";     

        int _count = 0;
        var _anchorScript = _anchor.GetComponent<AnchorPoint>();
        foreach (GameObject _block in _anchorScript.mList_Blocks)
        {
            var _blockScript = _block.GetComponent<Blocks>();
            if (!_blockScript.m_isTriggered)
            {
                if (_blockScript.m_BlockType == BlockType.NUMBERED)
                {
                    _count += 1;                
                }             
            }
            else
            {
                // BOMB LOSE CONDITION
                if (_blockScript.m_BlockType == BlockType.BOMB)
                {
                    is_lose = true;
                }               
            }
        }
        RC_Count.text = _count.ToString();

        // Check if the grid has been cleared
        if(_count == 0 && _anchor.GetComponent<AnchorPoint>().m_isTypeApplied)
        {
            _anchor.GetComponent<AnchorPoint>().m_isdone = true;
        }

        // Lose condition
        if (is_lose)
        {         
            if(waitTimer >= MAX_WAITTIMER)
            {
                Reset_Level();
            }
            else
            {
                waitTimer += 1.0f * Time.deltaTime;
            }
        }
        else
        {
            // Timer UI    
            TimerBar.value = currTimer;
            if (currTimer <= 0)
            {
                is_lose = true;
            }
            else
            {
                currTimer -= 0.1f;
            }
        }
       
    }

    // Spawns the grid and save the data
    private void GridSetup(GameObject _gridPrefab, int _anchorIndex, int _maxBomb,int _BombSpawnRate)
    {
        // Spawn Grid
        Instantiate(_gridPrefab, List_Anchors[_anchorIndex].transform.position, Quaternion.identity, List_Anchors[_anchorIndex].transform);

        // Find the blocks in the main anchor
        var _arrChildren = List_Anchors[_anchorIndex].GetComponentsInChildren<Transform>();

        // Anchor Script
        var _anchorScript = List_Anchors[_anchorIndex].GetComponent<AnchorPoint>();

        foreach (Transform _child in _arrChildren)
        {
            if (_child.gameObject.tag != "Blocks")
            {
                continue;
            }

            // Update the variables
            _anchorScript.mList_Blocks.Add(_child.gameObject);       

            // Material
            _child.GetComponent<Renderer>().material.mainTexture = List_BlockMat[1];
        }

        _anchorScript.m_numBomb = _maxBomb;
        _anchorScript.m_BombSpawnRate = _BombSpawnRate;
        _anchorScript.m_GridName = _gridPrefab.name;
        _anchorScript.m_numNormal = _anchorScript.mList_Blocks.Count;
    }

    // Material any block that has been triggered
    private void Triggered_Material(Transform _playerposition)
    {
        // Runs the material code where the player is closest to
        GameObject _closestobj = null;
        for (int i = 0; i < List_Anchors.Count - 1; i++)
        {
            if (i == 0)
            {
                _closestobj = List_Anchors[i];
                continue;
            }

            if (Vector3.Distance(_playerposition.position, _closestobj.transform.position) > Vector3.Distance(_playerposition.position, List_Anchors[i].transform.position))
            {
                _closestobj = List_Anchors[i];
            }
        }

        var _AnchorScript = List_Anchors[List_Anchors.IndexOf(_closestobj)].GetComponent<AnchorPoint>();     

        // Minesweeper logic    
        foreach (GameObject _block in _AnchorScript.mList_Blocks)
        {         
            // Change material
            var _blockScript = _block.GetComponent<Blocks>();
            var _blockMat = _block.GetComponent<Renderer>().material;

            if (_blockScript.m_isTriggered)
            {
                if (!_AnchorScript.m_isTypeApplied)// First step
                {
                    FirstStep(List_Anchors.IndexOf(_closestobj), _block);
                }

                if (_blockScript.m_BlockType == BlockType.NUMBERED)
                {
                    Numbered_Material(_block);
                    continue;
                }

                _blockMat.mainTexture = List_BlockMat[(int)_blockScript.m_BlockType];
            }
        }
    }

    // Minesweeper logic
    private void Numbered_Material(GameObject _block)
    {
        // If its not zero just return
        var _blockScript = _block.GetComponent<Blocks>();
        var _blockMat = _block.GetComponent<Renderer>().material;
        _blockMat.mainTexture = List_NumberBlockMat[(int)_blockScript.m_BlockNumberType];

        if (_blockScript.m_BlockNumberType != BlockNumberType.ZERO)
        {
            return;
        }

        // A list of rays being casted
        List<RaycastHit> _listRays = new List<RaycastHit>();

        // Check the surroundings of the blocks except down and up
        RaycastHit _ray;
        if (Physics.Raycast(_block.transform.position, gameObject.transform.forward, out _ray, _block.transform.localScale.x)) // Forward
        {
            _listRays.Add(_ray);
        }
        if (Physics.Raycast(_block.transform.position, -gameObject.transform.forward, out _ray, _block.transform.localScale.x)) // Backward
        {
            _listRays.Add(_ray);
        }
        if (Physics.Raycast(_block.transform.position, gameObject.transform.right, out _ray, _block.transform.localScale.x)) // Right
        {
            _listRays.Add(_ray);
        }
        if (Physics.Raycast(_block.transform.position, -gameObject.transform.right, out _ray, _block.transform.localScale.x)) // Left
        {
            _listRays.Add(_ray);
        }
        if (Physics.Raycast(_block.transform.position, gameObject.transform.up, out _ray, _block.transform.localScale.x)) // UP
        {
            _listRays.Add(_ray);
        }
        if (Physics.Raycast(_block.transform.position, -gameObject.transform.up, out _ray, _block.transform.localScale.x)) // down
        {
            _listRays.Add(_ray);
        }

        foreach (RaycastHit _hit in _listRays)
        {
            if (_hit.transform.gameObject.tag != "Blocks")
            {
                continue;
            }

            var _hitScript = _hit.transform.gameObject.GetComponent<Blocks>();

            if (_hitScript.m_BlockType == BlockType.NUMBERED)
            {
                _hitScript.m_isTriggered = true;              
            }
        }
    }

    // Place all the types of blocks into the blocks
    private void FirstStep(int _index,GameObject _firstBlock)
    {
        var _anchorScript = List_Anchors[_index].GetComponent<AnchorPoint>();

        var _firstBlockScript = _firstBlock.GetComponent<Blocks>();
        _firstBlockScript.m_BlockType = BlockType.NUMBERED;
               
        _anchorScript.m_isTypeApplied = true;
        int currBomb = 0;

        foreach (GameObject _child in _anchorScript.mList_Blocks)
        {
            var _childScript = _child.GetComponent<Blocks>();

            while (_childScript.m_BlockType == BlockType.EMPTY)
            {
                var _RNG = Random.Range(1, (int)BlockType.TOTAL_BLOCKTYPE);
                if ((BlockType)_RNG == BlockType.BOMB) // BOMB
                {                  
                    if (currBomb > _anchorScript.m_numBomb) // Bomb Limiter
                    {
                        continue;
                    }

                    var _crtlRNG = Random.Range(0, _anchorScript.m_BombSpawnRate); // Further rng it
                    if (_crtlRNG == _anchorScript.m_BombSpawnRate - 1)
                    {                      
                        _childScript.m_BlockType = (BlockType)_RNG; // Apply the bomb block                       
                    }

                }
                else
                {
                    _childScript.m_BlockType = (BlockType)_RNG; // Apply normal block
                }                        
            }
             
        }

        // Runs after all bombs have been placed
        Numbering(_anchorScript);
    }

    // Activades after the bombs have been placed
    private void Numbering(AnchorPoint _anchorScript)
    {
        foreach (GameObject _child in _anchorScript.mList_Blocks)
        {
            var _childScript = _child.GetComponent<Blocks>();

            if (_childScript.m_BlockType != BlockType.NUMBERED)
            {
                continue;
            }

            Collider[] _arrCol = Physics.OverlapSphere(_child.transform.position, _child.transform.localScale.x / 10);
            int _bombCount = 0;

            foreach (Collider _col in _arrCol)
            {
                if(_col.transform.gameObject.tag != "Blocks")
                {
                    continue;
                }

                var _colScript = _col.gameObject.GetComponent<Blocks>();

                if (_colScript.m_BlockType == BlockType.BOMB)
                {
                    _bombCount += 1;
                }
            }

            _childScript.m_BlockNumberType = (BlockNumberType)_bombCount;
        }
    }

    // Checks the grid ended
    private void Check_GridEnded(Transform _playerposition)
    {
        // Runs the code where the player is closest to
        GameObject _closestobj = null;
        for (int i = 0; i < List_Anchors.Count - 1; i++)
        {
            if (i == 0)
            {
                _closestobj = List_Anchors[i];
                continue;
            }

            if (Vector3.Distance(_playerposition.position, _closestobj.transform.position) > Vector3.Distance(_playerposition.position, List_Anchors[i].transform.position))
            {
                _closestobj = List_Anchors[i];
            }
        }

        var _AnchorScript = List_Anchors[List_Anchors.IndexOf(_closestobj)].GetComponent<AnchorPoint>();

        // Check the win and lose condition
        int _triggedblocks = 0;
        foreach (GameObject _block in _AnchorScript.mList_Blocks)
        {
            var _blockScript = _block.GetComponent<Blocks>();

            if(_blockScript.m_isTriggered)
            {
                if(_blockScript.m_BlockType == BlockType.NUMBERED)
                {
                    _triggedblocks++;
                }
            }
        }

        
        if(_triggedblocks == _AnchorScript.m_numNormal)
        {
            _AnchorScript.m_isdone = true;
        }

    }

    // Moves the anchor to the centre of the playing field
    private void AnchorMovement(int _index)
    {
        var _anchorScript = List_Anchors[_index].GetComponent<AnchorPoint>();       
        List_Anchors[_index].transform.localPosition = transform.localPosition;
    }
    // Updates and reverts back any anchor that has their grid cleared
    private void AnchorMovement()
    {
        foreach (GameObject _anchor in List_Anchors)
        {
            var _anchorScript = _anchor.GetComponent<AnchorPoint>();
            if (_anchorScript.m_isdone)
            {
                var _children = _anchor.GetComponentsInChildren<Transform>();
                foreach (Transform _child in _children)
                {
                    if (_child.gameObject.tag != "Anchor")
                    {
                        var _randSpeed = Random.Range(0.01f, 0.1f);
                        _child.Translate(Vector3.down * _randSpeed);
                        Destroy(_child.gameObject, 1);
                    }
                }
                _anchorScript.mList_Blocks.Clear();
            }
            if (_anchor.GetComponentsInChildren<Transform>().Length <= 1)
            {
                _anchor.transform.localPosition = _anchorScript.m_OldPosition;
            }
        }
    }

    // =================================
    // Public methods
    // =================================

    public void Level_Select(LevelType _level)
    {
        Curr_Level = _level;
    }

    // Gets the anchor the player is standing on 
    public Vector3 Get_Player_AnchorPosition(Transform _playerposition)
    {     
        GameObject _closestobj = null;
        for (int i = 0; i < List_Anchors.Count - 1; i++)
        {
            if (i == 0)
            {
                _closestobj = List_Anchors[i];
                continue;
            }

            if (Vector3.Distance(_playerposition.position, _closestobj.transform.position) > Vector3.Distance(_playerposition.position, List_Anchors[i].transform.position))
            {
                _closestobj = List_Anchors[i];
            }
        }

        var _pos = List_Anchors[List_Anchors.IndexOf(_closestobj)].transform.position;

        return _pos;
    }

    // Reset variables 
    public void Reset_Level()
    {
        var _player = GameObject.FindGameObjectWithTag("Player");
        var _playerRB = _player.GetComponent<Rigidbody>();

        // Delete all blocks
        foreach (GameObject _anchor in List_Anchors)
        {
            var _anchorScript = _anchor.GetComponent<AnchorPoint>();
            if (_anchor.transform.childCount == 1)
            {
                DestroyImmediate(_anchor.transform.GetChild(0).gameObject);
            }

            _anchorScript.mList_Blocks.Clear();
            _anchorScript.Reset_Variables();
        }

        // Player position
        var _pos = Get_Player_AnchorPosition(_player.transform);
        _pos.y = 0.5f;
        _playerRB.MovePosition(_pos + transform.forward * Time.deltaTime);

        // Wait timer

        waitTimer = 0.0f;

        // Ui Timer slider
        is_lose = false;
        currTimer = TIMER;
    }

    // Check lose Condition
    public void CheckIsLose(bool _isWon)
    {
        _isWon = isWin;
    }

    // Check win condition
    public void CheckIsWon(ref bool _isWon)
    {
        _isWon = isWin;
    }
}
