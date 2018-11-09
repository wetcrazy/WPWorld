using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceOnCollide : MonoBehaviour {

    private Rigidbody RigidRef;
    private Vector3 OrgPos;

    [SerializeField]
    private string BounceSFX;

    private SoundSystem SoundSystemRef;

    private float TimeElapsed;

	// Use this for initialization
	void Start () {
        RigidRef = GetComponent<Rigidbody>();

        OrgPos = this.transform.position;

        SoundSystemRef = GameObject.FindGameObjectWithTag("SoundSystem").GetComponent<SoundSystem>();
    }
	
	// Update is called once per frame
	void Update () {
        if(RigidRef.useGravity)
        {
            TimeElapsed += Time.deltaTime;

            if(TimeElapsed >= 0.1f)
            {
                if (Vector3.Distance(this.transform.position, OrgPos) < 0.01f || this.transform.position.y < OrgPos.y)
                {
                    RigidRef.constraints = RigidbodyConstraints.FreezeAll;
                    RigidRef.useGravity = false;

                    this.transform.position = OrgPos;
                    TimeElapsed = 0;
                }
            }
        }

        Debug.DrawRay(transform.position, transform.up.normalized * transform.lossyScale.y, Color.green);

        Debug.DrawRay(transform.position, (transform.up - transform.right * 0.5f).normalized * transform.lossyScale.y, Color.red);
        Debug.DrawRay(transform.position, (transform.up + transform.right * 0.5f).normalized * transform.lossyScale.y, Color.blue);

        Debug.DrawRay(transform.position, (transform.up - transform.forward * 0.5f).normalized * transform.lossyScale.y, Color.red);
        Debug.DrawRay(transform.position, (transform.up + transform.forward * 0.5f).normalized * transform.lossyScale.y, Color.blue);
    }

    private void OnCollisionStay(Collision collision)
    {
        GameObject CollidedObject = collision.gameObject;

        if (CollidedObject.tag == "Player")
        {
            if (!CollidedObject.GetComponent<TPSLogic>().GetGrounded() // Grounded Check
                && CollidedObject.transform.localPosition.y + CollidedObject.transform.localScale.y * 0.5f <= transform.localPosition.y - transform.localScale.y * 0.5f // Check if the bottom of the gameobject is colliding with the top of the player
                && Mathf.Abs(CollidedObject.transform.localPosition.x - transform.localPosition.x) < transform.localScale.x * 0.5f // Check if the player is within a certain x range to trigger
                && Mathf.Abs(CollidedObject.transform.localPosition.z - transform.localPosition.z) < transform.localScale.z * 0.5f // Check if the player is within a certain z range to trigger
                && CollidedObject.GetComponent<Rigidbody>().velocity.y > 0 // Check if the player is jumping and not falling
                )
            {
                if (Vector3.Distance(this.transform.position, OrgPos) < 0.05f && TimeElapsed == 0)
                {
                    // Releases the constraints of the rigidbody and pushes the force upwards
                    RigidRef.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
                    RigidRef.AddForce(0, 25 * transform.parent.parent.lossyScale.y, 0);
                    RigidRef.useGravity = true;
                    
                    // Plays the sound if it has been issued one
                    if (BounceSFX != "")
                        SoundSystemRef.PlaySFX(BounceSFX);

                    // Bounce back effect
                    Vector3 VelocityRef = CollidedObject.GetComponent<Rigidbody>().velocity;
                    if (VelocityRef.y > 0)
                        VelocityRef.y = -VelocityRef.y * 0.5f;
                    CollidedObject.GetComponent<Rigidbody>().velocity = VelocityRef;

                    // Checks if there is an enemy to kill on top of the block
                    RaycastHit hit;
                    if(Physics.Raycast(transform.position, transform.up, out hit, transform.localScale.y)
                        || Physics.Raycast(transform.position, transform.up + transform.forward * 0.5f, out hit, transform.localScale.y)
                        || Physics.Raycast(transform.position, transform.up - transform.forward * 0.5f, out hit, transform.localScale.y)
                        || Physics.Raycast(transform.position, transform.up + transform.right * 0.5f, out hit, transform.localScale.y)
                        || Physics.Raycast(transform.position, transform.up - transform.right * 0.5f, out hit, transform.localScale.y))
                        // Kill Enemy if there is an enemy on top of the bounce block
                        if(hit.transform.name.Contains("Enemy"))
                        {
                            hit.transform.GetComponent<Enemy>().AirborneDeath();
                            hit.transform.GetComponent<Rigidbody>().AddForce(0, 50 * transform.parent.parent.lossyScale.y, 0);
                        }
                }
            }
        }
    }
}
