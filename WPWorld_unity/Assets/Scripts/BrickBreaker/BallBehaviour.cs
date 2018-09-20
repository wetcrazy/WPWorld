using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the ball's behaviour
/// </summary>
public class BallBehaviour : MonoBehaviour
{
    // Brick Prefab
    public GameObject BrickPrefab; 
    public float speed, MAX_timer;

    private float curr_timer;
    private Rigidbody rb;
    private Vector3 gravity;
    private float y;

    private void Awake()
    {     
        rb = GetComponent<Rigidbody>();
        gravity = -Vector3.up;
    }

    private void Update()
    {
        // Y checking (prevents the ball from staying in place)
        if (y == transform.position.y)
        {
            gravity = -gravity;
        }
        else
        {
            y = transform.position.y;
        }

        // Update the velocity vector up 
        rb.AddForce(gravity * speed * Time.deltaTime);

        // X and Z randomizer 
        if(curr_timer> MAX_timer)
        {
            var _rand = Random.Range(0, 4);
            switch (_rand)
            {
                case 0:
                    rb.AddForce(-Vector3.right * speed * Time.deltaTime);
                    break;
                case 1:
                    rb.AddForce(Vector3.forward * speed * Time.deltaTime);
                    break;
                case 2:
                    rb.AddForce(Vector3.right * speed * Time.deltaTime);
                    break;
                case 3:
                    rb.AddForce(-Vector3.forward * speed * Time.deltaTime);
                    break;
                default:
                    break;
                
            }
            curr_timer = 0;
        }
        curr_timer += 1;

        // Velocity limter
        if (rb.velocity.y < -10)
        {
            rb.velocity.Set(rb.velocity.x, -10, rb.velocity.z);
        }
        else if (rb.velocity.y > 10)
        {
            rb.velocity.Set(rb.velocity.x, 10, rb.velocity.z);
        }

        if (rb.velocity.x < -10)
        {
            rb.velocity.Set(-10, rb.velocity.y, rb.velocity.z);
        }
        else if (rb.velocity.x > 10)
        {
            rb.velocity.Set(10, rb.velocity.y, rb.velocity.z);
        }

        if (rb.velocity.z < -10)
        {
            rb.velocity.Set(rb.velocity.x, rb.velocity.y, -10);
        }
        else if (rb.velocity.z > 10)
        {
            rb.velocity.Set(rb.velocity.x, rb.velocity.y, 10);
        }

        // Debug.Log(rb.velocity);
    }

    private void OnCollisionEnter(Collision _collision)
    {
        Debug.Log("Ball Touched");
        gravity = -gravity;
        if (_collision.gameObject.tag == BrickPrefab.tag)
        {
            Destroy(_collision.gameObject);
        }   
    }

    private Vector3 NextPosition()
    {
        var _temp = Random.insideUnitSphere * transform.parent.localScale.x;
        return _temp;
    }
}
