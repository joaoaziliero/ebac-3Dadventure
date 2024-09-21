using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CheckpointFeedbackActivation : CheckpointBase
{
    private MeshRenderer _totemRenderer;

    private void Awake()
    {
        _totemRenderer = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        _totemRenderer.material.SetColor("_EmissionColor", Color.black);
    }

    private void LightUpTotem()
    {
        _totemRenderer.material.SetColor("_EmissionColor", Color.white);
    }

    protected override void ConfirmCheckpointUse()
    {
        base.ConfirmCheckpointUse();
        LightUpTotem();
    }
}
