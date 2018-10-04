using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnFloor : MonoBehaviour {

    public GameObject blockgameobject;
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
    void Start () {
        offset = 0.1f;
        //locator = new Vector3(XOffset,YOffset,ZOffset);
        //rotaterlocater = new Quaternion(xrot, yrot, zrot, wrot);

        for(int i=0;i<mapsize;i++)
        {
            for(int j = 0; j < mapsize; j++)
            {
                 Instantiate(blockgameobject, new Vector3(gameObject.transform.position.x +i*offset, gameObject.transform.position.y , gameObject.transform.position.z + j * offset), gameObject.transform.rotation);

            }
        }
        

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
