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

    protected override void Init()
    {
        base.Init();
        AddToInit();
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
    }

    private void LookAtPlayer()
    {
        Observable
            .EveryUpdate()
            .Subscribe(_ => { transform.parent.LookAt(_player.transform); transform.parent.Rotate(0, 180, 0); })
            .AddTo(this);
    }
}
