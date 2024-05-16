using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerMotionSettings : ScriptableObject
{
    public string horizontalAxisName;
    [Range(-1, 1)] public float horizontalAxisValue;
    public string verticalAxisName;
    [Range(-1, 1)] public float verticalAxisValue;
    public float speed;
    public float jumpSpeed;
    public float gravity;
}
