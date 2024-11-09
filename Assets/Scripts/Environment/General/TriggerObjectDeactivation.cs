using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerObjectDeactivation : MonoBehaviour
{
    [SerializeField] private string _deactivationTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_deactivationTag))
        {
            Destroy(gameObject, 0.25f);
        }
    }
}
