using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Impulse : MonoBehaviour
{
    [SerializeField] ForceMode _impulseMode;
    [Range(10, 70)]
    [SerializeField] private int _force;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3)
        {
            var rb = other.GetComponent<Rigidbody>();
            var tr = other.GetComponent<Transform>();

            //rb.velocity = new Vector3(0f, tr.localScale.y * _force);

            rb.velocity = Vector3.up * _force;

        }
    }
}