using DG.Tweening;
using R3;
using R3.Triggers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils.StateMachines.Conventions;

public class HealthBase : MonoBehaviour
{
    [SerializeField] protected Color _colorOnDamage;
    [SerializeField] protected float _colorChangeDuration;
    [SerializeField] protected int _initialLifePoints;
    [SerializeField] private int _damageByProjectile;
    [SerializeField] private int _damageMultiplier;
    [SerializeField] private string _tagForProjectiles;

    public ReactiveProperty<int> currentLifePoints = null;
    private Collider _collider;

    private void Start()
    {
        Init();
        ManageDamage();
    }

    protected virtual void Init()
    {
        if (_damageMultiplier < 1) _damageMultiplier = 1;
        currentLifePoints ??= new ReactiveProperty<int>(_initialLifePoints);
        _collider = GetComponentInParent<BoxCollider>();
    }

    protected virtual void ManageDamage()
    {
        AccountForDamage();
        RunDamageVisualCue();
        CheckForDeath();
    }

    private void AccountForDamage()
    {
        _collider
            .OnTriggerEnterAsObservable()
            .Where(collision => collision.gameObject.CompareTag(_tagForProjectiles))
            .Select(collision => _damageByProjectile * _damageMultiplier)
            .Subscribe(damage => currentLifePoints.Value -= damage)
            .AddTo(this);
    }

    protected virtual void RunDamageVisualCue()
    {

    }

    protected virtual void CheckForDeath(Action onDeath = null)
    {
        currentLifePoints
            .Where(value => value <= 0)
            .Subscribe(_ => onDeath?.Invoke())
            .AddTo(this);
    }
}
