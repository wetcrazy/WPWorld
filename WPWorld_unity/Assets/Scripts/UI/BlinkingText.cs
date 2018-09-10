using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Makes text blink when attached to a gameobject with text
/// </summary>

public class BlinkingText : MonoBehaviour {

    private Text _text;
    public int _MAX_CD = 5;
    private float _curr_CD;

    void Start()
    {
        _text = GetComponent<Text>();
    }

    void Update()
    {
        if(_curr_CD > _MAX_CD)
        {
            _text.enabled = !_text.enabled;
            _curr_CD = 0;
        }
        _curr_CD += 0.1f;
    }
}
