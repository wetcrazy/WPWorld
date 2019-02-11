using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombermanPlayingField : MonoBehaviour
{
    public GameObject Player { get; set; }
    public GameObject WallParent;
    public GameObject FloorParent;
    public GameObject BreakablesParent;

    public List<GameObject> List_Floors;
    public List<GameObject> List_Breakables;
}
