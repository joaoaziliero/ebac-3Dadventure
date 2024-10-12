using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CheckpointFeedbackActivation : CheckpointBase
{
    private MeshRenderer _totemRenderer;
    private Color _bloomColor;

    private void Awake()
    {
        _totemRenderer = GetComponent<MeshRenderer>();
        _bloomColor = _totemRenderer.material.GetColor("_EmissionColor");
    }

    private void Start()
    {
        _totemRenderer.material.SetColor("_EmissionColor", Color.black);
    }

    private void LightUpTotem()
    {
        _totemRenderer.material.SetColor("_EmissionColor", _bloomColor);
    }

    protected override void ConfirmCheckpointUse()
    {
        base.ConfirmCheckpointUse();
        LightUpTotem();
    }
}
