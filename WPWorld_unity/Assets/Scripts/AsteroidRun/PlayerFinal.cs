using UnityEngine;
using UnityEngine.UI;

public class PlayerFinal : MonoBehaviour {
    
    [SerializeField]
    float PlayerSpeed = 70.0f;
    [SerializeField]
    Image PlayerHealthBar;
    [SerializeField]
    float CurrentHealth = 100;
    [SerializeField]
    float MaximumHealth = 100;
    [SerializeField]
    float PlayerLoseHealthSpeed = 5;

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

        //Make player health gradually fall
        if (CurrentHealth > 0)
        {
            CurrentHealth -= PlayerLoseHealthSpeed * Time.deltaTime;
            PlayerHealthBar.rectTransform.localScale = new Vector3(CurrentHealth / MaximumHealth, 1, 1);
        }
        else if (CurrentHealth < 0)
        {
            CurrentHealth = 0;
            PlayerHealthBar.rectTransform.localScale = new Vector3(0, 1, 1);
        }
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
