using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchorPoint : MonoBehaviour
{
    [SerializeField]
    private bool is_Done;
    [SerializeField]
    private float numBomb;
    [SerializeField]
    private float totalBlockCount;
    [SerializeField]
    private List<GameObject> List_Blocks;


    public bool m_isdone
    {
        set { is_Done = value; }
        get { return is_Done; }
    }

    public float m_numBomb
    {
        set { numBomb = value; }
        get { return numBomb; }
    }

    public float m_totalBlockCount
    {
        set { totalBlockCount = value; }
        get { return totalBlockCount; }
    }

    public List<GameObject> mList_Blocks
    {
        set { List_Blocks = value; }
        get { return List_Blocks; }
    }

    public void Reset_Variables()
    {
        numBomb = 0;
        totalBlockCount = 0;
    }
}
