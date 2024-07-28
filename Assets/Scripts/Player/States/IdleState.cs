using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils.StateMachines.Conventions;

[StateFilter(StateGroupings.PlayerMotion)]
public class IdleState : StateBase
{
    public override void OnStateEnter()
    {
        GetComponentInChildren<Animator>().SetTrigger("IDLE");
    }

    public override void OnStateUpdate()
    {

    }

    public override void OnStateExit()
    {

    }
}
