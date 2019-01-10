using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private GameObject LevelDesign;

    [SerializeField]
    private GameObject foodprefab = null;

    [SerializeField]
    private int MAX_Food = 1;

    private int Foodcount = 0;
    private List<GameObject> arr_Blocks = new List<GameObject>();

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
    }

    private void Update()
    {
        var arr_food = GameObject.FindGameObjectsWithTag("Food");

        Foodcount = arr_food.Length;

        FoodSpawner();
    }

    public void FoodSpawner()
    {
        if(Foodcount < MAX_Food)
        {
            var RNG = Random.Range(0, arr_Blocks.Count);
            var newPosition = arr_Blocks[RNG].transform.position;
            newPosition.y += arr_Blocks[RNG].transform.lossyScale.y;
            var newFood = Instantiate(foodprefab, newPosition, Quaternion.identity, transform.parent);
        }
    }
}
