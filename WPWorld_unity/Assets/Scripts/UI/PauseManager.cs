using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour {

    GameManager gameManager = null;

    private void Start()
    {
            //gameManager =
    }

    public void PauseButtonDown()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

    public void SaveGame()
    {
        
    }

   public void QuitToMainMenu()
    {

    }
}
