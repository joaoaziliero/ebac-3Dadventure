using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Utils.StateMachines.Conventions;

public class PlayerControl : MonoBehaviour
{
    private CharacterController _player;
    private PlayerMotionSettings _motionSettings;
    private StateManager _motionManager;
    private float _timer;

    private void Start()
    {
        _player = GetComponentInParent<CharacterController>();
        _motionSettings = Resources.Load<PlayerMotionSettings>("PlayerMotionSettings");
        _motionManager = GetComponent<StateManager>();
        _timer = 0;
    }

    private void Update()
    {
        ApplyGravity();
        MoveOnCardinalDirections();
        MoveByHeigthChange();
        CheckForIdleness();
    }

    private void ApplyGravity()
    {
        if (_motionManager.CurrentState != StateNames.JumpingState && !_player.isGrounded)
        {
            _player.Move(_motionSettings.gravity * _timer * Time.deltaTime * Vector3.down);
            _timer += Time.deltaTime;
        }
        else
        {
            _timer = 0;
        }
    }

    private void MoveOnCardinalDirections()
    {
        var X_Input = Input.GetAxis(_motionSettings.horizontalAxisName);
        var Z_Input = Input.GetAxis(_motionSettings.verticalAxisName);

        if (X_Input != 0 || Z_Input != 0)
        {
            _motionSettings.horizontalAxisValue = X_Input;
            _motionSettings.verticalAxisValue = Z_Input;
            _motionManager.ChooseState(StateNames.RunningState);
        }
        else
        {
            _motionSettings.horizontalAxisValue = 0;
            _motionSettings.verticalAxisValue = 0;
        }
    }

    private void MoveByHeigthChange()
    {
        if (Input.GetKeyDown(_motionSettings.jumpKeyCode))
        {
            _motionSettings.jumpKeyPressed = true;
            _motionManager.ChooseState(StateNames.JumpingState);
        }
        else
        {
            _motionSettings.jumpKeyPressed = false;
        }
    }

    private void CheckForIdleness()
    {
        if (_motionSettings.horizontalAxisValue == 0 && _motionSettings.verticalAxisValue == 0 && _player.isGrounded)
        {
            _motionManager.ChooseState(StateNames.IdleState);
        }
    }
}
