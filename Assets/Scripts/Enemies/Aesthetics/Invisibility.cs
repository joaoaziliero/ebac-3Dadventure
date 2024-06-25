using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invisibility : MonoBehaviour
{
    [SerializeField] [Range(0f, 100f)] private float _invisibleModeProbability;
    [SerializeField] private float _colorTweeningDuration;
    [SerializeField] private Ease _colorTweeningEase;
    [SerializeField] private float _invisibilityTimePeriod;

    private MeshRenderer _meshRenderer;
    private Color _originalColor;
    private Color _originalEmissionColor;
    private Coroutine _invisibilityRoutine;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _originalColor = _meshRenderer.material.GetColor("_Color");
        _originalEmissionColor = _meshRenderer.material.GetColor("_EmissionColor");
        _invisibilityRoutine = null;
    }

    private void Update()
    {
        if (Random.value <= (_invisibleModeProbability / 100) * Time.deltaTime && _invisibilityRoutine == null)
        {
            _invisibilityRoutine = StartCoroutine(ActivateTransparencyCycle());
        }
    }

    private IEnumerator ActivateTransparencyCycle()
    {
        TweenColoring(Color.clear, Color.clear);
        yield return new WaitForSeconds(_invisibilityTimePeriod - _colorTweeningDuration);
        TweenColoring(_originalColor, _originalEmissionColor);
        yield return new WaitForSeconds(_colorTweeningDuration);
        _invisibilityRoutine = null;
    }

    private void TweenColoring(Color endColor, Color endEmissionColor)
    {
        _meshRenderer.material
            .DOColor(endColor, "_Color", _colorTweeningDuration)
            .SetEase(_colorTweeningEase);
        _meshRenderer.material
            .DOColor(endEmissionColor, "_EmissionColor", _colorTweeningDuration)
            .SetEase(_colorTweeningEase);
    }
}
