using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombermanManager : MonoBehaviour
{
    public void PlayerDead(GameObject _selectedOBJ, bool _boolValue)
    {
        _selectedOBJ.GetComponent<BomberManPlayer>().SetisDead(_boolValue);
    }

    public void DestoryMyBombCount(GameObject _selectedOBJ)
    {
        _selectedOBJ.GetComponent<BomberManPlayer>().OneBombDestory();
    }
}
