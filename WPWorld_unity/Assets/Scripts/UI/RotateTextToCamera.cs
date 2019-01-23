using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTextToCamera : MonoBehaviour {
    GameObject CameraObject;

    private void Start()
    {
        CameraObject = GameObject.FindGameObjectWithTag("MainCamera");
    }

    void LateUpdate()
    {
        gameObject.transform.forward = (gameObject.transform.position - CameraObject.transform.position).normalized;
    }
}
