using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils.StateMachines.Conventions;

[StateFilter(StateGroupings.PlayerMotion)]
public class JumpingState : StateBase
{
    private CharacterController _controller;
    private PlayerMotionSettings _motionSettings;
    private Coroutine _jumpCoroutine;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _motionSettings = Resources.Load<PlayerMotionSettings>("PlayerMotionSettings");
        _jumpCoroutine = null;
    }

    public override void OnStateEnter()
    {
        JumpCheck();
    }

    public override void OnStateUpdate()
    {

    }

    public override void OnStateExit()
    {

    }

    private void JumpCheck()
    {
        if (_motionSettings.jumpKeyPressed && _jumpCoroutine == null)
        {
            _jumpCoroutine = StartCoroutine(Jump());
        }
    }

    private IEnumerator Jump()
    {
        float t = 0;
        while (true)
        {
            var ds = _motionSettings.jumpSpeed * Time.deltaTime - _motionSettings.gravity * t * Time.deltaTime;
            _controller.Move(ds * Vector3.up);
            t += Time.deltaTime;

            if (_controller.isGrounded)
            {
                StopCoroutine(_jumpCoroutine);
                _jumpCoroutine = null;
            }

            yield return new WaitForEndOfFrame();
        }
    }
}
