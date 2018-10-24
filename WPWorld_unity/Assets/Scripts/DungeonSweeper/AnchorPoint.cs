using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchorPoint : MonoBehaviour
{
    [SerializeField]
    private bool is_Done = false;
    [SerializeField]
    private bool is_GridApplied = false;
    [SerializeField]
    private bool is_TypeApplied = false;
    [SerializeField]
    private bool is_PlatformSpawn = false;
    [SerializeField]
    private int numBomb;
    [SerializeField]
    private int BombSpawnRate;
    [SerializeField]
    private int numNormal;
    [SerializeField]
    private string GridName;
    [SerializeField]
    private Vector3 OldPosition;
    [SerializeField]
    private List<GameObject> List_Blocks;

    private void Awake()
    {
        OldPosition = transform.localPosition;
    }

    public bool m_isdone
    {
        set { is_Done = value; }
        get { return is_Done; }
    }

    public bool m_isGridApplied
    {
        set { is_GridApplied = value; }
        get { return is_GridApplied; }
    }

    public bool m_isTypeApplied
    {
        set { is_TypeApplied = value; }
        get { return is_TypeApplied; }
    }

    public bool m_isPlatformSpawn
    {
        set { is_PlatformSpawn = value; }
        get { return is_PlatformSpawn; }
    }

    public int m_numBomb
    {
        set { numBomb = value; }
        get { return numBomb; }
    }
    public int m_BombSpawnRate
    {
        set { BombSpawnRate = value; }
        get { return BombSpawnRate; }
    }

    public int m_numNormal
    {
        set { numNormal = value; }
        get { return numNormal; }
    }

    public string m_GridName
    {
        set { GridName = value; }
        get { return GridName; }
    }

    public Vector3 m_OldPosition
    {    
        get { return OldPosition; }
    }

    public List<GameObject> mList_Blocks
    {
        set { List_Blocks = value; }
        get { return List_Blocks; }
    }

    public void Reset_Variables()
    {
        numBomb = 0;
        numNormal = 0;
        BombSpawnRate = 0;
        is_Done = false;
        is_GridApplied = false;
        is_TypeApplied = false;
        is_PlatformSpawn = false;
    }
}
