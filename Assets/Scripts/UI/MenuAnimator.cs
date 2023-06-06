using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using DG.Tweening;
using UnityEngine.InputSystem.Composites;
using UnityEngine.UI;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class MenuAnimator
{
    public const float AnimationDuration = .5f;
    private const float SlowAnimationDuration = 2f;
    private const float PulseDuration = .5f;
    private const int YOffset = 1500;
    private static readonly Vector2 upDrag = new Vector2(0, YOffset);
    private static readonly Vector2 downDrag = new Vector2(0, YOffset);

    public static void DragMenuDown(RectTransform menu)
    {
        menu.DOAnchorPos(Vector2.zero, AnimationDuration).SetUpdate(true);;
    }

    public static void DragMenuUp(RectTransform menu)
    {
        menu.DOAnchorPos(upDrag, AnimationDuration).SetUpdate(true);
    }

    public static void DragMenuUpSlowly(RectTransform menu)
    {
        menu.DOAnchorPos(upDrag, SlowAnimationDuration).SetUpdate(true);
    }

    public static void FadeIn(Image fadeImage)
    {
        fadeImage.DOFade(1, AnimationDuration).SetUpdate(true);;
    }

    public static void FadeOut(Image fadeImage)
    {
        fadeImage.DOFade(0, AnimationDuration).SetUpdate(true);;
    }

    public static void MoveWinTitle(RectTransform panel, RectTransform text)
    {
        DOTween.Sequence()
            .Append(panel.DOAnchorPos(new Vector2(0, -60), AnimationDuration))
            .AppendInterval(.1f)
            .Append(text.DOAnchorPos(Vector2.zero, AnimationDuration))
            .SetUpdate(true);
    }

    public static void PulsateButton(RectTransform button)
    {
        var sequence = DOTween.Sequence()
            .Append(button.DOScale(new Vector3(1.1f, 1.1f, 0), AnimationDuration))
            .Append(button.DOScale(Vector3.one, AnimationDuration));
        sequence.SetUpdate(true);
        sequence.SetLoops(-1, LoopType.Restart);
    }

    public static void KillAllTweens()
    {
        DOTween.KillAll();
    }
    
    public static void ZoomInAndPulsateButton(RectTransform button)
    {
        var sequence = DOTween.Sequence().Append(button.DOScale(Vector3.one, AnimationDuration)).SetUpdate(true);
        sequence.OnComplete(() =>
        {
            sequence.Pause();
            DOTween.Sequence()
                .Append(button.DOScale(new Vector3(1.1f, 1.1f, 0), PulseDuration))
                .Append(button.DOScale(Vector3.one, PulseDuration))
                .SetLoops(-1, LoopType.Restart)
                .SetUpdate(true);
        });
    }

    public static void ZoomInElement(RectTransform element)
    {
        Tween tween = element.DOScale(new Vector3(1f, 1f, 0), AnimationDuration);
        tween.SetUpdate(true);
    }

    public static void MoveElementUp(RectTransform element)
    {
        Tween tween = element.DOAnchorPos(upDrag, AnimationDuration);
        tween.SetUpdate(true);
    }
    
    public static void MoveElementDown(RectTransform element)
    {
        Tween tween = element.DOAnchorPos(downDrag, AnimationDuration);
        tween.SetUpdate(true);
    }
}
