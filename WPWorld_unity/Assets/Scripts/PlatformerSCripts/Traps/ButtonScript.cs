using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BUTTONTYPE
{
    ONETIME, // Activated once, cannot be turned off
    TOGGLE, // Can be turned on or off
    TIMER // Turned on by stepping on it, will turn off by itself soon.
}

public class ButtonScript : MonoBehaviour {

    public int ID;

    public BUTTONTYPE CurrType;

    // General Variables
    private bool HasInteracted;
    private bool HasStarted = true;

    // Animation Variables
    private float OrgScale;
    private Vector3 AlteringScale;
    public float ButtonSpeed;

    // Timer Variables
    public float TimeToReset;
    private float TimeElapsed;

    // Sound Variables
    public string ButtonSFX;
    private SoundSystem SoundSystemRef;

    public List<GameObject> ObjectsToChange = new List<GameObject>();

	// Use this for initialization
	void Start () {
        OrgScale = transform.GetChild(0).localScale.y;
        AlteringScale = transform.GetChild(0).localScale;

        SoundSystemRef = GameObject.FindGameObjectWithTag("SoundSystem").GetComponent<SoundSystem>();
	}
	
	// Update is called once per frame
	void Update () {
        transform.GetChild(0).transform.localScale = AlteringScale;

        if(HasInteracted)
        {
            switch (CurrType)
            {
                case (BUTTONTYPE.ONETIME):
                    if(HasStarted)
                    {
                        OnButton();
                        AlteringScale.y = 0.5f;
                        if (ButtonSFX != "")
                            SoundSystemRef.PlaySFX(ButtonSFX);
                        HasStarted = false;
                    }

                    HasInteracted = false;
                    break;
                case (BUTTONTYPE.TOGGLE):
                    if(HasStarted)
                    {
                        OnButton();
                        HasStarted = false;
                        HasInteracted = false;
                        AlteringScale.y = 0.5f;
                    }
                    else
                    {
                        OffButton();
                        HasStarted = true;
                        HasInteracted = false;
                        AlteringScale.y = 0.5f;
                    }

                    if (ButtonSFX != "")
                        SoundSystemRef.PlaySFX(ButtonSFX);

                    HasInteracted = false;
                    break;
                case (BUTTONTYPE.TIMER):
                    if(HasStarted)
                    {
                        OnButton();
                        HasStarted = false;

                        AlteringScale.y = 0.5f;

                        if (ButtonSFX != "")
                            SoundSystemRef.PlaySFX(ButtonSFX);
                    }
                    else
                    {
                        if(TimeToReset <= TimeElapsed)
                        {
                            OffButton();
                            HasStarted = true;
                            HasInteracted = false;
                            TimeElapsed = 0;
                        }
                        else
                            TimeElapsed += Time.deltaTime;
                    }
                    break;
            }
        }

        if(CurrType != BUTTONTYPE.ONETIME)
        {
            if (Mathf.Abs(AlteringScale.y - OrgScale) < OrgScale * 0.01f)
            {
                AlteringScale.y = OrgScale;
            }
            else
            {
                AlteringScale.y = Mathf.MoveTowards(AlteringScale.y, OrgScale, Time.deltaTime * ButtonSpeed);
            }
        }
    }

    public void OnButton()
    {
        for (int i = 0; i < ObjectsToChange.Count; i++)
        {
            if (ObjectsToChange[i] == null)
                continue;

            if (ObjectsToChange[i].GetComponent<BounceOnHit>())
            {
                ObjectsToChange[i].GetComponent<BounceOnHit>().Bounce();
            }

            if (ObjectsToChange[i].GetComponent<FallOnTop>())
            {
                ObjectsToChange[i].GetComponent<FallOnTop>().Fall();
            }

            if (ObjectsToChange[i].GetComponent<MoveOnCollide>())
            {
                ObjectsToChange[i].GetComponent<MoveOnCollide>().IsMoving = true;
            }

            if (ObjectsToChange[i].GetComponent<ShowOnHit>())
            {
                ObjectsToChange[i].GetComponent<ShowOnHit>().Show();
            }

            if (ObjectsToChange[i].GetComponent<SpawnOnHit>())
            {
                ObjectsToChange[i].GetComponent<SpawnOnHit>().Spawn();
            }
        }
    }

    public void OffButton()
    {
        for (int i = 0; i < ObjectsToChange.Count; i++)
        {
            if (ObjectsToChange[i] == null)
                continue;

            if (ObjectsToChange[i].GetComponent<MoveOnCollide>())
            {
                ObjectsToChange[i].GetComponent<MoveOnCollide>().IsMoving = false;
            }

            if (ObjectsToChange[i].GetComponent<ShowOnHit>())
            {
                ObjectsToChange[i].GetComponent<ShowOnHit>().Reset();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            HasInteracted = true;
        }
    }
}
