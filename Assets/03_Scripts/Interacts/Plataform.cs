using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataform : MonoBehaviour, IInteractable
{
    //[SerializeField] float _secondsWaiting=2f;
    //[SerializeField] Transform[] _waypoints;
    [SerializeField] float _maxVelocity;

    
    //[SerializeField]private int _actualIndex;
    //[SerializeField]private float _maxForce=0.01f;
    //[SerializeField]private Vector3 _velocity;


    private Rigidbody _rb;

    private bool _isObjectAttached;
    //[SerializeField] private float _minLimit = -5f;
    //[SerializeField] private float _maxLimit = 5f;
    public enum Axis { X,Y,Z}
    public Axis movementAxis = Axis.Z;

    [SerializeField] Transform banana;
    private Vector3 _startPos;

    public ModelBanana modelBanana;
    private Ray _moveRay;
    private float _moveRange=0.75f;
    [SerializeField]private LayerMask _moveMask;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        //modelBanana = GetComponent<ModelBanana>();
        _startPos = transform.position;
    }
    
    void FixedUpdate()
    {
        if (IsBlocked(modelBanana.Velocity)||banana==null) return;
        if (_isObjectAttached&&banana!=null)
        {

            switch (movementAxis)
            {
                case Axis.Z:
                    //localPosition.x = _startPos.x;
                    //localPosition.y = _startPos.y;
                    //bananaPosition.x = _startPos.x;
                    //bananaPosition.y = _startPos.y;
                    //localPosition.z = bananaPosition.z;
                    _rb.MovePosition(transform.position + modelBanana.Velocity * Time.fixedDeltaTime);
                    Debug.Log("me muevo en eje Z");
                    break;

            }
            
        }
        
    }

    private bool IsBlocked(Vector3 dir)
    {
        _moveRay = new Ray(transform.position, dir);
        Debug.DrawRay(transform.position, dir * _moveRange, Color.red);

        return Physics.Raycast(_moveRay, _moveRange, _moveMask);
    }

    //private void FixedUpdate()
    //{
    //    //AddForce(Seek(_waypoints[_actualIndex].position));

    //    if (Vector3.Distance(transform.position, _waypoints[_actualIndex].position)<=0.4f)
    //    {
    //        StartCoroutine(WaitSeconds());
    //        _actualIndex++;
    //        if (_actualIndex >= _waypoints.Length)
    //        {

    //            _actualIndex = 0;
    //        }

    //    }
    //    _velocity = _waypoints[_actualIndex].position - transform.position;
    //    _velocity.Normalize();
    //    //transform.position += _velocity * Time.fixedDeltaTime;
    //    //_rb.MovePosition(transform.position + _velocity*_maxVelocity * Time.fixedDeltaTime);
    //}



    //IEnumerator WaitSeconds()
    //{
    //    var velocity = _maxVelocity;
    //    _maxVelocity = 0;
    //    yield return new WaitForSeconds(_secondsWaiting);
    //    _maxVelocity = velocity;

    //}

    //Vector3 Seek(Vector3 target)
    //{
    //    var desired = target - transform.position;
    //    desired.Normalize();
    //    desired *= _maxVelocity;
    //    return CalculateSteering(desired);
    //}

    //Vector3 CalculateSteering(Vector3 desired)
    //{
    //    var steering = desired - _velocity;
    //    steering = Vector3.ClampMagnitude(steering, _maxForce);
    //    return steering;
    //}

    //void AddForce(Vector3 dir)
    //{
    //    _velocity += dir;

    //    _velocity = Vector3.ClampMagnitude(_velocity, _maxVelocity);
    //}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<ModelMonkey>())
        {
            
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.GetComponent<ModelMonkey>())
            collision.transform.SetParent(null);
    }

    public void LeftClickAction()
    {
        
    }

    public void RightClickAction(Transform parent)
    {        
        if (!_isObjectAttached)
        {
            //transform.SetParent(parent);
            banana = parent;
            _isObjectAttached = true;
        }
        else if (_isObjectAttached)
        {
            ReleaseObject();
        }
    }

    public void ReleaseObject()
    {
        //transform.SetParent(null);
        banana = null;
        _isObjectAttached = false;
    }
}
