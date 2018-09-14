using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour
{
    public GameObject BrickPrefab;
    public float speed, MAX_timer;

    private Vector3 oldPos;
    private Vector3 vel;
    private float curr_timer;

    private void Update()
    {
        // Check if the ball is moving
        if (transform.position != oldPos)
        {
            oldPos = transform.position;
            return;
        }

        Debug.Log("Ball: Time to move");
        float _step = speed * Time.deltaTime;

        if(curr_timer > MAX_timer)
        {
            curr_timer = 0.0f;
            int _rng = Random.Range(0, 8);
            switch(_rng)
            {
                case 0:
                    vel = Vector3.forward;
                    break;
                case 1:
                    vel = new Vector3(1.0f, 0.0f, 1.0f);
                    break;
                case 2:
                    vel = Vector3.right;
                    break;
                case 3:
                    vel = new Vector3(1.0f, 0.0f, -1.0f);
                    break;
                case 4:
                    vel = -Vector3.forward;
                    break;
                case 5:
                    vel = new Vector3(-1.0f, 0.0f, -1.0f);
                    break;
                case 6:
                    vel = -Vector3.right;
                    break;
                case 7:
                    vel = new Vector3(-1.0f, 0.0f, 1.0f);
                    break;
                default:
                    break;
            }

        }

        curr_timer += 0.1f;
        transform.Translate(vel * _step);
    }

    private void OnCollisionEnter(Collision _collision)
    {
        Debug.Log("Ball Touched");
        if (_collision.gameObject.tag == BrickPrefab.tag)
        {
            Destroy(_collision.gameObject);
        }

    }
}
