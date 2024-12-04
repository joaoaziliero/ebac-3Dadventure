using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CheckpointFeedbackActivation : CheckpointBase
{
    private MeshRenderer _totemRenderer;
    private Color _bloomColor;
    private AudioSource _audioSource;

    private void Awake()
    {
        _totemRenderer = GetComponent<MeshRenderer>();
        _bloomColor = _totemRenderer.material.GetColor("_EmissionColor");
        _totemRenderer.material.SetColor("_EmissionColor", Color.black);
        _audioSource = GetComponent<AudioSource>();
    }

    private void LightUpTotem()
    {
        _totemRenderer.material.SetColor("_EmissionColor", _bloomColor);
    }

    private void PlaySound()
    {
        _audioSource.Play();
    }

    protected override void ConfirmCheckpointUse()
    {
        base.ConfirmCheckpointUse();
        LightUpTotem();
        PlaySound();
    }
}
