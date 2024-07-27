using DG.Tweening;
using R3;
using R3.Triggers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils.StateMachines.Conventions;

public class EnemyControl : MonoBehaviour
{
    [SerializeField] private int _initialLifePoints;
    [SerializeField] private int _damageByProjectile;
    [SerializeField] private int _damageMultiplier;
    [SerializeField] private string _tagForProjectiles;
    [SerializeField] private Color _colorOnDamage;
    [SerializeField] private float _colorChangeDuration;

    private StateManager _stateManager;
    private Collider _collider;
    private ReactiveProperty<int> _currentLifePoints;
    private MeshRenderer _meshRenderer;
    private GameObject _player;

    private void Awake()
    {
        if (_damageMultiplier < 1) _damageMultiplier = 1;
        _stateManager = GetComponent<StateManager>();
        _collider = GetComponentInParent<Collider>();
        _currentLifePoints = new ReactiveProperty<int>(_initialLifePoints);
        _meshRenderer = GetComponentInParent<MeshRenderer>();
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Start()
    {
        _collider
            .OnTriggerEnterAsObservable()
            .Where(collision => collision.gameObject.CompareTag(_tagForProjectiles))
            .Select(collision => _damageByProjectile * _damageMultiplier)
            .Subscribe(damage => _currentLifePoints.Value -= damage)
            .AddTo(this);

        _currentLifePoints
            .Skip(1)
            .Where(_ => DOTween.IsTweening(_meshRenderer.material) == false)
            .Subscribe(_ => _meshRenderer.material.DOColor(_colorOnDamage, "_EmissionColor" , _colorChangeDuration).SetLoops(2, LoopType.Yoyo))
            .AddTo(this);

        _currentLifePoints
            .Where(value => value <= 0)
            .Subscribe(_ => _stateManager.ChooseState(StateNames.EnemyDeathState))
            .AddTo(this);

        Observable
            .EveryUpdate()
            .Subscribe(_ => { transform.parent.LookAt(_player.transform); transform.parent.Rotate(0, 180, 0); })
            .AddTo(this);
    }
}
