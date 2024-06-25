using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils.StateMachines.Conventions;

[StateFilter(StateGroupings.EnemyCoordination)]
public class EnemyDeathState : StateBase
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public override void OnStateEnter()
    {
        _animator.SetTrigger("DEATH");
    }

    public override void OnStateUpdate()
    {

    }

    public override void OnStateExit()
    {

    }
}

