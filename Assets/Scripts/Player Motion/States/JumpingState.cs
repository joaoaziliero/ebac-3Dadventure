using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils.StateMachines.Conventions;

[StateFilter(StateGroupings.PlayerMotion)]
public class JumpingState : StateBase
{
    private CharacterController _controller;
    private PlayerMotionSettings _motionSettings;
    
    public Coroutine jumpCoroutine;

    private void Awake()
    {
        GetComponentInChildren<PlayerControl>().jumpingState = this;
        _controller = GetComponent<CharacterController>();
        _motionSettings = Resources.Load<PlayerMotionSettings>("PlayerMotionSettings");
        jumpCoroutine = null;
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
        if (_motionSettings.jumpKeyPressed && jumpCoroutine == null)
        {
            jumpCoroutine = StartCoroutine(Jump());
        }
    }

    private IEnumerator Jump()
    {
        float t = 0;

        while (true)
        {
            var dt = Time.deltaTime;
            var ds = _motionSettings.jumpSpeed * dt - _motionSettings.gravity * t * dt;

            _controller.Move(ds * Vector3.up);

            if (_controller.isGrounded)
            {
                StopCoroutine(jumpCoroutine);
                jumpCoroutine = null;
            }

            t += dt;
            yield return new WaitForEndOfFrame();
        }
    }
}
