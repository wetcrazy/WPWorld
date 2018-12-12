using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EggRollingScript : MonoBehaviour
{
    private bool isDone;
    private RectTransform RectTrans;
    private Vector2 newPos;
    private float speed;
    private Vector3 zRot;

	// Use this for initialization
	void Start ()
    {
        isDone = false;
        RectTrans = this.gameObject.GetComponent<RectTransform>();
        newPos = RectTrans.anchoredPosition;
        newPos.x += 110.0f;
        speed = 50.0f;
        zRot = new Vector3(0, 0, -160.0f);
	}
	
	// Update is called once per frame
	void Update ()
    {
        float step = speed * Time.deltaTime;
        RectTrans.anchoredPosition = Vector2.MoveTowards(RectTrans.anchoredPosition, newPos, step);

        if (RectTrans.anchoredPosition != newPos)
        {
            RectTrans.Rotate(zRot * step * Time.deltaTime);
        }

        Debug.Log(RectTrans.localRotation.z);
	}
}
