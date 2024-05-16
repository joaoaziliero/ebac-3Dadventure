using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils.StateMachines.Conventions;

[StateFilter(StateGroupings.PlayerMotion)]
public class JumpingState : StateBase
{
    private CharacterController _controller;
    private PlayerMotionSettings _motionSettings;
    private Coroutine _jump;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _motionSettings = Resources.Load<PlayerMotionSettings>("PlayerMotionSettings");
    }

    public override void OnStateEnter()
    {
        _jump = StartCoroutine(Jump());
    }

    public override void OnStateUpdate()
    {

    }

    public override void OnStateExit()
    {

    }

    private IEnumerator Jump()
    {
        float t = 0;
        while (true)
        {
            var ds = _motionSettings.speed * Time.deltaTime - _motionSettings.gravity * t * Time.deltaTime;
            _controller.Move(ds * Vector3.up);
            t += Time.deltaTime;

            if (_controller.isGrounded)
            {
                StopCoroutine(_jump);
            }

            yield return new WaitForEndOfFrame();
        }
    }
}
