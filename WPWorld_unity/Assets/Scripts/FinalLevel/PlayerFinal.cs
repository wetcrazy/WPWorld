using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFinal : MonoBehaviour {

    [SerializeField]
    GameObject PlanetObject = null;
    [SerializeField]
    GameObject PlanetPivot = null;
    [SerializeField]
    float GRAVITY = 9.8f;
    [SerializeField]
    float PlayerSpeed = 70.0f;
    
    bool isFalling = true;
    Vector3 PlayerForward, PlayerRight;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        //if (isFalling)
        //{
            PlayerFall();
        //}
        
        KeyInput();
        
    }

    void KeyInput()
    {
        //NOTE: These are temporary controls for debugging purposes

        if(Input.GetKey(KeyCode.W))
        {
            //gameObject.transform.RotateAround(PlanetObject.transform.position, PlanetObject.transform.right, PlayerSpeed * Time.deltaTime);
            gameObject.transform.position += gameObject.transform.forward * PlayerSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            gameObject.transform.position -= gameObject.transform.forward * PlayerSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            gameObject.transform.position -= gameObject.transform.right * PlayerSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            gameObject.transform.position += gameObject.transform.right * PlayerSpeed * Time.deltaTime;
        }
    }

    void PlayerFall()
    {
        ////Point the player towards the center of the planet
        //gameObject.transform.up = (gameObject.transform.position - PlanetObject.transform.position).normalized;

        ////Move the player towards the center of planet (gravity)
        //gameObject.transform.position -= (gameObject.transform.up * GRAVITY * Time.deltaTime);

        GetComponent<Rigidbody>().AddForce((PlanetObject.transform.position - gameObject.transform.position).normalized * GRAVITY);
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    Debug.Log("Collision Enter");
    //    if (collision.gameObject == PlanetObject)
    //    {
    //        isFalling = false;
    //        Debug.Log("Player Has Stopped Falling");
    //    }
    //}

    //private void OnCollisionExit(Collision collision)
    //{
    //    Debug.Log("Collision Exit");
    //    if (collision.gameObject == PlanetObject)
    //    {
    //        isFalling = true;
    //        Debug.Log("Player Has Started Falling");
    //    }
    //}
}
