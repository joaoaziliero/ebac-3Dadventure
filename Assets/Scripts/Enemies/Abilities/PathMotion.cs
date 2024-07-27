using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PathMotion : MonoBehaviour
{
    [SerializeField] private List<GameObject> _waypointObjects;
    [SerializeField] private float _travelDuration;
    [SerializeField] private PathType _pathType;
    [SerializeField] private Ease _travelEase;

    private Tween _pathTween;

    private void Awake()
    {
        _pathTween = transform.DOPath(
            _waypointObjects.Select(obj => obj.transform.position).ToArray(),
            _travelDuration,
            _pathType)
            .SetEase(_travelEase)
            .SetLoops(-1, LoopType.Yoyo)
            .Pause();
    }

    private void Start()
    {
        _pathTween.Play();
    }

    private void OnDestroy()
    {
        _pathTween.Kill();
    }
}
