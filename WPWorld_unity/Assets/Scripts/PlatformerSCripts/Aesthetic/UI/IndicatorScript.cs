using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IndicatorScript : MonoBehaviour {

    [SerializeField]
    private float Speed = 1;

    private GameObject LookAtRef;

    private RectTransform RectRef;

	// Use this for initialization
	void Start () {
        LookAtRef = new GameObject("Test");

        RectRef = GetComponent<RectTransform>();

        Color N_Color = transform.GetChild(0).GetComponent<Image>().color;
        N_Color.a = 0;
        transform.GetChild(0).GetComponent<Image>().color = N_Color;
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 ScreenPoint = Camera.main.WorldToViewportPoint(GameObject.FindGameObjectWithTag("Player").transform.position);
        LookAtRef.transform.position = new Vector3(ScreenPoint.x * Screen.width, ScreenPoint.y * Screen.height, 0);
        transform.position = Vector3.Lerp(transform.position, LookAtRef.transform.position, Speed * Time.deltaTime);
        if (ScreenPoint.x > 0 && ScreenPoint.x < 1 &&
            ScreenPoint.y > 0 && ScreenPoint.y < 1)
        {
            if(transform.GetChild(0).GetComponent<Image>().color.a > 0)
            {
                Color N_Color = transform.GetChild(0).GetComponent<Image>().color;
                N_Color.a -= Time.deltaTime;
                transform.GetChild(0).GetComponent<Image>().color = N_Color;
            }
            return;
        }

        if (transform.GetChild(0).GetComponent<Image>().color.a < 1)
        {
            Color N_Color = transform.GetChild(0).GetComponent<Image>().color;
            N_Color.a += Time.deltaTime;
            transform.GetChild(0).GetComponent<Image>().color = N_Color;
        }

        transform.LookAt(LookAtRef.transform);

        if(RectRef.position.x < RectRef.sizeDelta.x / 2)
        {
            Vector3 n_Pos = RectRef.position;
            n_Pos.x = RectRef.sizeDelta.x / 2;
            RectRef.position = n_Pos;
        }

        if (RectRef.position.x > Screen.width - RectRef.sizeDelta.x / 2)
        {
            Vector3 n_Pos = RectRef.position;
            n_Pos.x = Screen.width - RectRef.sizeDelta.x / 2;
            RectRef.position = n_Pos;
        }

        if (RectRef.position.y < RectRef.sizeDelta.y / 2)
        {
            Vector3 n_Pos = RectRef.position;
            n_Pos.y = RectRef.sizeDelta.y / 2;
            RectRef.position = n_Pos;
        }

        if (RectRef.position.y > Screen.width - RectRef.sizeDelta.y / 2)
        {
            Vector3 n_Pos = RectRef.position;
            n_Pos.y = Screen.width - RectRef.sizeDelta.y / 2;
            RectRef.position = n_Pos;
        }
    }
}
