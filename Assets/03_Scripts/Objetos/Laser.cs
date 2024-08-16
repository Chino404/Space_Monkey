using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.UI.Image;

[RequireComponent(typeof(LineRenderer))]
public class Laser : MonoBehaviour
{
    [Header("LASER WARNING VALUES")]
    [Tooltip("Tiempo en que el laser esta desactivado")]public float timeDisableLaser = 3f;
    [Tooltip("Duracion de la advertencia del laser")]public float durationLaserWarning = 2f;
    [Tooltip("Duracion del laser")]public float durationLaser = 3f;
    [Range(0, 1)]public float widthLaserWarning;
    private float _timer;
    private RaycastHit _hit;
    private RaycastHit _hitTarget;
    public Transform startPoint;
    public Transform endPoint;
    [SerializeField] private LayerMask _layerObject;
    private bool _activeWarning;
    [SerializeField]private bool _activeLaser;

    private Vector3 _boxCastSize = new Vector3(1, 1, 1); // Tama�o del cubo


    [SerializeField]private LineRenderer _lineRenderer;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();

        if (_lineRenderer == null) Debug.LogError($"Falta el componente LineRenderer en {gameObject.name}");
        if (startPoint == null) Debug.LogError($"Falta el StartPoint en el {gameObject.name}");
        if (endPoint == null) Debug.LogError($"Falta el EndPoint en el {gameObject.name}");

        _lineRenderer.startWidth = 0;
    }

    private void Update()
    {
        _lineRenderer.SetPosition(0, startPoint.transform.position);

        if(!_activeWarning && _timer >= timeDisableLaser)
        {
            _activeWarning = true;
            StartCoroutine(ActiveWarning());
        }
        else if(_timer < timeDisableLaser) _timer += Time.deltaTime;

        

        //Raycast para el LineRenderer
        if (Physics.Raycast(startPoint.position, (endPoint.position - startPoint.position).normalized, out _hit, Mathf.Infinity, _layerObject))
        {
            endPoint.position = _hit.point;
            _lineRenderer.SetPosition(1, endPoint.transform.position);

            _boxCastSize = new Vector3(1, Vector3.Distance(startPoint.position, endPoint.position), 1);

            _lineRenderer.enabled = true;
        }
        else Debug.LogError("No choca con nada");

        if (_activeLaser)
        {
            // Direcci�n del Raycast (hacia el punto final)
            Vector3 direction = (endPoint.position - startPoint.position).normalized;

            // Calcular la distancia
            float distance = Vector3.Distance(startPoint.position, endPoint.position);

            // Realizar el BoxCast desde el punto de inicio hacia la direcci�n
            if (Physics.BoxCast(startPoint.position - direction * (distance / 2), _boxCastSize / 2, direction, out _hitTarget, transform.rotation, distance))
            {
                var targetComponent = _hitTarget.collider.GetComponent<ModelMonkey>();

                if (targetComponent != null)
                {
                    targetComponent.TakeDamageEntity(100, transform.position);
                    Debug.Log("A llorar monito");
                }
            }
        }
    }



    IEnumerator ActiveWarning()
    {
        StartCoroutine(InterpolateWidthLaserWarning());
        yield return new WaitForSeconds(durationLaserWarning);
        _lineRenderer.enabled = false;
        _activeLaser = true;
        _lineRenderer.startWidth = 7;
        yield return new WaitForSeconds(durationLaser);
        _lineRenderer.startWidth = 0;
        _activeWarning = false;
        _activeLaser = false;
        _timer = 0;
    }

    IEnumerator InterpolateWidthLaserWarning()
    {
        float elapsedTime = 0;

        while (elapsedTime < durationLaserWarning)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / durationLaserWarning;
            _lineRenderer.startWidth = Mathf.Lerp(0, widthLaserWarning, t);
            yield return null;
        }

        //_lineRenderer.startWidth = 0;
    }

    void OnDrawGizmos()
    {
        if (startPoint == null || endPoint == null) return;

        // Direcci�n del Raycast (hacia el punto final)
        Vector3 direction = (endPoint.position - startPoint.position).normalized;

        // Calcular la distancia
        float distance = Vector3.Distance(startPoint.position, endPoint.position);

        // Dibujar el cubo en el punto de inicio con la direcci�n y tama�o correcto
        //Gizmos.color = Color.red;
        //Gizmos.DrawWireCube(startPoint.position + direction * (distance / 2), _boxCastSize);

        // Dibujar una l�nea entre los dos puntos
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(startPoint.position, endPoint.position);
    }

}
