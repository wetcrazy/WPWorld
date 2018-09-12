using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneControlFinal : MonoBehaviour {

    [SerializeField]
    public GameObject PlanetObject = null;
    [SerializeField]
    public float GRAVITY = 10;
    [SerializeField]
    int MaximumAsteroidsInScene = 10;
    [SerializeField]
    float MinimumAsteroidDistanceToPlanet;
    [SerializeField]
    float MaximumAsteroidDistanceToPlanet;

    int TimerMinute = 0, TimerSecond = 0;

    List<GameObject> AsteroidList = new List<GameObject>();

	// Use this for initialization
	void Start () {
        string StartingTime = GameObject.Find("Timer").GetComponent<Text>().text;
        TimerMinute = int.Parse(StartingTime.Substring(0, 2));
        TimerSecond = int.Parse(StartingTime.Substring(2));
	}
	
	// Update is called once per frame
	void Update ()
    {
        //If there are fewer asteroids active in scene than specified, spawn an asteroid
        while (AsteroidList.Count < MaximumAsteroidsInScene)
        {
            SpawnAsteroid();
        }

        foreach (GameObject asteroid in AsteroidList)
        {
            //Removes any null objects
            if(!asteroid)
            {
                AsteroidList.Remove(asteroid);
                continue;
            }

            //Move the asteroid towards the planet
            asteroid.transform.position += (PlanetObject.transform.position - asteroid.transform.position).normalized * asteroid.GetComponent<AsteroidScript>().AsteroidSpeed * Time.deltaTime;
        }
	}

    void SpawnAsteroid()
    {
        //Spawn the asteroid in a random pos within the specified min/max distance
        GameObject AsteroidObject = (GameObject)Instantiate(Resources.Load("Asteroid_Run/Asteroid"),
            new Vector3(Random.Range(-MaximumAsteroidDistanceToPlanet, MaximumAsteroidDistanceToPlanet), Random.Range(PlanetObject.transform.position.y - MaximumAsteroidDistanceToPlanet, PlanetObject.transform.position.y + MaximumAsteroidDistanceToPlanet), Random.Range(-MaximumAsteroidDistanceToPlanet, MaximumAsteroidDistanceToPlanet)),
            transform.rotation);

        //Add to list of asteroids
        AsteroidList.Add(AsteroidObject);

        //Debugging stuff, don't touch
        float dist = Vector3.Distance(PlanetObject.transform.position, AsteroidObject.transform.position);
        if (dist > MaximumAsteroidDistanceToPlanet)
        {
            Debug.Log("Asteroid is over specified max distance!");
        }
        else if (dist < MinimumAsteroidDistanceToPlanet)
        {
            Debug.Log("Asteroid is under specified min distance!");
        }
    }
}
