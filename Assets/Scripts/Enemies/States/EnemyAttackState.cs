using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils.StateMachines.Conventions;

[StateFilter(StateGroupings.EnemyCoordination)]
public class EnemyAttackState : StateBase
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public override void OnStateEnter()
    {
        _animator.SetTrigger("ATTACK");
    }

    public override void OnStateUpdate()
    {

    }

    public override void OnStateExit()
    {

    }
}
