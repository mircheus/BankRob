using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MoveFinger : MonoBehaviour
{
    [SerializeField] private RectTransform _fingerPosition;
    [SerializeField] private float _xOffset;
    [SerializeField] private float _duration;

    private void OnEnable()
    {
        _xOffset = _fingerPosition.anchoredPosition.x * -1;
        Move();
    }

    private void Move()
    {
        var sequence = DOTween.Sequence();
        sequence.Append(_fingerPosition.DOAnchorPos(new Vector2(_xOffset, 0), _duration))
            .SetLoops(-1, LoopType.Restart);
    }
}
