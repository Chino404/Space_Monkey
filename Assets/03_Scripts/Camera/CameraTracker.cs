using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;


public class CameraTracker : MonoBehaviour
{
    public static CameraTracker Instance;

    public CharacterTarget characterTarget;

    [Header("Components")]
    [SerializeField] private Transform _point;
    public Transform Point {  get { return _point; } }

    [SerializeField]private Transform _target;

    [Header("Smoothing Values")]
    [Range(0.01f, 0.125f)] [SerializeField] float _smoothSpeedPosition = 0.075f;
    [Range(0.01f, 0.125f)] [SerializeField] float _smoothSpeedRotation = 0.075f;

    Vector3 _desiredPos, _smoothPos;
    Quaternion _smoothRot;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {

        if(_point == null)
        {
            Debug.LogError("ASIGNAR PUNTO A LA C�MARA");
            return;
        }

        StartCoroutine(Wait());

    }

    IEnumerator Wait()
    {
        yield return new WaitForEndOfFrame();

        transform.position = _point.position;

        if (characterTarget == CharacterTarget.Bongo) _target = GameManager.instance.bongo.transform;

        else if (characterTarget == CharacterTarget.Frank)_target = GameManager.instance.frank.transform;

        if (_target == null) Debug.LogError("FALTA TARGET");
        else gameObject.GetComponent<CameraTransparency>().target = _target;

    }

    private void FixedUpdate()
    {
        if (_target == null || _point == null) return;     
        
        SetPositionAndRotationTarget();
    }

    private void SetPositionAndRotationTarget()
    {

        // Calcular la posici�n deseada relativa al punto
        _desiredPos = _target.position + (_point.position - _target.position);

        _smoothPos = Vector3.Lerp(transform.position, _desiredPos, _smoothSpeedPosition);

        _smoothRot = Quaternion.Lerp(transform.rotation, _point.rotation, _smoothSpeedRotation);

        transform.SetPositionAndRotation(_smoothPos, _smoothRot);

        Debug.DrawLine(transform.position, _target.position, Color.green);
    }

    public void TransicionPoint(Transform newPoint)
    {
        _point = newPoint;
    }

}
