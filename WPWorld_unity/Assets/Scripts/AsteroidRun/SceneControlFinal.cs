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
    [SerializeField]
    GameObject CanvasTimer;

    int TimerMinute = 0, TimerSecond = 0;
    float SecondTimer = 0;
    bool TimerIsCountingDown = true;

    List<GameObject> AsteroidList = new List<GameObject>();

	// Use this for initialization
	void Start () {
        string StartingTime = CanvasTimer.GetComponent<Text>().text;
        TimerMinute = int.Parse(StartingTime.Substring(0, 2));
        TimerSecond = int.Parse(StartingTime.Substring(3));
	}
	
	// Update is called once per frame
	void Update ()
    {
        UpdateCanvasTimer();

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

    void UpdateCanvasTimer()
    {
        if (TimerSecond == 0 && TimerMinute == 0 && TimerIsCountingDown)
        {
            //Timer has reached 0
            TimerIsCountingDown = false;
        }

        //Updates the timer on the UI canvas
        SecondTimer += Time.deltaTime;

        //If 1 second has passed
        if (SecondTimer >= 1)
        {
            string temptime = CanvasTimer.GetComponent<Text>().text;

            if (TimerIsCountingDown)
            {
                if (TimerSecond == 0)
                {
                    //If the the second value reaches 0, count from 59 and reduce the minute value by 1
                    TimerSecond = 59;
                    --TimerMinute;

                    //Update the new minute value in temptime
                    if (TimerMinute <= 9)
                    {
                        temptime = '0' + TimerMinute.ToString() + temptime.Substring(2);
                    }
                    else
                    {
                        temptime = TimerMinute.ToString() + temptime.Substring(2);
                    }
                }
                else
                {
                    //Minus 1 second in Second value
                    --TimerSecond;
                }
            }
            else
            {
                if(TimerSecond >= 59)
                {
                    TimerSecond = 0;
                    ++TimerMinute;

                    //Update the new minute value in temptime
                    if (TimerMinute <= 9)
                    {
                        temptime = '0' + TimerMinute.ToString() + temptime.Substring(2);
                    }
                    else
                    {
                        temptime = TimerMinute.ToString() + temptime.Substring(2);
                    }
                }
                else
                {
                    ++TimerSecond;
                }
            }

            //Update the seconds value in temptime
            if (TimerSecond <= 9)
            {
                temptime = temptime.Substring(0, 3) + '0' + TimerSecond.ToString();
            }
            else
            {
                temptime = temptime.Substring(0, 3) + TimerSecond.ToString();
            }

            //Assign the new time to the CanvasTimer
            CanvasTimer.GetComponent<Text>().text = temptime;
            //Reset the secondtimer
            SecondTimer = 0;
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
