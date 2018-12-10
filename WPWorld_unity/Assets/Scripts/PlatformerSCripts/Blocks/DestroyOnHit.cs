using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnHit : MonoBehaviour {

	[SerializeField]
	private int AmountOfDebris;

	[SerializeField]
	private GameObject Debris;

	[SerializeField]
	private string DestroySFX;

	private Renderer RenderRef;
	private SoundSystem SoundSystemRef;

	// Use this for initialization
	void Start()
	{
		RenderRef = GetComponent<Renderer>();

		SoundSystemRef = GameObject.FindGameObjectWithTag("SoundSystem").GetComponent<SoundSystem>();
	}

	// Update is called once per frame
	void Update()
	{
		if (!RenderRef.isVisible)
		{
			GetComponent<Collider>().enabled = false;
		}
		else
		{
			GetComponent<Collider>().enabled = true;
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (!RenderRef.isVisible)
			return;

		if(other.tag == "Player")
		{
			if(!other.GetComponent<TPSLogic>().GetGrounded()
				&& other.GetComponent<Rigidbody>().velocity.y >= 0
				)
			{
				RenderRef.enabled = false;

				if (DestroySFX != "")
					SoundSystemRef.PlaySFX(DestroySFX);

				for (int i = 0; i < AmountOfDebris; i++)
				{
					GameObject n_Debris = Instantiate(Debris, this.transform);
					Rigidbody RigidRef = n_Debris.GetComponent<Rigidbody>();
					RigidRef.AddForce(new Vector3(Random.Range(-50, 50) * transform.parent.parent.lossyScale.x,
						Random.Range(25, 50) * transform.parent.parent.lossyScale.y,
						Random.Range(-50, 50) * transform.parent.parent.lossyScale.z));
				}

				Vector3 VelocityRef = other.GetComponent<Rigidbody>().velocity;
				if (VelocityRef.y > 0)
					VelocityRef.y = -VelocityRef.y * 0.5f;
				other.GetComponent<Rigidbody>().velocity = VelocityRef;

				//if (hit.transform.name.Contains("Enemy"))
				//{
				//	hit.transform.GetComponent<Enemy>().AirborneDeath();
				//	hit.transform.GetComponent<Rigidbody>().AddForce(0, 50 * transform.parent.parent.lossyScale.y, 0);
				//}
			}
		}
	}

	public void Reset()
	{
		RenderRef.enabled = true;
	}
}
