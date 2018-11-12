using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniverseSelectionText : MonoBehaviour {
    GameObject CameraObject;

    private void Awake()
    {
        CameraObject = GameObject.FindGameObjectWithTag("MainCamera");
    }

    void LateUpdate()
    {
        gameObject.transform.LookAt(CameraObject.transform.position);
    }
}
