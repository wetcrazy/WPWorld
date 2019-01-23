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

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        foreach(GameObject n_Coin in Coins)
        {
            n_Coin.transform.localPosition = Vector3.Lerp(Vector3.zero, transform.localPosition, MovementSpeed * Time.deltaTime);
        }
	}

    public void CreateCoin(Vector3 n_Pos)
    {
        Coins.Add(Instantiate(CoinPrefab, n_Pos, Quaternion.identity, transform));
    }
}
