using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils.StateMachines.Conventions;

[StateFilter(StateGroupings.PlayerMotion)]
public class RunningState : StateBase
{
    private CharacterController _controller;
    private PlayerMotionSettings _motionSettings;
    private Animator _animator;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _motionSettings = Resources.Load<PlayerMotionSettings>("PlayerMotionSettings");
        _animator = GetComponentInChildren<Animator>();
    }

    public override void OnStateEnter()
    {
        _animator.SetTrigger("RUN");
    }

    public override void OnStateUpdate()
    {
        var X_Input = _motionSettings.horizontalAxisValue;
        var rotation = _motionSettings.turnSpeed * X_Input * Vector3.up;
        var Z_Input = _motionSettings.verticalAxisValue;
        var velocity = _motionSettings.runKeyPressed
            ? _motionSettings.runSpeed * Z_Input * transform.forward
            : _motionSettings.speed * Z_Input * transform.forward;
        var playbackSpeed = _motionSettings.runKeyPressed
            ? _motionSettings.runAnimSpeed
            : _motionSettings.walkAnimSpeed;

        transform.Rotate(rotation * Time.deltaTime);
        _controller.Move(velocity * Time.deltaTime);
        _animator.speed = playbackSpeed;
    }

    public override void OnStateExit()
    {

    }
}
