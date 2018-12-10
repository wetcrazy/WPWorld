using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallOnTop : MonoBehaviour {
	private Rigidbody RigidRef;
	private Renderer RenderRef;
	private Collider ColliderRef;

	private Vector3 OrgPos;

	[SerializeField]
	private float TimeToFall;

	private float TimeElapsed;

	[SerializeField]
	private string InteractedSFX;

	private SoundSystem SoundSystemRef;

	private bool IsFalling = false;

	// Use this for initialization
	void Start()
	{
		RigidRef = GetComponent<Rigidbody>();
		RenderRef = GetComponent<Renderer>();
		ColliderRef = GetComponent<Collider>();

		RigidRef.constraints = RigidbodyConstraints.FreezeAll;
		RigidRef.useGravity = true;
		OrgPos = transform.position;

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
				transform.position = OrgPos;
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
			if (hit.transform.tag == "Player")
			{
				IsFalling = true;
				if (InteractedSFX != "")
					SoundSystemRef.PlaySFX(InteractedSFX);
			}
		}
	}

	public void Reset()
	{
		RigidRef.constraints = RigidbodyConstraints.FreezeAll;
		RenderRef.enabled = true;
		ColliderRef.isTrigger = false;
		IsFalling = false;
		TimeElapsed = 0;

		transform.position = OrgPos;
	}
}
