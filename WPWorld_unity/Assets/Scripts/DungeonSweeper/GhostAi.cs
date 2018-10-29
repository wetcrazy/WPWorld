using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Hooming AI
public class GhostAi : MonoBehaviour
{
    public enum STATE
    {
        STATE_IDLE,
        STATE_FINDING,
        STATE_FOLLOWING
    }

    //[Range(0.01f, 0.1f)]
    [SerializeField]
    private float Speed = 0.0f;
    [Range(0.1f, 0.5f)]
    [SerializeField]
    private float Range;
    [SerializeField]
    private float MAX_TIMER = 10;

    public bool isRelease = false;

    public float Timer = 0.0f;
    public STATE AI_STATE;
    private Vector3 _newVect = new Vector3();

    private void Awake()
    {
        AI_STATE = STATE.STATE_IDLE;
    }

    private void Update()
    {     
        var _player = GameObject.FindGameObjectWithTag("Player");
        if (Timer > MAX_TIMER)
        {
            AI_STATE = STATE.STATE_FINDING;
            Timer = 0.0f;
        }

        switch (AI_STATE)
        {
            case STATE.STATE_IDLE:             
                RandomTarget();
                break;

            case STATE.STATE_FINDING:
                Finding(_player.transform.position);
                break;

            case STATE.STATE_FOLLOWING:
                MoveTo(_player.transform.position);
                AI_STATE = STATE.STATE_FINDING;
                break;
        }

        Timer += 0.1f;
    }

    private void MoveTo(Vector3 _target)
    {
        Speed = Random.Range(0.01f, 0.1f);
        var _newTar = new Vector3(_target.x, _target.y, _target.z);
        gameObject.transform.LookAt(_newTar);
        //gameObject.transform.Translate(Vector3.forward * Speed);

        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, _newTar, Speed);
    }

    private void Finding(Vector3 _target)
    {
        if(Vector3.Distance(transform.position,_target) < Range)
        {
            AI_STATE = STATE.STATE_FOLLOWING;
        }
        else
        {            
            AI_STATE = STATE.STATE_IDLE;
        }
    }

    private void RandomTarget()
    {
        if(transform.position == _newVect)
        {
            _newVect.x = Random.Range(-1.0f, 1.0f);
            _newVect.z = Random.Range(-1.0f, 1.0f);
            _newVect.y = 0;
        }
        MoveTo(_newVect);      
    }
}
