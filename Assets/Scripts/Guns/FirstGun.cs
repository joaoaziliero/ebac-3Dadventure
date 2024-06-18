using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstGun : GunBase
{
    [SerializeField] private KeyCode _keyToShoot;
    [SerializeField] private float _offsetFactor;

    private void Update()
    {
        if (Input.GetKeyDown(_keyToShoot))
        {
            Shoot(transform.position + _offsetFactor * _projectilePrefab.GetComponent<Projectile>().projectileSpeed * _timeBetweenShots * transform.forward, transform.forward);
        }
    }
}
