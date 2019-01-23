using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformStageScript : MonoBehaviour
{
    [Header("Sound Settings")]
    [SerializeField]
    private string BGM;

    [Space]
    [Header("Movement Settings")]
    [SerializeField]
    private float MinimumDistance;
    [SerializeField]
    private float MaximumDistance;

    private SoundSystem SoundSystemRef;

    private void Start()
    {
        SoundSystemRef = GameObject.FindGameObjectWithTag("SoundSystem").GetComponent<SoundSystem>();
        if (BGM != "")
            SoundSystemRef.PlayBGM(BGM);

        Debug.Log(transform.eulerAngles.y);

        if ((transform.eulerAngles.y >= 0 && transform.eulerAngles.y < 45) ||
            (transform.eulerAngles.y >= 315 && transform.eulerAngles.y < 360))
        {
            transform.eulerAngles = Vector3.zero;
        }

        if (transform.eulerAngles.y >= 45 && transform.eulerAngles.y < 135)
        {
            transform.eulerAngles = new Vector3(0, 90, 0);
        }

        if (transform.eulerAngles.y >= 135 && transform.eulerAngles.y < 225)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

        if (transform.eulerAngles.y >= 225 && transform.eulerAngles.y < 315)
        {
            transform.eulerAngles = new Vector3(0, 270, 0);
        }
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, Camera.main.transform.position) > MinimumDistance)
        {
            Vector3 NewPos = Vector3.MoveTowards(transform.position, Camera.main.transform.position, Time.deltaTime);
            NewPos.y = transform.position.y;

            transform.position = NewPos;
        }

        if (Vector3.Distance(transform.position, Camera.main.transform.position) < MaximumDistance)
        {
            Vector3 NewPos = Vector3.MoveTowards(transform.position, Camera.main.transform.position, -1 * Time.deltaTime);
            NewPos.y = transform.position.y;

            transform.position = NewPos;
        }
    }
}
