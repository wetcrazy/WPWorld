using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public GameObject PlatformManager;

    private void Awake()
    {
        GridSetup(List_GridSizesPrefab[1], 0, 0);
    }

    private void Update()
    {
        var _player = GameObject.FindGameObjectWithTag("Player");
        var _playerScript = _player.GetComponent<DSPlayer>();

        Triggered_Material(_player.transform);
    }

    // Spawns the grid and save the data
    private void GridSetup(GameObject _gridPrefab, int _anchorIndex, int _maxBomb)
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
            _anchorScript.m_numBomb = _maxBomb;

            // Material
            _child.GetComponent<Renderer>().material.mainTexture = List_BlockMat[1];
        }
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

        foreach (GameObject _block in _AnchorScript.mList_Blocks)
        {
            var _blockScript = _block.GetComponent<Blocks>();
            var _blockMat = _block.GetComponent<Renderer>().material;

            if (_blockScript.m_isTriggered)
            {
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
                if (_hitScript.m_BlockNumberType == BlockNumberType.ZERO)
                {
                    Numbered_Material(_hit.transform.gameObject);
                }
            }
        }
    }
}
