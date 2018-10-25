using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Hooming AI
public class GhostAi : MonoBehaviour
{
	enum SPEED
    {
        LOW,
        MID,
        HIGH,
    }

    enum RANGE
    {
        LOW,
        MID,
        HIGH,
    }

    private void Update()
    {
        var _player = GameObject.FindGameObjectWithTag("Player");
        var _playerScript = _player.GetComponent<DSPlayer>();

        Follow(_player.transform.position);
    }

    private void Follow(Vector3 _playerPos)
    {

    }
}
