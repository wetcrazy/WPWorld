using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchorPoint : MonoBehaviour
{
    [SerializeField]
    private bool is_Done;
    [SerializeField]
    private int numBomb;
    [SerializeField]
    private int numNormal;
    [SerializeField]
    private List<GameObject> List_Blocks;


    public bool m_isdone
    {
        set { is_Done = value; }
        get { return is_Done; }
    }

    public int m_numBomb
    {
        set { numBomb = value; }
        get { return numBomb; }
    }

    public int m_numNormal
    {
        set { numNormal = value; }
        get { return numNormal; }
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
    }
}
