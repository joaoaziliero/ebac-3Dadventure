using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils.StateMachines.Conventions;

[StateFilter(StateGroupings.PlayerMotion)]
public class JumpingState : StateBase
{
    public Coroutine JumpExecutionRoutine { get; private set; }
    
    private CharacterController _controller;
    private PlayerMotionSettings _motionSettings;
    
    private void Awake()
    {
        JumpExecutionRoutine = null;
        _controller = GetComponent<CharacterController>();
        _motionSettings = Resources.Load<PlayerMotionSettings>("PlayerMotionSettings");
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
        if (_motionSettings.jumpKeyPressed && JumpExecutionRoutine == null)
        {
            JumpExecutionRoutine = StartCoroutine(Jump());
        }
    }

    private IEnumerator Jump()
    {
        float t = 0;
        float pull = _motionSettings.gravity;

        while (true)
        {
            var dt = Time.deltaTime;
            var ds = _motionSettings.jumpSpeed * dt - pull * t * dt;

            _controller.Move(ds * Vector3.up);
            t += dt;

            if (t > _motionSettings.jumpSpeed / _motionSettings.gravity)
            {
                pull = _motionSettings.gravityOnFallFromJump;
            }

            if (_controller.isGrounded)
            {
                StopCoroutine(JumpExecutionRoutine);
                JumpExecutionRoutine = null;
            }

            yield return new WaitForEndOfFrame();
        }
    }
}
