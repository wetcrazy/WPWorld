using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinFeedbackUI : MonoBehaviour {

    [SerializeField]
    private float ScalingSpeed;

    [SerializeField]
    private Vector3 PulseSize;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if(Vector3.Distance(transform.localScale, new Vector3(1,1,1)) < 0.1f)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, new Vector3(1, 1, 1), ScalingSpeed * Time.deltaTime);
        }

        if(Input.GetKeyDown(KeyCode.A))
        {
            Pulse();
        }
	}

    public void Pulse()
    {
        transform.localScale = PulseSize;
    }
}
