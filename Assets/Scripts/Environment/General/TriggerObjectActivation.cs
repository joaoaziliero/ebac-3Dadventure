using R3;
using R3.Triggers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerObjectActivation : MonoBehaviour
{
    [SerializeField] private GameObject _objectToActivate;
    [SerializeField] private string _activationTag;

    private void Start()
    {
        GetComponent<Collider>()
            .OnTriggerEnterAsObservable()
            .Where(collision => collision.gameObject.CompareTag(_activationTag))
            .Subscribe(_ => _objectToActivate.SetActive(true));
    }
}
