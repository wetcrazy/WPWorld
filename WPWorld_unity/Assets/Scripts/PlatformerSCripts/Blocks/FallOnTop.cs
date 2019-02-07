using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallOnTop : MonoBehaviour {

    [Header("ID Settings")]
    public int ID;

	private Vector3 OrgPos;

	[SerializeField]
	private float TimeToFall;
	private float TimeElapsed;
    private bool IsFalling = false; 

    [SerializeField]
	private string InteractedSFX;

    private Rigidbody RigidRef;
    private Renderer RenderRef;
    private Collider ColliderRef;
    private SoundSystem SoundSystemRef;

    // Use this for initialization
    void Start()
	{
		RigidRef = GetComponent<Rigidbody>();
		RenderRef = GetComponent<Renderer>();
		ColliderRef = GetComponent<Collider>();

		RigidRef.constraints = RigidbodyConstraints.FreezeAll;
		RigidRef.useGravity = true;
		OrgPos = transform.localPosition;

		SoundSystemRef = GameObject.FindGameObjectWithTag("SoundSystem").GetComponent<SoundSystem>();
	}

	// Update is called once per frame
	void Update()
	{
		if (IsFalling)
		{
			if (TimeToFall > TimeElapsed)
			{
				TimeElapsed += Time.deltaTime;

				transform.localPosition += Random.insideUnitSphere * transform.localScale.x * transform.localScale.y * transform.localScale.z;
			}
			else
			{
				transform.localPosition = OrgPos;
				RigidRef.constraints = RigidbodyConstraints.None;
				ColliderRef.isTrigger = true;

				IsFalling = false;
			}
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Killbox")
		{
			RenderRef.enabled = false;
			RigidRef.constraints = RigidbodyConstraints.FreezeAll;
		}
	}

	private void OnCollisionStay(Collision collision)
	{
		if (IsFalling || TimeElapsed >= TimeToFall)
			return;

		RaycastHit hit;

		if (Physics.Raycast(transform.position, transform.up, out hit, transform.localScale.y)
			|| Physics.Raycast(transform.position, transform.up + transform.forward * 0.5f, out hit, transform.localScale.y)
			|| Physics.Raycast(transform.position, transform.up - transform.forward * 0.5f, out hit, transform.localScale.y)
			|| Physics.Raycast(transform.position, transform.up + transform.right * 0.5f, out hit, transform.localScale.y)
			|| Physics.Raycast(transform.position, transform.up - transform.right * 0.5f, out hit, transform.localScale.y))
		{
			if (hit.transform.tag == "Player" && hit.transform.GetComponent<TPSLogic>().isMine())
			{
                //Send event to all players that this block has fallen
                object[] content = new object[]
                    {
                        ID
                    };

                ExitGames.Client.Photon.SendOptions sendOptions = new ExitGames.Client.Photon.SendOptions { Reliability = true };
                Photon.Pun.PhotonNetwork.RaiseEvent((byte)EventCodes.EVENT_CODES.PLATFORM_EVENT_BLOCK_FALL, content, Photon.Realtime.RaiseEventOptions.Default, sendOptions);

                Fall();
			}
		}
	}

    public void Fall()
    {
        if (IsFalling || TimeElapsed >= TimeToFall)
            return;

        IsFalling = true;
        if (InteractedSFX != "")
            SoundSystemRef.PlaySFX(InteractedSFX);
    }

	public void Reset()
	{
		RigidRef.constraints = RigidbodyConstraints.FreezeAll;
		RenderRef.enabled = true;
		ColliderRef.isTrigger = false;
		IsFalling = false;
		TimeElapsed = 0;

		transform.localPosition = OrgPos;
	}
}
