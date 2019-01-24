using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinFeedbackUI : MonoBehaviour {
    
    [Header("Prefab Settings")]
    [SerializeField]
    private GameObject CoinPrefab;
    private List<GameObject> Coins = new List<GameObject>();

    [Space]
    [Header("Speed Settings")]
    [SerializeField]
    private float MovementSpeed;
    [SerializeField]
    private float ScalingSpeed;

    private RectTransform RectTransformRef;

	// Use this for initialization
	void Start () {
        RectTransformRef = GetComponent<RectTransform>();
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
        transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
    }
}
