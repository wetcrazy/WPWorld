using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour
{
    public Queue<Vector3> turningPos = new Queue<Vector3>();
    public Queue<Head.STATE_FACING> turningDirection = new Queue<Head.STATE_FACING>();
}
