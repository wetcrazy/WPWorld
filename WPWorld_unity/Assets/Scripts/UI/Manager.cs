using UnityEngine;
using UnityEngine.UI;

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

    public void ChangeButtonColour()
    {
        for (int i = 0; i < gameObject.transform.childCount; ++i)
        {
            Transform theButton = gameObject.transform.GetChild(i);

            if (gameObject.transform.GetChild(i).GetComponent<Button>() == null)
            {
                continue;
            }

            if (gameObject.transform.GetChild(i).name == UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name)
            {
                theButton.GetComponent<Image>().color = Color.green;
            }
            else
            {
                theButton.GetComponent<Image>().color = Color.white;
            }
        }
    }
}
