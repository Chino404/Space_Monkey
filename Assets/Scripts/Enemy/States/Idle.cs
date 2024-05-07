using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : IState
{

    Enemy _enemy;
    FSM _fsm;



    public Idle(Enemy enemy, FSM fsm)
    {
        _enemy = enemy;
        _fsm = fsm;
    }
    public void OnEnter()
    {

    }

    public void OnUpdate()
    {
        _enemy.anim.SetBool("Walk", false);
        if ((_enemy.transform.position - _enemy.target.transform.position).sqrMagnitude < _enemy.viewRadius * _enemy.viewRadius)
            _fsm.ChangeState("Follow Player");

    }

    public void OnExit()
    {

    }

    

    
}
