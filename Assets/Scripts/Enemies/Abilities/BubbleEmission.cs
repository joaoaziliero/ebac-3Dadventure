using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleEmission : MonoBehaviour
{
    [SerializeField] private float _finalBubbleScale;
    [SerializeField] private float _bubbleGrowthDuration;
    [SerializeField] private float _intervalBetweenBubbles;

    private Tween _bubbleScaleTween;
    private Coroutine _delayCoroutine;

    private void Start()
    {
        _delayCoroutine = StartCoroutine(DelayBubbles());
    }

    private void Update()
    {
        if (_delayCoroutine == null)
        {
            _delayCoroutine = StartCoroutine(DelayBubbles());
        }
    }

    private void OnDestroy()
    {
        StopCoroutine(_delayCoroutine);
        _bubbleScaleTween.Kill();
    }

    private IEnumerator DelayBubbles()
    {
        _bubbleScaleTween = transform.DOScale(_finalBubbleScale, _bubbleGrowthDuration).SetLoops(2, LoopType.Yoyo);
        yield return new WaitForSeconds(2 * _bubbleGrowthDuration + _intervalBetweenBubbles);
        _delayCoroutine = null;
    }
}
