using DG.Tweening;
using R3;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : HealthBase
{
    [SerializeField] private Image _healthBar;
    [SerializeField] private float _healthBarUnfillDuration;
    private Animator _animator;

    protected override void Init()
    {
        base.Init();
        AddToInit();
        UpdateHealthBar();
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
    }

    private void UpdateHealthBar()
    {
        _currentLifePoints
            .Select(value => value * 1.0f)
            .Subscribe(value => _healthBar.DOFillAmount(value / _initialLifePoints, _healthBarUnfillDuration))
            .AddTo(this);
    }
}
