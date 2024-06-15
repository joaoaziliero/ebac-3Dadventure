using R3;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GunBase : MonoBehaviour
{
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private int _poolSize;
    [SerializeField] private float _timeBetweenShots;

    private List<GameObject> _projectilePool;

    private void Awake()
    {
        _projectilePool = new List<GameObject>(_poolSize);

        for (int i = 0; i < _poolSize; i++)
        {
            _projectilePool.Add(Instantiate(_projectilePrefab));
            _projectilePool[i].SetActive(false);
        }
    }

    public void Shoot(Vector3 initialPosition, Vector3 direction)
    {
        var obj = SelectInactiveProjectile();
        if (obj == null) return;
        obj.transform.position = initialPosition;
        obj.transform.forward = direction;

        if (_projectilePool.All(projectile => projectile.activeInHierarchy == false))
        {
            obj.SetActive(true);
        }
        else
        {
            Observable
                .Timer(TimeSpan.FromSeconds(_timeBetweenShots))
                .Do(onCompleted: _ => obj.SetActive(true))
                .Subscribe();
        }
    }

    private GameObject SelectInactiveProjectile()
    {
        int i = 0;
        while (_projectilePool[i].activeInHierarchy)
        {
            i++;
            if (i >= _poolSize) return null;
        }
        return _projectilePool[i];
    }
}
