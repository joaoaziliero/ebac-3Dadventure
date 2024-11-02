using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ClothingFunctions : MonoBehaviour
{
    public PlayerMotionSettings motionSettings;
    
    private float _originalRunningSpeed;
    private Vector3 _originalPlayerScale;

    private void Awake()
    {
        motionSettings = Resources.Load<PlayerMotionSettings>("PlayerMotionSettings");
        _originalRunningSpeed = motionSettings.runSpeed;
        _originalPlayerScale = transform.parent.localScale;
    }

    public void Normalize()
    {
        NormalizeSpeed();
        NormalizeScale();
    }

    public void RunFaster()
    {
        NormalizeScale();
        motionSettings.runSpeed *= motionSettings.runSpeedMultiplier;
    }

    public void Enlarge()
    {
        NormalizeSpeed();
        transform.parent.DOScale(2 * _originalPlayerScale, 1);
    }

    private void NormalizeSpeed()
    {
        motionSettings.runSpeed = _originalRunningSpeed;
    }

    private void NormalizeScale()
    {
        transform.parent.DOScale(_originalPlayerScale, 1);
    }

    private void OnDestroy()
    {
        NormalizeSpeed();
    }
}
