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
        var rotation = _motionSettings.turnSpeed * X_Input * Vector3.up;
        var Z_Input = _motionSettings.verticalAxisValue;
        var velocity = _motionSettings.speed * Z_Input * transform.forward;

        transform.Rotate(rotation * Time.deltaTime);
        _controller.Move(velocity * Time.deltaTime);
    }

    public override void OnStateExit()
    {

    }
}
