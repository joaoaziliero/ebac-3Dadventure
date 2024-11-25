using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ClothingFunctions : MonoBehaviour
{
    [SerializeField] private float _originalRunningSpeed;
    [SerializeField] private Vector3 _originalPlayerScale;

    private PlayerMotionSettings _motionSettings;

    private void Awake()
    {
        _motionSettings = Resources.Load<PlayerMotionSettings>("PlayerMotionSettings");
    }

    public void Normalize()
    {
        NormalizeSpeed();
        NormalizeScale();
    }

    public void RunFaster()
    {
        NormalizeScale();
        _motionSettings.runSpeed *= _motionSettings.runSpeedMultiplier;
    }

    public void Enlarge()
    {
        NormalizeSpeed();
        transform.parent.DOScale(2 * _originalPlayerScale, 1);
    }

    private void NormalizeSpeed()
    {
        _motionSettings.runSpeed = _originalRunningSpeed;
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
