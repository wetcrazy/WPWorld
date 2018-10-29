using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthPopup : MonoBehaviour {

    private Image ImageRef;

    private Color ColorRef;

    private TPSLogic PlayerRef;

    //Debug Serialize
    [SerializeField]
    private bool Showing = false;

    [SerializeField]
    private GameObject NormalHeart;
    [SerializeField]
    private GameObject DeathHeart;

    private float TimeElapsed;

	// Use this for initialization
	void Start () {
        ImageRef = GetComponent<Image>();

        ColorRef = ImageRef.color;
        ColorRef.a = 0;
        ImageRef.color = ColorRef;

        PlayerRef = GameObject.FindGameObjectWithTag("Player").GetComponent<TPSLogic>();
	}
	
	// Update is called once per frame
	void Update () {
        // Spawning
        if (transform.GetChild(0).childCount != Mathf.Abs(PlayerRef.GetDeaths()))
        {
            if (transform.GetChild(0).childCount != 0)
                for (int i = 0; i < transform.GetChild(0).childCount; i++)
                {
                    Destroy(transform.GetChild(0).GetChild(i).gameObject);
                }
            else
                SpawnHearts();
        }

        // Fading in & out effect
        ImageRef.color = ColorRef / 2;
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<Image>().color = ColorRef;

            for(int j = 0; j < transform.GetChild(i).childCount; j++)
            {
                Color HeartColor = new Vector4(1, 1, 1, ColorRef.a);
                transform.GetChild(i).GetChild(j).GetComponent<Image>().color = HeartColor;
            }
        }

        // Behaviour
        if(Showing)
        {
            if (ColorRef.a < 1)
            {
                ColorRef.a += Time.deltaTime;
            }
            else
            {
                ColorRef.a = 1;

                TimeElapsed += Time.deltaTime;

                if(TimeElapsed > 1.0f)
                {
                    TimeElapsed = 0;
                    Showing = false;
                }
            }

            transform.GetChild(0).transform.position = Vector3.Lerp(transform.GetChild(0).transform.position, new Vector3(Screen.width * 0.5f, Screen.height * 0.5f), 1.5f * Time.deltaTime);
        }
        else
        {
            if (ColorRef.a > 0)
            {
                ColorRef.a -= Time.deltaTime;

                transform.GetChild(0).transform.position = Vector3.Lerp(transform.GetChild(0).transform.position, new Vector3(Screen.width * 0.5f, Screen.height + transform.GetChild(0).GetComponent<RectTransform>().sizeDelta.y), 1.75f * Time.deltaTime);
            }
            else
            {
                ColorRef.a = 0;
                transform.GetChild(0).transform.position = new Vector3(Screen.width / 2,
                    -transform.GetChild(0).GetComponent<RectTransform>().sizeDelta.y * 0.5f,
                    0);
            }
        }
    }

    private void SpawnHearts()
    {
        for (int i = 1; i <= Mathf.Abs(PlayerRef.GetDeaths()); ++i)
        {
            GameObject SpawnHeart;
            if (i == 1)
            {
                if (PlayerRef.GetDeaths() > 0)
                    SpawnHeart = Instantiate(DeathHeart, transform.GetChild(0).position, transform.rotation);
                else
                    SpawnHeart = Instantiate(NormalHeart, transform.GetChild(0).position, transform.rotation);
                SpawnHeart.GetComponent<RectTransform>().SetParent(transform.GetChild(0));
                SpawnHeart.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                continue;
            }

            if (i % 2 == 0)
            {
                if (PlayerRef.GetDeaths() > 0)
                    SpawnHeart = Instantiate(DeathHeart, transform.GetChild(0).position, transform.rotation);
                else
                    SpawnHeart = Instantiate(NormalHeart, transform.GetChild(0).position, transform.rotation);
                SpawnHeart.transform.position -= new Vector3(SpawnHeart.GetComponent<RectTransform>().sizeDelta.x * (i / 2), 0, 0);
            }
            else
            {
                if (PlayerRef.GetDeaths() > 0)
                    SpawnHeart = Instantiate(DeathHeart, transform.GetChild(0).position, transform.rotation);
                else
                    SpawnHeart = Instantiate(NormalHeart, transform.GetChild(0).position, transform.rotation);
                SpawnHeart.transform.position += new Vector3(SpawnHeart.GetComponent<RectTransform>().sizeDelta.x * Mathf.Floor(i / 2), 0, 0);
            }

            SpawnHeart.GetComponent<RectTransform>().SetParent(transform.GetChild(0));
            SpawnHeart.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        }
    }
}
