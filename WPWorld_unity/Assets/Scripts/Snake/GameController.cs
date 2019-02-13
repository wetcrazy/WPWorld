using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    //Text
    [Header("Canvas Text")]
    public Text ScoreDisplay;
    public Text LifeDisplay;
    public Text WLconditionDisplay;
    public Text MultiplierDisplay;

    [SerializeField]
    private GameObject LevelDesign;

    [SerializeField]
    private GameObject foodprefab = null;
    [SerializeField]
    private GameObject blockprefab = null;

    public GameObject Level;
    //public GameObject Body;

    [SerializeField]
    private int MAX_Food = 1;
    [SerializeField]
    private int MAX_Blocks = 6;

    private int Blockcount = 0;
    private int Foodcount = 0;
    private List<GameObject> arr_Blocks = new List<GameObject>();
    private List<GameObject> arr_BODY = new List<GameObject>();

    private float score = 0;
    Head PlayerHeadComponent;

    private void Start()
    {
        //texts
        WLconditionDisplay.text = "";

        var arr_ChildLvl = LevelDesign.GetComponentsInChildren<Transform>();
        foreach(Transform child in arr_ChildLvl)
        {
            if(child.gameObject.tag == "Blocks")
            {
                arr_Blocks.Add(child.gameObject);
            }
        }
        var arr_body = Level.GetComponentsInChildren<Transform>();
        foreach (Transform child in arr_body)
        {
            if (child.gameObject.tag == "Body" || child.gameObject.tag == "Player")
            {
                arr_BODY.Add(child.gameObject);
            }
        }

        PlayerHeadComponent = Head.LocalPlayerInstance.GetComponent<Head>();
    }

    private void Update()
    {
        var arr_food = GameObject.FindGameObjectsWithTag("Food");

        Foodcount = arr_food.Length;
        FoodSpawner();
        var arr_Speedfood = GameObject.FindGameObjectsWithTag("Speedy");
        if(arr_Speedfood.Length <=0)
        {
            Food_stunSpawner();
        }

        if (PlayerHeadComponent.spawn_block)
        {
            //Vector3 newPosition;
            //int RNG;
            //var newFood = Instantiate(foodprefab, Vector3.zero, Quaternion.identity, transform.parent);
            //RNG = Random.Range(0, arr_Blocks.Count);
            //newPosition = arr_Blocks[RNG].transform.localPosition;

            //newPosition.y += 1;
            //newFood.transform.localPosition = newPosition;
           // var arr_Block = GameObject.FindGameObjectsWithTag("Block");

           // Blockcount = arr_Block.Length;
            BlockSpawner();
            PlayerHeadComponent.spawn_block = false;
        }
    }

    public void FoodSpawner()
    {
        //bool notcolliding;
        int RNG;
        Vector3 newPosition;


        if (Foodcount < MAX_Food)
        {
            
            RNG = Random.Range(0, arr_Blocks.Count);
            newPosition = arr_Blocks[RNG].transform.localPosition;

            newPosition.y += 2;
            var newFood = Instantiate(foodprefab, Vector3.zero, Quaternion.identity, transform.parent);
            newFood.transform.localPosition = newPosition;
        }

    }

    public void BlockSpawner()
    {
        // bool notcolliding;
        int RNG;
        Vector3 newPosition;


        RNG = Random.Range(0, arr_Blocks.Count);
        newPosition = arr_Blocks[RNG].transform.localPosition;

        newPosition.y += 5;

        var newBlock = Instantiate(blockprefab, Vector3.zero, Quaternion.Euler(Vector3.zero),transform.parent);
        newBlock.transform.localPosition = newPosition;

    }

    public void Food_stunSpawner()
    {
        //bool notcolliding;
        int RNG;
        Vector3 newPosition;


        if (score % 100 == 0)
        {

            RNG = Random.Range(0, arr_Blocks.Count);
            newPosition = arr_Blocks[RNG].transform.localPosition;

            newPosition.y += 5;
            var newFood = Instantiate(foodprefab, Vector3.zero, Quaternion.identity, transform.parent);
            newFood.transform.localPosition = newPosition;
        }

    }

    //UI Text Updaters
    public void UpdateMultiplierText(float amount)
    {
        MultiplierDisplay.text = "x" + amount;
    }

    public void UpdateScoreText(float amount)
    {
        ScoreDisplay.text = " Score : " + amount;
        score = amount;
    }

    public void UpdateLivesText(float amount)
    {
        LifeDisplay.text = " Lives : " + amount;
    }

    public void UpdateWLConditionText(string text)
    {
        WLconditionDisplay.text = text;
    }

    //Player Movement
    public void Movement_Up()
    {
        PlayerHeadComponent.Inputup();
    }

    public void Movement_Down()
    {
        PlayerHeadComponent.Inputdown();
    }

    public void Movement_Left()
    {
        PlayerHeadComponent.Inputleft();
    }

    public void Movement_Right()
    {
        PlayerHeadComponent.Inputright();
    }
}

