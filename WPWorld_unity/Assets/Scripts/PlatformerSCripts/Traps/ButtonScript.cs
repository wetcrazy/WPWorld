using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour {

    [SerializeField]
    private bool HasInteracted;
    private bool HasDoneAction = false;

    [SerializeField]
    private float ButtonTimeDelay;
    private float TimeElapsed;

    // Animation Variables
    private Vector3 OrgSize;
    private Vector3 CurrScale;
    private Vector3 ToScale;
    [SerializeField]
    private float ButtonSpeed;

    [SerializeField]
    private float TimeToComplete;

    [SerializeField]
    private List<GameObject> ObjectsToChange = new List<GameObject>();

	// Use this for initialization
	void Start () {
        OrgSize = transform.GetChild(0).localScale;

        CurrScale = OrgSize;
        ToScale = OrgSize;
	}
	
	// Update is called once per frame
	void Update () {
        if (HasInteracted)
        {
            if (!HasDoneAction)
            {
                Action();

                CurrScale.y = 0.5f;
            }
            else
            {
                if (TimeElapsed >= ButtonTimeDelay)
                    Revert();
                else
                    TimeElapsed += Time.deltaTime;

                ToScale.y = OrgSize.y;
            }
        }

        if(Vector3.Distance(CurrScale, ToScale) > transform.localScale.x * 0.1f)
        {
            CurrScale = Vector3.Lerp(CurrScale, ToScale, Time.deltaTime * ButtonSpeed);
            TimeToComplete += Time.deltaTime;
        }
        transform.GetChild(0).localScale = CurrScale;
    }

    private void Action()
    {
        for(int i = 0;i < ObjectsToChange.Count; i++)
        {
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

        HasDoneAction = true;
    }

    private void Revert()
    {
        for (int i = 0; i < ObjectsToChange.Count; i++)
        {
            if (ObjectsToChange[i].GetComponent<MoveOnCollide>())
            {
                ObjectsToChange[i].GetComponent<MoveOnCollide>().IsMoving = false;
            }

            if (ObjectsToChange[i].GetComponent<ShowOnHit>())
            {
                ObjectsToChange[i].GetComponent<ShowOnHit>().Reset();
            }
        }

        TimeElapsed = 0;
        HasInteracted = false;
        HasDoneAction = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!HasDoneAction)
            HasInteracted = true;
    }
}
