using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceOnHit : MonoBehaviour {

	[SerializeField]
	private string BounceSFX;

	private SoundSystem SoundSystemRef;

	private float TimeElapsed;

	private Rigidbody RigidRef;
	private Collider ColliderRef;
	private Vector3 OrgPos;

	// Use this for initialization
	void Start()
	{
		RigidRef = GetComponent<Rigidbody>();
		ColliderRef = GetComponent<Collider>();

		OrgPos = this.transform.position;

		SoundSystemRef = GameObject.FindGameObjectWithTag("SoundSystem").GetComponent<SoundSystem>();
	}

	// Update is called once per frame
	void Update()
	{
		if (transform.position.y < OrgPos.y)
		{
			RigidRef.constraints = RigidbodyConstraints.FreezeAll;
			transform.position = OrgPos;
			ColliderRef.isTrigger = false;
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			if (!other.GetComponent<TPSLogic>().GetGrounded()
				&& other.GetComponent<Rigidbody>().velocity.y >= 0)
			{
				// Check for Enemies above the block
				RaycastHit hit;
				if (Physics.Raycast(transform.position, transform.up, out hit, transform.lossyScale.y)
					|| Physics.Raycast(transform.position, transform.up + transform.right, out hit, transform.lossyScale.y)
					|| Physics.Raycast(transform.position, transform.up - transform.right, out hit, transform.lossyScale.y)
					|| Physics.Raycast(transform.position, transform.up + transform.forward, out hit, transform.lossyScale.y)
					|| Physics.Raycast(transform.position, transform.up - transform.forward, out hit, transform.lossyScale.y)
					)
				{
					if (hit.transform.name.Contains("Enemy"))
					{
						hit.transform.GetComponent<Enemy>().AirDeath();
					}
				}

				if (BounceSFX != "")
					SoundSystemRef.PlaySFX(BounceSFX);
				RigidRef.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
				RigidRef.AddForce(transform.up * 0.5f, ForceMode.VelocityChange);

				ColliderRef.isTrigger = true;
			}
		}
	}
}
