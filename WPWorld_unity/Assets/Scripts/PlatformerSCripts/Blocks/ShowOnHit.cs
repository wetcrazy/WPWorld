using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowOnHit : MonoBehaviour {

    [Header("ID Settings")]
    public int ID;

	private BoxCollider ColliderRef;
	private Renderer RenderRef;

	private Vector3 OrgCenter;
	private Vector3 OrgSize;

    [Space]
    [Header("Sound Settings")]
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
		if (other.tag == "Player" && other.GetComponent<TPSLogic>().isMine())
		{
			if (other.GetComponent<Rigidbody>().velocity.y > 0)
			{
                //Send event to all players that this block has been unhidden
                object[] content = new object[]
                    {
                        ID
                    };

                ExitGames.Client.Photon.SendOptions sendOptions = new ExitGames.Client.Photon.SendOptions { Reliability = true };
                Photon.Pun.PhotonNetwork.RaiseEvent((byte)EventCodes.EVENT_CODES.PLATFORM_EVENT_BLOCK_HIDDEN, content, Photon.Realtime.RaiseEventOptions.Default, sendOptions);

                //Perform the event locally
                if (ShowSFX != "")
					SoundSystemRef.PlaySFX(ShowSFX);

				Vector3 VelocityRef = other.GetComponent<Rigidbody>().velocity;
				if (VelocityRef.y > 0)
					VelocityRef.y = -VelocityRef.y * 0.5f;
				other.GetComponent<Rigidbody>().velocity = VelocityRef;

                Show();
			}
		}
	}

    public void Show()
    {
        ColliderRef.size = new Vector3(1, 1, 1);
        ColliderRef.center = Vector3.zero;

        RenderRef.enabled = true;
    }

	public void Reset()
	{
		RenderRef.enabled = false;
		ColliderRef.size = OrgSize;
		ColliderRef.center = OrgCenter;
	}
}
