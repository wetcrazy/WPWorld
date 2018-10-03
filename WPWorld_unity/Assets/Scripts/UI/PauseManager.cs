using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour {

    [Tooltip("Buttons in Pause Bar\nPause Button must always be Element 0")]
    [SerializeField]
    List<GameObject> PauseBarButtons = new List<GameObject>();
    [SerializeField]
    float DistanceBetweenIcons = 30;
    [SerializeField]
    GameObject PauseBarBackground;
    [SerializeField]
    float PauseBarBackgroundFillSpeed = 40;


    Vector3 AnchorPos;
    bool isShowingPauseBar = false;
    float PauseBarBackgroundFillAmount = 0;

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

    private void Update()
    {
        if (isShowingPauseBar && PauseBarBackgroundFillAmount < 1.3f)
        {
            PauseBarBackgroundFillAmount += (Time.deltaTime * PauseBarBackgroundFillSpeed);
            Vector3 newLocalScale = PauseBarBackground.GetComponent<RectTransform>().localScale;
            newLocalScale.x = PauseBarBackgroundFillAmount;
            PauseBarBackground.GetComponent<RectTransform>().localScale = newLocalScale;
        }
        else if (!isShowingPauseBar)
        {
            if (PauseBarBackgroundFillAmount > 0)
            {
                PauseBarBackgroundFillAmount -= (Time.deltaTime * PauseBarBackgroundFillSpeed);
                Vector3 newLocalScale = PauseBarBackground.GetComponent<RectTransform>().localScale;
                newLocalScale.x = PauseBarBackgroundFillAmount;
                PauseBarBackground.GetComponent<RectTransform>().localScale = newLocalScale;
            }
            else
            {
                PauseBarBackgroundFillAmount = 0;
                PauseBarBackground.gameObject.SetActive(false);
            }
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
    }

    public void SaveGame()
    {
        
    }

   public void QuitToMainMenu()
    {

    }
}
