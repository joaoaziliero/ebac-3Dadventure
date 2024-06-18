using R3;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float projectileSpeed;
    [SerializeField] private float _timeToDeactivate;

    private void OnEnable()
    {
        Observable
            .Timer(TimeSpan.FromSeconds(_timeToDeactivate))
            .Do(onCompleted: _ => gameObject.SetActive(false))
            .Subscribe();
    }
    void Update()
    {
        transform.Translate(projectileSpeed * Time.deltaTime * transform.forward, Space.World);
    }
}
