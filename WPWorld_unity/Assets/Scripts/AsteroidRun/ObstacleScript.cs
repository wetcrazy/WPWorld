using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour {

    [SerializeField]
    float ObstacleRiseSpeed = 100;

    bool isRising = true;
    SceneControlFinal SceneControllerScript = null;

    public void ObstacleInit(GameObject PlanetObject)
    {
        SceneControllerScript = GameObject.Find("Scripts").GetComponent<SceneControlFinal>();
    }

    public void ObstacleUpdate(GameObject PlanetObject)
    {
        if(isRising)
        {
            //transform.position += transform.up * Time.deltaTime;
            GetComponent<Rigidbody>().AddForce(transform.up * Time.deltaTime * ObstacleRiseSpeed);
            gameObject.transform.up = (gameObject.transform.position - PlanetObject.transform.position).normalized;
        }
        else
        {
            //gameObject.transform.up = (gameObject.transform.position - PlanetObject.transform.position).normalized;
            GetComponent<Rigidbody>().AddForce((PlanetObject.transform.position - gameObject.transform.position).normalized * SceneControllerScript.GRAVITY * 10);
            gameObject.transform.RotateAround(PlanetObject.transform.position, gameObject.transform.right, Time.deltaTime * 20);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Obstacle")
        {
            Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), collision.gameObject.GetComponent<Collider>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Planet")
        {
            gameObject.GetComponent<Collider>().isTrigger = false;
            isRising = false;
        }
    }
}
