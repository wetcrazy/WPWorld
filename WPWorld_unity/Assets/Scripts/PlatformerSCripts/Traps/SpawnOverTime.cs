using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOverTime : MonoBehaviour {

    [Header("Spawn Settings")]
    [SerializeField]
    private GameObject ItemToSpawn;

    [Header("Animation Settings")]
    [SerializeField]
    private GameObject Arms;
    [SerializeField]
    private Vector3 AnimationStopVector;
    private Vector3 AnimOrgVector;
    [SerializeField]
    private float AnimationSpeed;
    private bool Anim_Returning = false;

    [SerializeField]
    private float TimeToComplete = 0;

	// Use this for initialization
	void Start () {
        AnimOrgVector = Arms.transform.localEulerAngles;
	}
	
	// Update is called once per frame
	void Update () {
        if(Arms != null)
        {
            if(!Anim_Returning)
            {
                if (Vector3.Distance(Arms.transform.localEulerAngles, AnimationStopVector) <= 0.01f)
                {
                    Anim_Returning = !Anim_Returning;
                }
                else
                {
                    Arms.transform.localRotation = Quaternion.RotateTowards(Quaternion.Euler(Arms.transform.localEulerAngles),
                        Quaternion.Euler(AnimationStopVector),
                        AnimationSpeed * Time.deltaTime);

                    Debug.Log(Vector3.Distance(Arms.transform.localEulerAngles, AnimationStopVector));
                }
                TimeToComplete += Time.deltaTime;
            }
            else
            {
                if (Vector3.Distance(Arms.transform.localEulerAngles, AnimOrgVector) <= 0.01f)
                {
                    Anim_Returning = !Anim_Returning;
                    Instantiate(ItemToSpawn, transform.position + Vector3.Scale(transform.up, transform.localScale), Quaternion.identity);
                }
                else
                {
                    Arms.transform.localRotation = Quaternion.RotateTowards(Quaternion.Euler(Arms.transform.localEulerAngles),
                        Quaternion.Euler(AnimOrgVector),
                        AnimationSpeed * Time.deltaTime);
                }
            }
        }
	}
}
