using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SplashScreenManager : MonoBehaviour
{
    [SerializeField]
    private Button SinglePlayerButton;
    [SerializeField]
    private Button MultiPlayerButton;

    private void Start()
    {
        Color SPButtonColor = SinglePlayerButton.image.color;
        Color MPButtonColor = MultiPlayerButton.image.color;

        SPButtonColor.a = 0;
        MPButtonColor.a = 0;

        SinglePlayerButton.image.color = SPButtonColor;
        MultiPlayerButton.image.color = MPButtonColor;
    }

    public void RevealButtons()
    {
        Color SPButtonColor = SinglePlayerButton.image.color;
        Color MPButtonColor = MultiPlayerButton.image.color;

        SPButtonColor.a = Mathf.Lerp(SPButtonColor.a, 1, 0.1f);
        MPButtonColor.a = Mathf.Lerp(MPButtonColor.a, 1, 0.1f);

        SinglePlayerButton.image.color = SPButtonColor;
        MultiPlayerButton.image.color = MPButtonColor;
    }
}
