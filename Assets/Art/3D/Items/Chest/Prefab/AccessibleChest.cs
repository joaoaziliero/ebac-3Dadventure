using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Networking;
using R3;

public class AccessibleChest : ChestBase
{
    public GameObject chestLid;
    public GameObject prefabToSpawn;
    public int prefabCopies;
    public float lidRotationAngle = -90;
    public float lidRotationPeriod;
    public string playerTag = "Player";

    private float _angleOfRest;
    private float _eulerY;
    private float _eulerZ;
    private bool _hasSpawned = false;

    private void Awake()
    {
        _angleOfRest = chestLid.transform.rotation.eulerAngles.x;
        _eulerY = chestLid.transform.rotation.eulerAngles.y;
        _eulerZ = chestLid.transform.rotation.eulerAngles.z;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            DOTween.Kill(chestLid.transform);
            AwardChestContent();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            DOTween.Kill(chestLid.transform);
            DeactivateChest();
        }
    }

    public void SpawnChestContent()
    {
        if (_hasSpawned == false)
        {
            for (int i = 0; i < prefabCopies; i++)
            {
                Instantiate(prefabToSpawn, transform).transform.localPosition.Set(0, 0, 0);
            }

            _hasSpawned = true;
        }
    }

    public override void AwardChestContent()
    {
        RotateLid(_angleOfRest + lidRotationAngle, lidRotationPeriod);
        Observable.Timer(TimeSpan.FromSeconds(1)).Do(onCompleted: _ => SpawnChestContent()).Subscribe();
    }

    public override void DeactivateChest()
    {
        RotateLid(_angleOfRest, lidRotationPeriod);
    }

    public void RotateLid(float angle, float period)
    {
        chestLid.transform.DORotate(new Vector3(angle, _eulerY, _eulerZ), period);
    }
}
