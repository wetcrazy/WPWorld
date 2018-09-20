using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float Power;
    public float Radius;
   
    private void OnCollisionEnter()
    {
        Collider[] _colliders = Physics.OverlapSphere(transform.position, Radius);
        foreach (Collider _hit in _colliders)
        {
            if (_hit.transform.tag == "Player")
            {
                Rigidbody rb = _hit.GetComponent<Rigidbody>();

                Debug.Log("BOOOM");
                rb.AddExplosionForce(Power, transform.position, Radius, 3.0F, ForceMode.Impulse);
                Destroy(gameObject);
            }
           
        }
    }
}
