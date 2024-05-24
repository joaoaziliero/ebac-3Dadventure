using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerMotionSettings : ScriptableObject
{
    [Header("Settings")]
    public string horizontalAxisName;
    public string verticalAxisName;
    public float speed;
    public float walkAnimSpeed;
    public KeyCode runKeyCode;
    public float runSpeed;
    public float runAnimSpeed;
    public KeyCode jumpKeyCode;
    public float jumpSpeed;
    public float gravity;
    public float gravityOnFallFromJump;
    public float turnSpeed;

    [Header("Input Storage Visualization")]
    [Range(-1, 1)] public float horizontalAxisValue;
    [Range(-1, 1)] public float verticalAxisValue;
    [HideInInspector] public bool runKeyPressed;
    [HideInInspector] public bool jumpKeyPressed;
}
