using R3;
using R3.Triggers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZZ_CausalLink : MonoBehaviour
{
    private void Start()
    {
        Observable.EveryValueChanged(transform, transf => transf.rotation.eulerAngles.x)
            .Where(value => value >= 315.0)
            .Take(1)
            .Subscribe(value => Debug.Log($"Rotation detected at eulerX == {value}"))
            .AddTo(this);
    }
}
