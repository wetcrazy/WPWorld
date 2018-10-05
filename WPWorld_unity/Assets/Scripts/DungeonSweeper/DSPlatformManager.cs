using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DSPlatformManager : MonoBehaviour
{

    public GameObject PlatformPrefab;

    [SerializeField]
    private Vector3 ExpandSize;
    private bool isExpand = true;
    [SerializeField]
    private List<GameObject> List_Platform;

    bool once = false;

    public bool m_isExpand
    {
        get { return isExpand; }
        set { isExpand = value; }
    }

    private void Update()
    {
        if (List_Platform.Count == 0)
        {
            return;
        }

        foreach (GameObject _platformOBJ in List_Platform)
        {
            var _platformScript = _platformOBJ.GetComponent<DSPlatform>();
          
            if(_platformScript.m_isExpanding)
            {
                _platformScript.Expand(ExpandSize);
            }          
        }
       
        if (List_Platform.Count > 1)
        { 
            Rotate_Platform(List_Platform[1]);
        }
        if (List_Platform.Count > 3)
        {
            Rotate_Platform(List_Platform[3]);
        }
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

    private void Rotate_Platform(GameObject _obj)
    {
        var _objScript = _obj.GetComponent<DSPlatform>();
        if(_objScript.m_isRotated)
        {
            return;
        }
        _objScript.Rotate();
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
