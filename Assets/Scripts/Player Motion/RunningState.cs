using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils.StateMachines.Conventions;

[StateFilter(StateGroupings.PlayerMotion)]
public class RunningState : StateBase
{
    private CharacterController _controller;
    private PlayerMotionSettings _motionSettings;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _motionSettings = Resources.Load<PlayerMotionSettings>("PlayerMotionSettings");
    }

    public override void OnStateEnter()
    {

    }

    public override void OnStateUpdate()
    {
        var X_Input = _motionSettings.horizontalAxisValue;
        var Z_Input = _motionSettings.verticalAxisValue;

        var velocity = _motionSettings.speed * new Vector3(X_Input, 0, Z_Input);
        _controller.Move(velocity * Time.deltaTime);
    }

    public override void OnStateExit()
    {

    }
}
