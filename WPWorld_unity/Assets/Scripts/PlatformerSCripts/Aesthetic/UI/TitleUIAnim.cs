using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleUIAnim : MonoBehaviour {

    [Header("Animation Settings")]
    [SerializeField]
    private GameObject Egg;
    [SerializeField]
    private GameObject VentureText;
    private RectTransform VentureRef;

    [SerializeField]
    private Vector3 VentureTextMoveToPoint;
    [SerializeField]
    private float VentureTextSpeed;

    private Image ImageRef;
    private Color ColorRef;

	// Use this for initialization
	void Start () {
        ImageRef = GetComponent<Image>();
        ColorRef = ImageRef.color;

        VentureRef = VentureText.GetComponent<RectTransform>();

        ColorRef.a = 0;
	}
	
	// Update is called once per frame
	void Update () {

        if(!transform.parent.gameObject.activeSelf)
        {
            return;
        }

        if(ColorRef.a < 1.0f)
        {
            ColorRef.a += 0.05f;
        }
        else
        {
            if(Vector3.Distance(Egg.transform.parent.localEulerAngles, new Vector3(0,0,180)) < 0.1f)
            {
                Egg.transform.parent.localEulerAngles = new Vector3(0, 0, 180);

                VentureRef.anchoredPosition = Vector3.MoveTowards(VentureRef.anchoredPosition, VentureTextMoveToPoint, VentureTextSpeed * Time.deltaTime); 
            }
            else
            {
                Egg.transform.parent.localEulerAngles = Vector3.MoveTowards(Egg.transform.parent.localEulerAngles, new Vector3(0, 0, 180), 200 * Time.deltaTime);
            }
        }

        // Transparency Changes
        {
            ImageRef.color = ColorRef;
            Egg.GetComponent<Image>().color = ColorRef;
            VentureText.GetComponent<Image>().color = ColorRef;
        }
	}
}
