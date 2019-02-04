using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private GameObject LevelDesign;

    [SerializeField]
    private GameObject foodprefab = null;

    public GameObject Level;
    //public GameObject Body;

    [SerializeField]
    private int MAX_Food = 1;

    private int Foodcount = 0;
    private List<GameObject> arr_Blocks = new List<GameObject>();
    private List<GameObject> arr_BODY = new List<GameObject>();

    private void Start()
    {
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
    }

    private void Update()
    {
        var arr_food = GameObject.FindGameObjectsWithTag("Food");

        Foodcount = arr_food.Length;

        FoodSpawner();
    }

    public void FoodSpawner()
    {
        bool notcolliding;
        int RNG;
        Vector3 newPosition;

        if (Foodcount < MAX_Food)
        {
            do
            {
                notcolliding = false;
                RNG = Random.Range(0, arr_Blocks.Count);
                newPosition = arr_Blocks[RNG].transform.position;

                newPosition.y += arr_Blocks[RNG].transform.lossyScale.y;
                //for(int i =0;i<arr_BODY.Count;i++)
                //{
                //    if (
                //    (newPosition.x >= arr_BODY[i].transform.lossyScale.x - 1) &&
                //    (newPosition.x <= arr_BODY[i].transform.lossyScale.x + 1) &&
                //    (newPosition.z >= arr_BODY[i].transform.lossyScale.z - 1) &&
                //    (newPosition.z <= arr_BODY[i].transform.lossyScale.z + 1))
                //    {
                //        notcolliding = true;
                //    }
                //}
            }
            while (notcolliding);
            var newFood = Instantiate(foodprefab, newPosition, Quaternion.identity, transform.parent);
        }
    }
}
