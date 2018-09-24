using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public GameObject ARControllerOBJ;

    public void SendLevelInfoBrickBreaker()
    {
        ARControllerOBJ.SendMessage("SetNextObject", "BrickBreaker");
    }

    public void SendLevelInfo3DPuzzle()
    {
        ARControllerOBJ.SendMessage("SetNextObject", "3DPuzzleStage1");
    }
}
