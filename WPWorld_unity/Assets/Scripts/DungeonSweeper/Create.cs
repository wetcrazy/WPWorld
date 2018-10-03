using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Create : MonoBehaviour
{
    public GameObject Manager;

    public void onCreateButtonDown()
    {
        Manager.SendMessage("Set_Anchorie", DungeonsweeperManager.AnchorPointType.ANCHOR_TWO);
    }

}
