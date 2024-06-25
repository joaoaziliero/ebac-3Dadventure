using R3;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invisibility : MonoBehaviour
{
    [SerializeField] private float _invisibleModeProbability;

    private MeshRenderer _meshRenderer;
    private Color _originalColor;
    private Color _originalEmissionColor;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _originalColor = _meshRenderer.material.GetColor("_Color");
        _originalEmissionColor = _meshRenderer.material.GetColor("_EmissionColor");
    }

    private void Update()
    {
        if (Random.value >= _invisibleModeProbability)
        {

        }
    }
}
