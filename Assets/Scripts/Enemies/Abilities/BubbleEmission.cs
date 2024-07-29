using DG.Tweening;
using R3;
using R3.Triggers;
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
    private Collider _attackTrigger;

    private void Awake()
    {
        _attackTrigger = GetComponent<Collider>();
    }

    private void Start()
    {
        _delayCoroutine = StartCoroutine(DelayBubbles());

        _attackTrigger
            .OnTriggerEnterAsObservable()
            .Where(collision => collision.CompareTag("Player"))
            .Subscribe(_ => transform.parent.GetComponent<GunBase>().Shoot(transform.parent.position, transform.parent.forward))
            .AddTo(this);
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
