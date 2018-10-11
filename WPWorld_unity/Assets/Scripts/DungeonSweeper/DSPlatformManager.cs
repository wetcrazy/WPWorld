using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DSPlatformManager : MonoBehaviour
{
    public GameObject PlatformPrefab;
    public GameObject DSManager;

    [SerializeField]
    private Vector3 ExpandingSpeed;

    private List<GameObject> List_Anchors;
    private List<GameObject> List_Platforms;

    private void Awake()
    {
        List_Anchors = DSManager.GetComponent<Dungeonsweeper2>().List_Anchors;
        List_Platforms = new List<GameObject>();
    }

    // Update
    private void LateUpdate()
    {
        for (int i = 0; i < List_Anchors.Count; i++)
        {
            var _anchorScript = List_Anchors[i].GetComponent<AnchorPoint>();        

            if (_anchorScript.m_isdone && !_anchorScript.m_isPlatformSpawn)
            {
                _anchorScript.m_isPlatformSpawn = true;
                string _num = "";
                foreach (char _char in _anchorScript.m_GridName)
                {
                    if (char.IsDigit(_char))
                    {
                        _num += _char;
                    }
                    else
                    {
                        break;
                    }
                }
                int _temp = int.Parse(_num);
                Spawn_Bridge(_temp, _anchorScript.mList_Blocks[0].transform.parent.localScale.x / 10, List_Anchors[i], List_Anchors[i + 1]);
            }
        }

        if(List_Platforms == null)
        {
            return;
        }

        foreach (GameObject _platform in List_Platforms)
        {
            var _bridge = _platform.GetComponentInChildren<DSPlatform>();

            if(_bridge.m_isExpanding)
            {
                _platform.transform.localScale += ExpandingSpeed;
            }           
        }
    }

    // Spawn the bridge
    private void Spawn_Bridge(int _numCubes, float _cubeSize, GameObject _anchor1, GameObject _anchor2)
    {
        float _radius = (_numCubes / 2) * 0.2f;
        var _direction = _anchor2.transform.position - _anchor1.transform.position;
        var _pos = _anchor1.transform.position + (_radius * _direction.normalized);

        var _temp = Instantiate(PlatformPrefab, _pos, Quaternion.identity , transform);
        _temp.transform.LookAt(_anchor2.transform.position);
        List_Platforms.Add(_temp);      
    }
}
