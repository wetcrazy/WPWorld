using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour {

    [Tooltip("Buttons in Pause Bar\nPause Button must always be Element 0")]
    [SerializeField]
    List<GameObject> PauseBarButtons = new List<GameObject>();
    [SerializeField]
    float DistanceBetweenIcons = 30;
    [SerializeField]
    GameObject PauseBarBackground;

    Vector3 AnchorPos;
    bool isShowingPauseBar = false;

    //For external scripts
    public bool isPauseBarOpen
    {
        get { return isShowingPauseBar; }
    }

    private void Start()
    {
        AnchorPos = PauseBarButtons[0].transform.position;
        PauseBarBackground.gameObject.SetActive(false);

        for (int i = 0; i < PauseBarButtons.Count; ++i)
        {
            PauseBarButtons[i].SetActive(false);
            PauseBarButtons[i].transform.localPosition.Set((i + 1) * -DistanceBetweenIcons, -5, 0);
        }
    }
    
    public void PauseButtonDown()
    {
        //If Pause Bar is already open
        if(isShowingPauseBar)
        {
            DisablePauseBar();
        }
        else //If Pause Bar is closed 
        {
            EnablePauseBar();
            //isFirstTouchDown = true;
        }

        isShowingPauseBar = !isShowingPauseBar;
    }

    void EnablePauseBar()
    {
        foreach (GameObject theButton in PauseBarButtons)
        {
            theButton.SetActive(true);
        }

        PauseBarBackground.gameObject.SetActive(true);
    }

    void DisablePauseBar()
    {
        foreach (GameObject theButton in PauseBarButtons)
        {
            theButton.SetActive(false);
        }

        PauseBarBackground.gameObject.SetActive(false);
    }

    public void SaveGame()
    {
        
    }

   public void QuitToMainMenu()
    {

    }
}
