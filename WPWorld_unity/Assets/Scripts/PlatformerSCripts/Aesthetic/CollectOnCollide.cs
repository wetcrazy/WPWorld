using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectOnCollide : MonoBehaviour {

    private Renderer RenderRef;
    [SerializeField]
    private string CollectSFX;

    [SerializeField]
    private int PointsToAdd;

    public bool HasCollected;

    private SoundSystem SoundSystemRef;

	// Use this for initialization
	void Start () {
        RenderRef = GetComponent<Renderer>();
        SoundSystemRef = GameObject.FindGameObjectWithTag("SoundSystem").GetComponent<SoundSystem>();

        HasCollected = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (!RenderRef.isVisible)
            return;
        transform.Rotate(new Vector3(0, 5, 0));
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(RenderRef.isVisible)
            {
                RenderRef.enabled = false;
                other.GetComponent<TPSLogic>().CurrPointsPub += PointsToAdd;

                HasCollected = true;

                if (CollectSFX != "")
                    SoundSystemRef.PlaySFX(CollectSFX);
            }
        }
    }

    public void Reset()
    {
        RenderRef.enabled = true;
    }
}
