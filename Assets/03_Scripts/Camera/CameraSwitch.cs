using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class CameraSwitch : MonoBehaviour
{
    [Header("Componet")]
    private CameraTracker _tracker;
    public Transform backTo;
    public Transform goTo;

    private void Start()
    {
        _tracker = CameraTracker.Instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<ModelMonkey>())
        {
            _tracker.TransicionPoint(goTo);
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if(other.gameObject.GetComponent<ModelMonkey>())
        {
            _tracker.TransicionPoint(backTo);
        }
    }

}
