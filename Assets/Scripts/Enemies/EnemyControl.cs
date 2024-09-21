using DG.Tweening;
using R3;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils.StateMachines.Conventions;

public class EnemyControl : HealthBase
{
    private StateManager _stateManager;
    private GameObject _player;
    private MeshRenderer _meshRenderer;

    protected override void Init()
    {
        base.Init();
        AddToInit();
    }

    protected override void RunDamageVisualCue()
    {
        _currentLifePoints
            .Skip(1)
            .Where(_ => DOTween.IsTweening(_meshRenderer.material) == false)
            .Subscribe(_ => _meshRenderer.material.DOColor(_colorOnDamage, "_EmissionColor", _colorChangeDuration).SetLoops(2, LoopType.Yoyo))
            .AddTo(this);
    }

    protected override void CheckForDeath(Action onDeath = null)
    {
        base.CheckForDeath(() => { _stateManager.ChooseState(StateNames.EnemyDeathState); });
    }

    protected override void ManageDamage()
    {
        base.ManageDamage();
        LookAtPlayer();
    }

    private void AddToInit()
    {
        _stateManager = GetComponent<StateManager>();
        _player = GameObject.FindGameObjectWithTag("Player");
        _meshRenderer = GetComponentInParent<MeshRenderer>();
    }

    private void LookAtPlayer()
    {
        Observable
            .EveryUpdate()
            .Subscribe(_ =>
            {
                if (_player != null)
                {
                    transform.parent.LookAt(_player.transform);
                    transform.parent.Rotate(0, 180, 0);
                }
                else
                {
                    _player = GameObject.FindGameObjectWithTag("Player");
                }
            })
            .AddTo(this);
    }
}
