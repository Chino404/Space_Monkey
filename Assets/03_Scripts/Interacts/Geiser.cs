using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Geiser : MonoBehaviour
{
    [SerializeField, Range(0,50f)] private float _actualForceGeiser = 2;
    [Tooltip("Fuerza del geiser original")]private float _iniForceGeiser;
    [Tooltip("Fuerza del geiser cuando se est� planeando")]private float _forceGeiserOnPenguin;

    [SerializeField]private Vector3 _scaleCollider;
    private Vector3 _iniScaleCollider;
    private BoxCollider _myCollider;

    [Space(10), Header("-> Particle")]
    [SerializeField] private float _actualSpeedParticle;
    private float _iniSpeedParticle;
    private ParticleSystem _myParticle;

    private void Awake()
    {
        _myParticle = GetComponentInChildren<ParticleSystem>();
        _myCollider = GetComponent<BoxCollider>();

        _scaleCollider = _myCollider.size;

        var main = _myParticle.main; //Para poder llegar a sus variables
        _actualSpeedParticle = main.startSpeed.constant;

        _iniScaleCollider = _myCollider.size;
        _iniSpeedParticle = main.startSpeed.constant;
        _iniForceGeiser = _actualForceGeiser;
    }

    private void Start()
    {
        _forceGeiserOnPenguin = _actualForceGeiser / 1.5f;
    }

    public void ModifyScaleYGeiser(float newScaleY, float newSpeedParticle)
    {
        _scaleCollider.y = newScaleY;
        _myCollider.size = _scaleCollider;

        var main = _myParticle.main; //Para poder llegar a sus variables
        _actualSpeedParticle = newSpeedParticle;
        main.startSpeed = _actualSpeedParticle;

        //var porcentaje = newScaleY / (_iniScaleCollider.y * 1.5f);
    }

    public void RevertChange()
    {
        _myCollider.size = _iniScaleCollider;

        var main = _myParticle.main; //Para poder llegar a sus variables
        main.startSpeed = _iniSpeedParticle;
    }

    private void OnTriggerStay(Collider other)
    {
        var rb = other.gameObject.GetComponent<Rigidbody>();
        var bongo = other.gameObject.GetComponent<ModelBongo>();

        if (rb != null)
        {
            if (bongo != null && bongo.IsFly) _actualForceGeiser = _forceGeiserOnPenguin;
            else _actualForceGeiser = _iniForceGeiser;


            //rb.AddForce(transform.up * _actualForceGeiser, ForceMode.VelocityChange);

            rb.velocity = new Vector3(rb.velocity.x, _actualForceGeiser, rb.velocity.z);
        }
    }

    
}
