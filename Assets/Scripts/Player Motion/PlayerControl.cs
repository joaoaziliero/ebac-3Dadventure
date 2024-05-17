using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils.StateMachines.Conventions;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private CharacterController _player;

    private PlayerMotionSettings _motionSettings;
    private StateManager _motionManager;

    private void Start()
    {
        _motionSettings = Resources.Load<PlayerMotionSettings>("PlayerMotionSettings");
        _motionManager = GetComponent<StateManager>();
    }

    private void Update()
    {
        MoveOnCardinalDirections();
        MoveByHeigthChange();
        ApplyConstantGroundingPull();
        CheckForIdleness();
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

    private void ApplyConstantGroundingPull()
    {
        var pull = _motionSettings.pullForGrounding * Vector3.down;
        _player.Move(pull * Time.deltaTime);
    }

    private void CheckForIdleness()
    {
        if (_motionSettings.horizontalAxisValue == 0 && _motionSettings.verticalAxisValue == 0 && _player.isGrounded)
        {
            _motionManager.ChooseState(StateNames.IdleState);
        }
    }
}
