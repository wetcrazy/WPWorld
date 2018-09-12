using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFinal : MonoBehaviour {
    
    [SerializeField]
    float PlayerSpeed = 70.0f;

    SceneControlFinal SceneControllerScript = null;
    //bool isFalling = true;

	// Use this for initialization
	void Start () {
        SceneControllerScript = GameObject.Find("Scripts").GetComponent<SceneControlFinal>();
	}
	
	// Update is called once per frame
	void Update () {
        
        PlayerFall();
        
        KeyInput();
        
    }

    void KeyInput()
    {
        //NOTE: These are temporary controls for debugging purposes
        if(Input.GetKey(KeyCode.W))
        {
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
        //Adds a constant force that pushes the player towards the center of the planet
        GetComponent<Rigidbody>().AddForce((SceneControllerScript.PlanetObject.transform.position - gameObject.transform.position).normalized * SceneControllerScript.GRAVITY);
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

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Asteroid")
        {
            //Removes the asteroid
            Destroy(other.gameObject);
        }
    }
}
