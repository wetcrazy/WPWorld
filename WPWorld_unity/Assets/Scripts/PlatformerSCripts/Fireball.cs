using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour {

    [Header("Movement Settings")]
    [SerializeField]
    private float ForwardForce;
    [SerializeField]
    private float UpwardForce;

    [SerializeField]
    private Vector3 TestVelocity;

    [Header("Collision Settings")]
    [SerializeField]
    private GameObject ExplosionFX;
    [SerializeField]
    private string ExplosionSFX;

    private SoundSystem SoundSystemRef;
    private Rigidbody RigidRef;

	// Use this for initialization
	void Start () {
        RigidRef = GetComponent<Rigidbody>();
        SoundSystemRef = GameObject.FindGameObjectWithTag("SoundSystem").GetComponent<SoundSystem>();

        RigidRef.AddForce(transform.forward * ForwardForce);
    }
	
	// Update is called once per frame
	void Update () {
        TestVelocity = RigidRef.velocity;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Killbox")
        {
            CreateExplosion(transform.position);
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject CollisionRef = collision.gameObject;

        if(CollisionRef.tag == "Player"
            || CollisionRef.name.Contains("Enemy")
            || CollisionRef.GetComponent<Fireball>() != null)
        {
            Destroy(this.gameObject);
            if (CollisionRef.tag == "Player")
                CollisionRef.GetComponent<TPSLogic>().Death();
            if (CollisionRef.name.Contains("Enemy"))
                CollisionRef.GetComponent<Enemy>().AirDeath();
            if (CollisionRef.GetComponent<Fireball>() != null)
                CreateExplosion((transform.position + CollisionRef.transform.position) / 2);
        }
        else
        {
            RigidRef.velocity = Vector3.zero;

            if(CollisionRef.transform.position.y + CollisionRef.transform.lossyScale.y * 0.5f < transform.position.y)
            {
                RigidRef.AddForce(transform.forward * ForwardForce);
                RigidRef.AddForce(transform.up * UpwardForce);
            }
            else
            {
                transform.forward = -transform.forward;
            }
        }
    }

    public void CreateExplosion(Vector3 n_Pos)
    {
        if (ExplosionFX != null)
            Instantiate(ExplosionFX, n_Pos, Quaternion.identity);
        if (ExplosionSFX != "")
            SoundSystemRef.PlaySFX(ExplosionSFX);
    }
}
