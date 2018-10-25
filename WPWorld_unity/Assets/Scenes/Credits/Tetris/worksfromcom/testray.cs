using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testray : MonoBehaviour
{

    void Update()
    {
        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << 8;

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        layerMask = ~layerMask;

        RaycastHit hit;
        //if (Physics.Raycast(transform.position,transform.TransformDirection(Vector3.down),out hit, Mathf.Infinity,layerMask ))
        //{

        //}
        if (Physics.Raycast(transform.position, Vector3.down, out hit))
            //       print("Found an object - distance: " + hit.distance);
            // Does the ray intersect any objects excluding the player layer
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity, layerMask))
            {
                Debug.DrawRay(transform.position + new Vector3(1, 0, 1) * 0.1f, transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
                Debug.DrawRay(transform.position + new Vector3(0, 0, 1) * 0.1f, transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
                Debug.DrawRay(transform.position + new Vector3(-1, 0, 1) * 0.1f, transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
                Debug.DrawRay(transform.position + new Vector3(1, 0, 0) * 0.1f, transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
                Debug.DrawRay(transform.position + new Vector3(0, 0, 0) * 0.1f, transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
                Debug.DrawRay(transform.position + new Vector3(-1, 0, 0) * 0.1f, transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
                Debug.DrawRay(transform.position + new Vector3(1, 0, -1) * 0.1f, transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
                Debug.DrawRay(transform.position + new Vector3(0, 0, -1) * 0.1f, transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
                Debug.DrawRay(transform.position + new Vector3(-1, 0, -1) * 0.1f, transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
                //Debug.Log("Did Hit");
                //Debug.Log(Physics.Raycast)
            }
            else
            {
                //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * 1000, Color.white);
                //  Debug.Log("Did not Hit"); Debug.DrawRay(transform.position + new Vector3(1,0,1 ) * 0.1f, transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
                Debug.DrawRay(transform.position + new Vector3(0, 0, 1) * 0.1f, transform.TransformDirection(Vector3.down) * 1000, Color.white);
                Debug.DrawRay(transform.position + new Vector3(-1, 0, 1) * 0.1f, transform.TransformDirection(Vector3.down) * 1000, Color.white);
                Debug.DrawRay(transform.position + new Vector3(1, 0, 0) * 0.1f, transform.TransformDirection(Vector3.down) * 1000, Color.white);
                Debug.DrawRay(transform.position + new Vector3(0, 0, 0) * 0.1f, transform.TransformDirection(Vector3.down) * 1000, Color.white);
                Debug.DrawRay(transform.position + new Vector3(-1, 0, 0) * 0.1f, transform.TransformDirection(Vector3.down) * 1000, Color.white);
                Debug.DrawRay(transform.position + new Vector3(1, 0, -1) * 0.1f, transform.TransformDirection(Vector3.down) * 1000, Color.white);
                Debug.DrawRay(transform.position + new Vector3(0, 0, -1) * 0.1f, transform.TransformDirection(Vector3.down) * 1000, Color.white);
                Debug.DrawRay(transform.position + new Vector3(-1, 0, -1) * 0.1f, transform.TransformDirection(Vector3.down) * 1000, Color.white);
            }
    }
}
