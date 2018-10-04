using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DSPlatformManager : MonoBehaviour
{

    public GameObject PlatformPrefab;

    [SerializeField]
    private Vector3 expandSize;
    private bool isExpand = true;
    [SerializeField]
    private List<GameObject> List_Platform;

    public bool m_isExpand
    {
        get { return isExpand; }
        set { isExpand = value; }
    }

    private void Update()
    {
        if (List_Platform.Capacity == 0)
        {
            return;
        }
        //Expand(List_Platform[0]);
        /*
        if (isExpand)
        {
            if (List_Platform.Capacity == 1)
            {
                Expand(List_Platform[0]);
                return;
            }

            GameObject _closest = List_Platform[0];
            var _player = GameObject.FindGameObjectWithTag("Player");

            foreach (GameObject _obj in List_Platform)
            {
                if(Vector3.Distance(_player.transform.position, _obj.transform.position) < Vector3.Distance(_player.transform.position, _closest.transform.position))
                {
                    _closest = _obj;
                }
            }

            Expand(List_Platform[List_Platform.IndexOf(_closest)]);
        }
        */
    }

    // 0000000000000000000000000000000000000000000
    //              PRIVATE METHOD
    // 0000000000000000000000000000000000000000000

    private void Find_Children() // Find the children in itself
    {
        var _arr_children = GameObject.FindGameObjectsWithTag("Bridge");
        foreach (GameObject _children in _arr_children)
        {           
            if (List_Platform.IndexOf(_children) < 0)
            {
                List_Platform.Add(_children);
            }
        }
    }

    // 000000000000000000000000000000000000000000
    //              PUBLIC METHOD
    // 000000000000000000000000000000000000000000

    public void SpawnPlatform() // Force spawn a plateform
    {
        var _gameObj = PlatformPrefab;
        Instantiate(_gameObj, transform.position, Quaternion.identity, transform.parent);
        Find_Children();
    }

    public void DeletePlateform()
    {
        if (List_Platform == null)
        {
            return;
        }
        List_Platform.RemoveAt(0);
    }
}
