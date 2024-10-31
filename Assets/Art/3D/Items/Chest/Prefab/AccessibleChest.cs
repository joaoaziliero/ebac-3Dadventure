using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AccessibleChest : ChestBase
{
    public GameObject chestLid;
    public float lidRotationAngle = 90;
    public float lidRotationPeriod;

    public override void AwardChestContent()
    {
        RotateLid(lidRotationAngle, lidRotationPeriod);
    }

    public override void DeactivateChest()
    {
        RotateLid(-lidRotationAngle, lidRotationPeriod);

    }

    public void RotateLid(float angle, float period)
    {
        chestLid.transform.DORotate(new Vector3(angle, 0, 0), period);
    }
}
