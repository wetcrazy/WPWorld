using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EggRollingScript : MonoBehaviour
{
    private bool isDone;
    private RectTransform rect;
    private float speed;
    private Vector3 zRot;

    [SerializeField]
    private GameObject Target;

	// Use this for initialization
	void Start ()
    {
        isDone = false;
        rect = this.gameObject.GetComponent<RectTransform>();   
        speed = 50.0f;
        zRot = new Vector3(0, 0, -100.0f);
	}
	
	// Update is called once per frame
	void Update ()
    {
        float step = speed * Time.deltaTime;
        // rect.anchoredPosition = Vector2.MoveTowards(rect.anchoredPosition, newPos, step);        
        rect.anchoredPosition = Vector2.MoveTowards(rect.anchoredPosition, Target.transform.localPosition, step);
        if (rect.anchoredPosition != new Vector2(Target.transform.localPosition.x, Target.transform.localPosition.y))
        {
            rect.Rotate(zRot * Time.deltaTime);              
        }
        else
        {
            isDone = true;
        }       
        
        if(isDone)
        {
            SendMessageManager();
        }
    }

    public bool GetisDone()
    {
        return isDone;
    }

    public void SendMessageManager()
    {
        var Manger = GameObject.FindGameObjectWithTag("SplashManager");
        Manger.GetComponent<SplashScreenManager>().RevealButtons();
    }
}
