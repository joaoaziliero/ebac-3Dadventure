using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils.StateMachines.Conventions;

[StateFilter(StateGroupings.PlayerMotion)]
public class IdleState : StateBase
{
    public override void OnStateEnter()
    {
        Debug.Log("Player is idle");
    }

    public override void OnStateUpdate()
    {

    }

    public override void OnStateExit()
    {

    }
}
