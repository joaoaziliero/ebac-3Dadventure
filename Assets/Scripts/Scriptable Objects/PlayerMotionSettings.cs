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
    public KeyCode jumpKeyCode;
    public float jumpSpeed;
    public float gravity;

    [Header("Input Storage Visualization")]
    [Range(-1, 1)] public float horizontalAxisValue;
    [Range(-1, 1)] public float verticalAxisValue;
    [HideInInspector] public bool jumpKeyPressed;
}
