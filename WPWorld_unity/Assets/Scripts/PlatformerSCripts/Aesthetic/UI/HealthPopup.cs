using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthPopup : MonoBehaviour
{
    private Image ImageRef;

    private Color ColorRef;

    private GameObject PlayerRef;
    private MovementAvaliability OrgAvaliability;

    //Debug Serialize
    [SerializeField]
    private bool Showing = false;
    private bool HasReset = true;

    [SerializeField]
    private GameObject NormalHeart;
    [SerializeField]
    private GameObject DeathHeart;

    private float TimeElapsed;

    // Use this for initialization
    void Start()
    {
        ImageRef = GetComponent<Image>();

        ColorRef = ImageRef.color;
        ColorRef.a = 0;
        ImageRef.color = ColorRef;

        PlayerRef = GameObject.FindGameObjectWithTag("Player");
        OrgAvaliability = PlayerRef.GetComponent<PlayerMovement>().GetRestriction();
    }

    // Update is called once per frame
    void Update()
    {
        // Spawning
        if (transform.GetChild(0).childCount != Mathf.Abs(PlayerRef.GetComponent<TPSLogic>().GetDeaths()))
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
            if (transform.GetChild(i).GetComponent<Image>() != null)
                transform.GetChild(i).GetComponent<Image>().color = ColorRef;
            if (transform.GetChild(i).GetComponent<Text>() != null)
            {
                Color TextColor = transform.GetChild(i).GetComponent<Text>().color;
                TextColor.a = ColorRef.a;
                transform.GetChild(i).GetComponent<Text>().color = TextColor;
            }

            for (int j = 0; j < transform.GetChild(i).childCount; j++)
            {
                Color HeartColor = new Vector4(1, 1, 1, ColorRef.a);
                transform.GetChild(i).GetChild(j).GetComponent<Image>().color = HeartColor;
            }
        }

        // Behaviour
        if (Showing)
        {
            if (ColorRef.a < 1)
            {
                ColorRef.a += 2.0f * Time.deltaTime;
            }
            else
            {
                ColorRef.a = 1;

                TimeElapsed += Time.deltaTime;

                if (TimeElapsed > 0.25f)
                {
                    TimeElapsed = 0;
                    Showing = false;
                }
            }

            transform.GetChild(0).transform.position = Vector3.Lerp(transform.GetChild(0).transform.position, new Vector3(Screen.width * 0.5f, Screen.height * 0.5f), 3.0f * Time.deltaTime);
        }
        else
        {
            if (ColorRef.a > 0)
            {
                ColorRef.a -= 2.0f * Time.deltaTime;

                transform.GetChild(0).transform.position = Vector3.Lerp(transform.GetChild(0).transform.position, new Vector3(Screen.width * 0.5f, Screen.height + transform.GetChild(0).GetComponent<RectTransform>().sizeDelta.y), 3.0f * Time.deltaTime);
            }
            else
            {
                ColorRef.a = 0;
                transform.GetChild(0).transform.position = new Vector3(Screen.width / 2,
                    -transform.GetChild(0).GetComponent<RectTransform>().sizeDelta.y * 0.5f,
                    0);
                if(!HasReset)
                {
                    PlayerRef.GetComponent<PlayerMovement>().SetRestriction(OrgAvaliability);
                    PlayerRef.GetComponent<TPSLogic>().SetJumpRestrict(false);
                    HasReset = true;
                }
            }
        }
    }

    private void SpawnHearts()
    {
        for (float y = 0; y < Mathf.Ceil(Mathf.Abs(PlayerRef.GetComponent<TPSLogic>().GetDeaths()) / Mathf.Round(Screen.width / DeathHeart.GetComponent<RectTransform>().sizeDelta.x)); y++)
        {
            for (int x = 1; x <= Mathf.Abs(PlayerRef.GetComponent<TPSLogic>().GetDeaths()) - (y * Mathf.Round(Screen.width / DeathHeart.GetComponent<RectTransform>().sizeDelta.x)); ++x)
            {
                if (x > Mathf.Round(Screen.width / DeathHeart.GetComponent<RectTransform>().sizeDelta.x))
                {
                    continue;
                }
                GameObject SpawnHeart;
                if (PlayerRef.GetComponent<TPSLogic>().GetDeaths() > 0)
                    SpawnHeart = Instantiate(DeathHeart, transform.GetChild(0).position, transform.rotation);
                else
                    SpawnHeart = Instantiate(NormalHeart, transform.GetChild(0).position, transform.rotation);

                SpawnHeart.GetComponent<RectTransform>().SetParent(transform.GetChild(0));
                SpawnHeart.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

                if (y % 2 == 0 && y != 0)
                    SpawnHeart.transform.localPosition -= new Vector3(0, SpawnHeart.GetComponent<RectTransform>().sizeDelta.y * (y / 2), 0);
                else
                    SpawnHeart.transform.localPosition += new Vector3(0, SpawnHeart.GetComponent<RectTransform>().sizeDelta.y * Mathf.Ceil(y / 2), 0);

                if (x == 1)
                {
                    continue;
                }

                if (x % 2 == 0)
                    SpawnHeart.transform.localPosition -= new Vector3(SpawnHeart.GetComponent<RectTransform>().sizeDelta.x * (x / 2), 0, 0);
                else
                    SpawnHeart.transform.localPosition += new Vector3(SpawnHeart.GetComponent<RectTransform>().sizeDelta.x * Mathf.Floor(x / 2), 0, 0);
            }
        }
    }

    public void ShowDisplay()
    {
        if (ColorRef.a != 0)
            return;
        OrgAvaliability = PlayerRef.GetComponent<PlayerMovement>().GetRestriction();
        PlayerRef.GetComponent<PlayerMovement>().SetRestriction(MovementAvaliability.NONE);
        PlayerRef.GetComponent<TPSLogic>().SetJumpRestrict(true);
        Showing = true;
        HasReset = false;
    }
}
