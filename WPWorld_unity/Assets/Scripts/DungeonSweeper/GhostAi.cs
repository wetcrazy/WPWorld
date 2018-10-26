using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Hooming AI
public class GhostAi : MonoBehaviour
{
    [Range(0.01f,0.05f)]
    [SerializeField]
    private float Speed;
    [Range(0.1f,0.5f)]
    [SerializeField]
    private float Range;
    [SerializeField]
    private float MAX_TIMER;

    public bool isRelease = false;
    
    private float Timer = 0.0f;

    private void Update()
    {      
        if (!isRelease)
        {
            return;
        }

        Vector3 _target = new Vector3();
        var _player = GameObject.FindGameObjectWithTag("Player");

        Collider[] _collided = Physics.OverlapSphere(transform.position, Range);

        if(_collided.Length <= 2)
        {
            _target = new Vector3(0, 0.2f, 0);
        }
        else
        {
            foreach (Collider _col in _collided)
            {
                if (_col.gameObject == _player)
                {
                    _target = _player.transform.localPosition;
                    break;
                }
            }

            if(_target == new Vector3(0,0,0) && Timer > MAX_TIMER)
            {
                Timer = 0.0f;
                Debug.Log("DING");
                //_target = RandomTarget();
            }
        }

        Timer += 0.1f;
        GoTo(_target);
    }

    private void GoTo(Vector3 _target)
    {
        var _newTar = new Vector3(_target.x, 0.2f, _target.z);
        gameObject.transform.LookAt(_newTar);
        gameObject.transform.Translate(Vector3.forward * Speed);
    }

    private Vector3 RandomTarget()
    {
        Vector3 _newTarget = new Vector3();

        _newTarget.x = Random.Range(-30, 31);
        _newTarget.z = Random.Range(-30, 31);
        _newTarget.y = 0.2f;

        return _newTarget;
    }
}
