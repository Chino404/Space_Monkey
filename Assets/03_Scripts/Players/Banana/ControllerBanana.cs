using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerBanana
{
    private float _xAxis, _zAxis/*, _inputMouseX, _inputMouseY*/;

    private Vector3 _dirRaw = new Vector3();
    private Vector3 _dir = new Vector3();

    private ModelBanana _model;

    [SerializeField] int _bulletQuantity;

   


    public ControllerBanana(ModelBanana model)
    {
        _model = model;
    }

    

    //public void ArtificialUpdate()
    //{
    //    _inputMouseX = Input.GetAxisRaw("Mouse X");
    //    _inputMouseY = Input.GetAxisRaw("Mouse Y");

    //    if(_inputMouseX != 0 || _inputMouseY != 0) _model.Rotation(_inputMouseX, _inputMouseY);

    //    //if (Input.GetKeyDown(KeyCode.Q)) GameManager.instance.Swap();   

    //}
    public void ArtificialUpdate()
    {
        if (Input.GetMouseButtonDown(0)) _model.ElectricCharge();
        if (Input.GetMouseButtonDown(1)) _model.MoveObjects();
        //if (Input.GetMouseButtonUp(1)) _model.ReleaseObjects();

        if (Input.GetKeyDown(KeyCode.Space)) _model.FlyingUp();

    }

    public void ListenFixedKeys()
    {
        //_xAxis = Input.GetAxisRaw("Horizontal");
        //_zAxis = Input.GetAxisRaw("Vertical");

        //if (_xAxis != 0 || _zAxis != 0)
        //    _model.Movement(_xAxis, _zAxis);
        //else _model.Velocity = Vector3.zero;

        _dirRaw.x = Input.GetAxisRaw("Horizontal");
        _dirRaw.z = Input.GetAxisRaw("Vertical");

        _dir.x = Input.GetAxis("Horizontal");
        _dir.z = Input.GetAxis("Vertical");

        _model.Movement(_dirRaw, _dir);



        //if(Input.GetKey(KeyCode.Space)) _model.FlyingUp();
        //if(Input.GetKey(KeyCode.LeftControl)) _model.FlyingDown();
        //else _model.StopFly();
    }
}
