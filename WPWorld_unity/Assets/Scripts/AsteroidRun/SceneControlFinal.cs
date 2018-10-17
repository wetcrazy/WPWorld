using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneControlFinal : MonoBehaviour {

    [SerializeField]
    public GameObject PlanetObject = null;
    [SerializeField]
    GameObject HealthPowerupObject = null;
    [SerializeField]
    float HealthPowerupSpawnDuration = 5;
    [SerializeField]
    public float GRAVITY = 10;
    [SerializeField]
    int MaximumAsteroidsInScene = 10;
    [SerializeField]
    float MinimumAsteroidDistanceToPlanet;
    [SerializeField]
    float MaximumAsteroidDistanceToPlanet;
    [SerializeField]
    int NumOfObstacles = 4;
    [SerializeField]
    int MaxNumOfObstacles = 6;
    [SerializeField]
    GameObject CanvasTimer = null;

    int TimerMinute = 0, TimerSecond = 0;
    float SecondTimer = 0;
    float HealthPowerupSpawnTimer;
    bool TimerIsCountingDown = true;

    List<GameObject> AsteroidList = new List<GameObject>();
    List<GameObject> ObstacleList = new List<GameObject>();

	// Use this for initialization
	void Start () {
        string StartingTime = CanvasTimer.GetComponent<Text>().text;
        TimerMinute = int.Parse(StartingTime.Substring(0, 2));
        TimerSecond = int.Parse(StartingTime.Substring(3));

        HealthPowerupSpawnTimer = HealthPowerupSpawnDuration;
        SpawnHealthPowerup();

        for (int i = NumOfObstacles; i > 0; --i)
        {
            SpawnObstacle();    
        }
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

        if(!HealthPowerupObject.activeSelf)
        {
            //Countdown the spawn timer
            HealthPowerupSpawnTimer -= Time.deltaTime;

            if(HealthPowerupSpawnTimer <= 0)
            {
                //Spawn the powerup after timer is up
                SpawnHealthPowerup();
                //Reset the timer
                HealthPowerupSpawnTimer = HealthPowerupSpawnDuration;
            }
        }
	}

    private void FixedUpdate()
    {
        foreach (GameObject obstacle in ObstacleList)
        {
            if (!obstacle.activeSelf)
            {
                continue;
            }

            obstacle.GetComponent<ObstacleScript>().ObstacleUpdate(PlanetObject);
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

                    if (NumOfObstacles < MaxNumOfObstacles)
                    {
                        //Spawn an obstacle
                        SpawnObstacle();
                        ++NumOfObstacles;
                    }

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

                    if (NumOfObstacles < MaxNumOfObstacles)
                    {
                        //Spawn an obstacle
                        SpawnObstacle();
                        ++NumOfObstacles;
                    }

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

    void SpawnObstacle()
    {
        GameObject ObstacleObj = Instantiate(Resources.Load<GameObject>("Asteroid_Run/Obstacle"), gameObject.transform.parent);
        ObstacleObj.GetComponent<ObstacleScript>().ObstacleInit(PlanetObject);

        //Assign a random pos on planet to the obstacle
        ObstacleObj.transform.position = (Random.onUnitSphere * 0.1f) + PlanetObject.transform.position;

        ObstacleList.Add(ObstacleObj);
    }

    void SpawnHealthPowerup()
    {
        //Set powerup to active
        HealthPowerupObject.SetActive(true);

        //Assign a random pos on planet to the health powerup
        HealthPowerupObject.transform.position = (Random.onUnitSphere * 0.4f) + PlanetObject.transform.position;
    }

    void SpawnAsteroid()
    {
        //Spawn the asteroid in a random pos within the specified min/max distance
        GameObject AsteroidObject = Instantiate(Resources.Load<GameObject>("Asteroid_Run/Asteroid"),
            new Vector3(Random.Range(-MaximumAsteroidDistanceToPlanet, MaximumAsteroidDistanceToPlanet), Random.Range(PlanetObject.transform.position.y - MaximumAsteroidDistanceToPlanet, PlanetObject.transform.position.y + MaximumAsteroidDistanceToPlanet), Random.Range(-MaximumAsteroidDistanceToPlanet, MaximumAsteroidDistanceToPlanet)),
            transform.rotation,
            gameObject.transform.parent);

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
