using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnCollide : MonoBehaviour {

    [SerializeField]
    private Vector3 MoveAmount;
    [SerializeField]
    private float MoveSpeed;

    [SerializeField]
    private Vector3 MovePosition;

    private Vector3 OrgPosition;

    private bool IsMoving = false;

	private PlayerMovement PlayerRef;
	private MovementAvaliability OrgAvaliability;

	// Use this for initialization
	void Start () {
        OrgPosition = transform.localPosition;
        MovePosition = transform.localPosition + MoveAmount;

		PlayerRef = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
		OrgAvaliability = PlayerRef.GetRestriction();
	}
	
	// Update is called once per frame
	void Update () {
		if(IsMoving)
        {
            if(Vector3.Distance(transform.localPosition, MovePosition) > transform.localScale.magnitude * 0.01f)
			{
				this.transform.localPosition = Vector3.Lerp(transform.localPosition, MovePosition, Time.deltaTime * MoveSpeed);
				PlayerRef.SetRestriction(MovementAvaliability.NONE);
			}
            else
			{
				this.transform.localPosition = MovePosition;
				PlayerRef.SetRestriction(OrgAvaliability);
				PlayerRef.gameObject.GetComponent<TPSLogic>().AbleToJumpPub = false;
				IsMoving = false;
			}
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
            IsMoving = true;
    }

    public void Reset()
    {
        transform.localPosition = OrgPosition;
        IsMoving = false;
    }
}
