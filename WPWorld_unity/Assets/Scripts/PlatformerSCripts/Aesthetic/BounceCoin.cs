using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceCoin : MonoBehaviour {

    private Rigidbody RigidRef;

    private Vector3 OrgPos;

    [SerializeField]
    private float UpwardForce;

    [SerializeField]
    private GameObject ParticleFX;

    [SerializeField]
    private GameObject ScorePopup;

    private float TimeElapsed;

	// Use this for initialization
	void Start () {
        RigidRef = GetComponent<Rigidbody>();
        OrgPos = transform.position;

        RigidRef.AddForce(transform.up * UpwardForce, ForceMode.VelocityChange);

	}
	
	// Update is called once per frame
	void Update () {
        TimeElapsed += Time.deltaTime;

        transform.Rotate(new Vector3(0, 5, 0));

        if (TimeElapsed > 0.1f)
        {
            if(Vector3.Distance(OrgPos, transform.position) < 0.01f || OrgPos.y > transform.position.y)
            {
                GameObject n_Particle = Instantiate(ParticleFX, transform);
                n_Particle.transform.parent = null;
                n_Particle.transform.localScale = new Vector3(1, 1, 1);
                n_Particle.transform.position = this.transform.position;

                GameObject n_Score = Instantiate(ScorePopup, transform);
                n_Score.transform.parent = null;
                n_Score.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                n_Score.transform.position = this.transform.position;

                Destroy(this.gameObject);
            }
        }
	}
}
