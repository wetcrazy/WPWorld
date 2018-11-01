using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPopup : MonoBehaviour {

    [SerializeField]
    private string EnterSFX;

    private SoundSystem SoundSystemRef;

    private void Awake()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }

    // Use this for initialization
    void Start () {
        SoundSystemRef = GameObject.FindGameObjectWithTag("SoundSystem").GetComponent<SoundSystem>();
    }
	
	// Update is called once per frame
	void Update () {
        transform.GetChild(0).LookAt(2 * transform.position - Camera.main.transform.position);
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            transform.GetChild(0).gameObject.SetActive(true);
            if (EnterSFX != "")
                SoundSystemRef.PlaySFX(EnterSFX);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
