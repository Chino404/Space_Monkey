using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Attack : IState
{
    Enemy _enemy;
    FSM _fsm;
    
    

    public Attack (Enemy enemy, FSM fsm)
    {
        _enemy = enemy;
        _fsm = fsm;
    }
    public void OnEnter()
    {
        Debug.Log("ataco");
    }

    public void OnUpdate()
    {
        //Debug.Log("estoy atacando");
        _enemy.anim.SetBool("Walk", false);
        if(!_enemy.isHitting)
        _enemy.Hit();
        
        if ((_enemy.transform.position - _enemy.target.transform.position).sqrMagnitude > _enemy.attackDistance * _enemy.attackDistance)
        {
            _fsm.ChangeState("Follow Player");
            //_enemy.anim.SetBool("Attack", false);
        }
        
    }

    public void OnExit()
    {

    }

    
}
