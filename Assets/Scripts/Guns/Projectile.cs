using R3;
using R3.Triggers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float projectileSpeed;
    [SerializeField] private float _timeToDeactivate;
    [SerializeField] private string _tagForEnemies;
    [SerializeField] private bool _flipTravelDirection = false;

    private void OnEnable()
    {
        Observable
            .Timer(TimeSpan.FromSeconds(_timeToDeactivate))
            .Do(onCompleted: _ => gameObject.SetActive(false))
            .Subscribe();

        GetComponent<Collider>()
            .OnTriggerEnterAsObservable()
            .Where(collision => collision.gameObject.CompareTag(_tagForEnemies))
            .Subscribe(_ => gameObject.SetActive(false))
            .AddTo(this);
    }
    void Update()
    {
        if (_flipTravelDirection)
            transform.Translate((-1) * projectileSpeed * Time.deltaTime * transform.forward, Space.World);
        else
            transform.Translate((+1) * projectileSpeed * Time.deltaTime * transform.forward, Space.World);
    }
}
