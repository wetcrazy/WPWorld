using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SplashScreenManager : MonoBehaviour
{
    [SerializeField]
    private Button SinglePlayerButton;
    [SerializeField]
    private Button MultiPlayerButton;

    private void Start()
    {
        Color newColor = Color.white;
        newColor.a = 0;

        SinglePlayerButton.image.color = newColor;
        MultiPlayerButton.image.color = newColor;

        newColor = Color.black;
        newColor.a = 0;

        SinglePlayerButton.gameObject.GetComponentInChildren<Text>().color = newColor;
        MultiPlayerButton.gameObject.GetComponentInChildren<Text>().color = newColor;

        SinglePlayerButton.enabled = false;
        MultiPlayerButton.enabled = false;
    }

    public void RevealButtons()
    {
        float step = Time.deltaTime;

        // Buttons
        Color SPButtonColor = SinglePlayerButton.image.color;
        Color MPButtonColor = MultiPlayerButton.image.color;

        SPButtonColor.a = Mathf.Lerp(SPButtonColor.a, 1, step);
        MPButtonColor.a = Mathf.Lerp(MPButtonColor.a, 1, step);

        SinglePlayerButton.image.color = SPButtonColor;
        MultiPlayerButton.image.color = MPButtonColor;


        // Text
        Color SPButtonTextColor = SinglePlayerButton.gameObject.GetComponentInChildren<Text>().color;
        Color MPButtonTextColor = MultiPlayerButton.gameObject.GetComponentInChildren<Text>().color;

        SPButtonTextColor.a = Mathf.Lerp(SPButtonTextColor.a, 1, step);
        MPButtonTextColor.a = Mathf.Lerp(MPButtonTextColor.a, 1, step);

        SinglePlayerButton.gameObject.GetComponentInChildren<Text>().color = SPButtonTextColor;
        MultiPlayerButton.gameObject.GetComponentInChildren<Text>().color = MPButtonTextColor;

        SinglePlayerButton.enabled = true;
        MultiPlayerButton.enabled = true;
    }

    public void SinglePlayerButtonOnTouch()
    {
        SceneManager.LoadScene("ARPlayground");
    }

    public void MultiPlayerButtonOnTouch()
    {
        SceneManager.LoadScene("PhotonTest");
    }
}
