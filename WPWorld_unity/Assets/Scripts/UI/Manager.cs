using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public GameObject ARControllerOBJ;

    public void SendLevelInfoBrickBreaker()
    {
        ARControllerOBJ.SendMessage("NextObj");
    }
	
}
