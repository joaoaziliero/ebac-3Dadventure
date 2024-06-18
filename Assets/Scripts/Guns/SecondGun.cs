using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondGun : GunBase
{
    [SerializeField] private KeyCode _keyToShoot;
    private float _rotationAngle;
    private int _rotationCount;

    private void Awake()
    {
        _rotationAngle = 360 / _poolSize;
        _rotationCount = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(_keyToShoot))
        {
            var rotation = Quaternion.Euler(_rotationAngle * _rotationCount * Vector3.up);
            Shoot(transform.position, rotation * transform.forward);
            _rotationCount++;
        }
    }

    private void OnDisable()
    {
        _rotationCount = 0;
    }
}
