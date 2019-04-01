using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EggRollingScript : MonoBehaviour
{
    private bool isDone;
    private RectTransform rect;
    private float speed;
    private float time = 1.0f;
    private Vector3 zRot;

    [SerializeField]
    private GameObject Target;

	// Use this for initialization
	void Start ()
    {
        isDone = false;
        rect = this.gameObject.GetComponent<RectTransform>();        
	}

    // Update is called once per frame
    void Update()
    {
        speed = Vector2.Distance(rect.anchoredPosition, Target.transform.localPosition) / time;
        float step = speed * Time.deltaTime;
        zRot = new Vector3(0, 0, -speed);
        // rect.anchoredPosition = Vector2.MoveTowards(rect.anchoredPosition, newPos, step);        
        rect.anchoredPosition = Vector2.MoveTowards(rect.anchoredPosition, Target.transform.localPosition, step);
        SendMessageManager();
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
