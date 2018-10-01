using UnityEngine;
using UnityEngine.UI;

//--NOTE: THIS SCRIPT HAS BEEN DEPRECATED I.E. MARKED FOR REMOVAL--\\

public class Manager : MonoBehaviour
{
    public GameObject ARControllerOBJ;
    
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
