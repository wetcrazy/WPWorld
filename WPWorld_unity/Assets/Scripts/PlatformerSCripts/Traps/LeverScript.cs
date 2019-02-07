using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverScript : MonoBehaviour {

    [Header("ID Settings")]
    public int ID;

    // Shared Variables
    [Header("Main Settings")]
    [SerializeField]
    private bool HasInteracted;
    private bool HasDoneAction = false;

    [Space]
    // Animation Variables
    [Header("Animation Settings")]
    private Vector3 LeverRotation;
    private Vector3 MoveToRotation;
    [SerializeField]
    private float LeverSpeed;

    [Space]
    // Sound Variables
    [Header("Sound Settings")]
    [SerializeField]
    private string LeverSFX;
    private SoundSystem SoundSystemRef;

    // All game objects to trigger
    [Header("Objects to Trigger")]
    public List<GameObject> ObjectsToChange = new List<GameObject>();

	// Use this for initialization
	void Start () {
        LeverRotation = new Vector3(-55, 0, 0);
        MoveToRotation = new Vector3(-55, 0, 0);

        SoundSystemRef = GameObject.FindGameObjectWithTag("SoundSystem").GetComponent<SoundSystem>();
    }
	
	// Update is called once per frame
	void Update () {
        if(HasInteracted)
        {
            if (!HasDoneAction)
            {
                OnLever();
            }
            else
            {
                OffLever();
            }

            if (LeverSFX != "")
                SoundSystemRef.PlaySFX(LeverSFX);
        }

        if(Vector3.Distance(LeverRotation, MoveToRotation) > transform.localScale.x)
        {
            LeverRotation = Vector3.Lerp(LeverRotation, MoveToRotation, Time.deltaTime * LeverSpeed);
        }

        transform.GetChild(0).localEulerAngles = LeverRotation;
    }

    public void OnLever()
    {
        for(int i = 0; i < ObjectsToChange.Count; i++)
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

            if(ObjectsToChange[i].GetComponent<MoveOnCollide>())
            {
                ObjectsToChange[i].GetComponent<MoveOnCollide>().IsMoving = true;
            }

            if (ObjectsToChange[i].GetComponent<ShowOnHit>())
            {
                ObjectsToChange[i].GetComponent<ShowOnHit>().Show();
            }

            if(ObjectsToChange[i].GetComponent<SpawnOnHit>())
            {
                ObjectsToChange[i].GetComponent<SpawnOnHit>().Spawn();
            }
        }

        HasInteracted = false;
        HasDoneAction = true;

        MoveToRotation = new Vector3(55, 0, 0);
    }

    public void OffLever()
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

        HasInteracted = false;
        HasDoneAction = false;

        MoveToRotation = new Vector3(-55, 0, 0);
    }

    public void Activate()
    {
        HasInteracted = !HasInteracted;
    }
}
