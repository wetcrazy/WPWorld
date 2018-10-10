using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnFloor : MonoBehaviour {

    public GameObject blockgameobject;
    public GameObject blockboundary;
    public int mapsize;

    private float offset;

    private float XOffset;
    private float YOffset;
    private float ZOffset;

    private float xrot;
    private float yrot;
    private float zrot;
    private float wrot;

    private Vector3 locator;
    private Quaternion rotaterlocater;
    // Use this for initialization
    private int height;
    void Start () {
        offset = 0.1f;
        height = 20;
        //locator = new Vector3(XOffset,YOffset,ZOffset);
        //rotaterlocater = new Quaternion(xrot, yrot, zrot, wrot);

        for (int i = 0; i < mapsize; i++)
        {
            for (int j = 0; j < mapsize; j++)
            {
                Instantiate(blockgameobject, new Vector3(gameObject.transform.position.x + i * offset, gameObject.transform.position.y, gameObject.transform.position.z + j * offset), gameObject.transform.rotation);

            }
        }

        //Vector3 centermappos = new Vector3(offset, gameObject.transform.position.y,offset);
        //GameObject newobject = Instantiate(blockgameobject, centermappos, gameObject.transform.rotation);
        //newobject.transform.localScale = new Vector3(mapsize*offset, gameObject.transform.localScale.y*offset, mapsize*offset);

        //newobject.GetComponent(M) = new Material
        //boundary

        //for (int x = 0; x < height; x++)
        //{
        //    for (int y = 0; y < height; y++)
        //    {
        //        Instantiate(blockboundary, new Vector3(gameObject.transform.position.x +x * offset, gameObject.transform.position.y+y*offset, gameObject.transform.position.z + (-1*offset)), gameObject.transform.rotation);

        //    }
        //}
        //for (int x = 0; x < height; x++)
        //{
        //    for (int y = 0; y < height; y++)
        //    {
        //        Instantiate(blockboundary, new Vector3(gameObject.transform.position.x + (-1 * offset), gameObject.transform.position.y + y * offset, gameObject.transform.position.z + x * offset), gameObject.transform.rotation);

        //    }
        //}
        //for (int x = 0; x < height; x++)
        //{
        //    for (int y = 0; y < height; y++)
        //    {
        //        Instantiate(blockboundary, new Vector3(gameObject.transform.position.x + (mapsize  * offset), gameObject.transform.position.y + y * offset, gameObject.transform.position.z + x * offset), gameObject.transform.rotation);

        //    }
        //}
        //for (int x = 0; x < height; x++)
        //{
        //    for (int y = 0; y < height; y++)
        //    {
        //        Instantiate(blockboundary, new Vector3(gameObject.transform.position.x + x * offset, gameObject.transform.position.y + y * offset, gameObject.transform.position.z + (mapsize * offset)), gameObject.transform.rotation);

        //    }
        //}

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
