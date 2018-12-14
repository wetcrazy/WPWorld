using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowOnHit : MonoBehaviour {

	private BoxCollider ColliderRef;
	private Renderer RenderRef;

	private Vector3 OrgCenter;
	private Vector3 OrgSize;

	[SerializeField]
	private string ShowSFX;

	private SoundSystem SoundSystemRef;

	// Use this for initialization
	void Start()
	{
		ColliderRef = GetComponent<BoxCollider>();
		RenderRef = GetComponent<Renderer>();
		SoundSystemRef = GameObject.FindGameObjectWithTag("SoundSystem").GetComponent<SoundSystem>();

		OrgCenter = ColliderRef.center;
		OrgSize = ColliderRef.size;

		RenderRef.enabled = false;
	}

	// Update is called once per frame
	void Update()
	{
		ColliderRef.isTrigger = !RenderRef.enabled;

		for (int i = 0; i < transform.childCount; i++)
			transform.GetChild(i).GetComponent<Renderer>().enabled = RenderRef.enabled;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			if (other.GetComponent<Rigidbody>().velocity.y > 0)
			{
				if (ShowSFX != "")
					SoundSystemRef.PlaySFX(ShowSFX);

				Vector3 VelocityRef = other.GetComponent<Rigidbody>().velocity;
				if (VelocityRef.y > 0)
					VelocityRef.y = -VelocityRef.y * 0.5f;
				other.GetComponent<Rigidbody>().velocity = VelocityRef;

				ColliderRef.size = new Vector3(1, 1, 1);
				ColliderRef.center = Vector3.zero;

				RenderRef.enabled = true;
			}
		}
	}

	public void Reset()
	{
		RenderRef.enabled = false;
		ColliderRef.size = OrgSize;
		ColliderRef.center = OrgCenter;
	}
}
