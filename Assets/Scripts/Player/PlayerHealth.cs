using DG.Tweening;
using R3;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class PlayerHealth : HealthBase
{
    [Header("Health Bar Setup")]
    [SerializeField] private Image _healthBar;
    [SerializeField] private float _healthBarUnfillDuration;

    [Header("Camera Shake Setup")]
    [SerializeField] private float _amplitudeGain;
    [SerializeField] private float _frequencyGain;
    [SerializeField] private float _shakeDuration;

    private CinemachineBasicMultiChannelPerlin _perlin;
    private Animator _animator;

    protected override void Init()
    {
        base.Init();
        AddToInit();
        UpdateHealthBar();
        RunCameraShake();
    }

    protected override void RunDamageVisualCue()
    {
        _currentLifePoints
            .Skip(1)
            .Where(_ => DOTween.IsTweening("Health") == false)
            .Subscribe(_ => _healthBar.DOColor(_colorOnDamage, _colorChangeDuration).SetLoops(2, LoopType.Yoyo).SetId("Health"))
            .AddTo(this);
    }

    protected override void CheckForDeath(Action onDeath = null)
    {
        base.CheckForDeath(() => 
        {
            _animator.SetTrigger("DEATH");
            transform.parent.GetComponentInChildren<PlayerControl>().enabled = false;
            Observable.Timer(TimeSpan.FromSeconds(3))
                .Do(_ => transform.parent.gameObject.SetActive(false))
                .Subscribe()
                .AddTo(this);
        });
    }

    private void AddToInit()
    {
        _animator = transform.parent.GetComponentInChildren<Animator>();
        _perlin = transform.parent.GetComponentInChildren<CinemachineBasicMultiChannelPerlin>();
    }

    private void UpdateHealthBar()
    {
        _currentLifePoints
            .Select(value => value * 1.0f)
            .Subscribe(value => _healthBar.DOFillAmount(value / _initialLifePoints, _healthBarUnfillDuration))
            .AddTo(this);
    }

    private void RunCameraShake()
    {
        _currentLifePoints
            .Skip(1)
            .Subscribe(_ => ShakeCamera(_amplitudeGain, _frequencyGain, _shakeDuration))
            .AddTo(this);
    }

    private void ShakeCamera(float amplitude, float frequency, float duration)
    {
        if (_perlin.m_AmplitudeGain == 0 && _perlin.m_FrequencyGain == 0)
        {
            _perlin.m_AmplitudeGain = amplitude;
            _perlin.m_FrequencyGain = frequency;

            Observable.Timer(TimeSpan.FromSeconds(duration))
                .Do(onCompleted: _ =>
                {
                    _perlin.m_AmplitudeGain = 0;
                    _perlin.m_FrequencyGain = 0;
                })
                .Subscribe()
                .AddTo(this);
        }
    }
}
