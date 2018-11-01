using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextBlocksBehaviour : MonoBehaviour {


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Killbox"))
        {
            this.gameObject.SetActive(false);
        }

    }
}
