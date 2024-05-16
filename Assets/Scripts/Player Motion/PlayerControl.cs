using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils.StateMachines.Conventions;

public class PlayerControl : MonoBehaviour
{
    private PlayerMotionSettings _motionSettings;
    private StateManager _motionManager;

    private void Start()
    {
        _motionSettings = Resources.Load<PlayerMotionSettings>("PlayerMotionSettings");
        _motionManager = GetComponent<StateManager>();
    }

    private void Update()
    {
        var X_Input = Input.GetAxis(_motionSettings.horizontalAxisName);
        var Z_Input = Input.GetAxis(_motionSettings.verticalAxisName);

        if (X_Input != 0 || Z_Input != 0)
        {
            _motionSettings.horizontalAxisValue = X_Input;
            _motionSettings.verticalAxisValue = Z_Input;
            _motionManager.ChooseState(StateNames.XZMotionState);
        }
        else
        {
            _motionSettings.horizontalAxisValue = 0;
            _motionSettings.verticalAxisValue = 0;
        }
    }
}
