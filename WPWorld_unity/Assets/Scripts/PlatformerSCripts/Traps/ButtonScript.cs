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

    public BUTTONTYPE CurrType;

    // General Variables
    private bool HasInteracted;
    private bool HasStarted = true;

    private float OrgScale;
    private Vector3 AlteringScale;

    // One Time Variables

    // Toggle Variables

    // Timer Variables
    public float TimeToReset;
    private float TimeElapsed;

    public string ButtonSFX;

    public List<GameObject> ObjectsToChange = new List<GameObject>();

	// Use this for initialization
	void Start () {
        OrgScale = transform.GetChild(0).localScale.y;
        AlteringScale = transform.GetChild(0).localScale;   
	}
	
	// Update is called once per frame
	void Update () {
        transform.GetChild(0).transform.localScale = AlteringScale;

        if(HasInteracted)
        {
            switch (CurrType)
            {
                case (BUTTONTYPE.ONETIME):
                    Action();
                    HasInteracted = false;
                    break;
                case (BUTTONTYPE.TOGGLE):
                    if(HasStarted)
                    {
                        Action();
                        HasStarted = false;
                        HasInteracted = false;
                    }
                    else
                    {
                        Revert();
                        HasStarted = true;
                        HasInteracted = false;
                    }

                    HasInteracted = false;
                    break;
                case (BUTTONTYPE.TIMER):
                    if(HasStarted)
                    {
                        Action();
                        HasStarted = false;
                    }
                    else
                    {
                        if(TimeToReset <= TimeElapsed)
                        {
                            Revert();
                            HasStarted = true;
                            HasInteracted = false;
                            TimeElapsed = 0;
                        }
                        else
                        {
                            TimeElapsed += Time.deltaTime;
                            AlteringScale.y = Mathf.Lerp(AlteringScale.y, OrgScale, Time.deltaTime);
                        }
                    }
                    break;
            }
        }
    }

    private void Action()
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

        AlteringScale.y = 0.7f;
    }

    private void Revert()
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

        AlteringScale.y = OrgScale;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            HasInteracted = true;
        }
    }
}
