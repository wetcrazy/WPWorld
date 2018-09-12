using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemovePlane : MonoBehaviour
{
    /// <summary>
    /// Removes tracking textures
    /// </summary>

    // Update is called once per frame
    void Update ()
    {
		if(CheckPlanetExistance())
        {
            gameObject.SetActive(false);
            return;
        }
        gameObject.SetActive(true);
	}

    // Check if the spawn exist
    private bool CheckPlanetExistance()
    {
        GameObject planetOBJ = GameObject.FindGameObjectWithTag("Stage Floor");
        if (planetOBJ == null || !planetOBJ.activeSelf)
        {
            return false;
        }
        return true;
    }
}
