using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public GameObject ARControllerOBJ;

    //public void SendLevelInfoBrickBreaker()
    //{
    //    ARControllerOBJ.SendMessage("SetNextObject", "TrapPlayground");
    //}

    //public void SendLevelInfo3DPuzzle()
    //{
    //    ARControllerOBJ.SendMessage("SetNextObject", "3DPuzzleStage1");
    //}

    public void SendLevelInfo(string LevelName)
    {
        ARControllerOBJ.SendMessage("SetNextObject", LevelName);
    }

    public void ChangeButtonColour()
    {
        for (int i = 0; i < gameObject.transform.childCount; ++i)
        {
            GameObject theButton = gameObject.transform.GetChild(i).gameObject;

            if (theButton.GetComponent<Button>() == null)
            {
                continue;
            }

            if (theButton.name == UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name)
            {
                //Change selected button to green
                theButton.GetComponent<Image>().color = Color.green;
            }
            else
            {
                theButton.GetComponent<Image>().color = Color.white;
            }
        }
    }
}
