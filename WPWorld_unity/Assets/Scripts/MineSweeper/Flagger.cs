using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Allows the player to spawn flags
/// </summary>
public class Flagger : MonoBehaviour
{
    public GameObject FlagPrefab;
    public List<GameObject> List_Of_FlagOBJ;

    private GameObject PlayerOBJ;

    private void Awake()
    {
        PlayerOBJ = GameObject.FindGameObjectWithTag("Player");
        List_Of_FlagOBJ = new List<GameObject>();
    }

    public void OnButtonDownSpawnFlag()
    {
        var _clone = Instantiate(FlagPrefab, PlayerOBJ.transform.position, Quaternion.identity, transform.parent);
        List_Of_FlagOBJ.Add(_clone);
    }

    public void OnButtonDownAllFlagClear()
    {
        var _allOBJ = GameObject.FindGameObjectsWithTag("Flag");
        foreach(GameObject _obj in _allOBJ)
        {
            Destroy(_obj);
        }
    }
    
    public void OnButtonDownFlagClearOnPoint()
    {
        Collider[] _Arr_col = Physics.OverlapSphere(PlayerOBJ.transform.position, transform.localScale.x / 10);

        foreach (Collider _col in _Arr_col)
        {
            if (_col.gameObject.tag == "Flag")
            {
                Destroy(_col.gameObject);
            }
        }
    }
}
