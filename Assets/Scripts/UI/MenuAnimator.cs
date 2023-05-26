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
    public const float PulseDuration = .2f;
    private const int YOffset = 1100;
    private static readonly Vector2 upDrag = new Vector2(0, YOffset);

    public static void DragMenuDown(RectTransform menu)
    {
        menu.DOAnchorPos(Vector2.zero, AnimationDuration);
    }

    public static void DragMenuUp(RectTransform menu)
    {
        menu.DOAnchorPos(upDrag, AnimationDuration);
    }

    public static void FadeIn(Image fadeImage)
    {
        fadeImage.DOFade(1, AnimationDuration);
    }

    public static void FadeOut(Image fadeImage)
    {
        fadeImage.DOFade(0, AnimationDuration);
    }

    public static void MoveWinTitle(RectTransform panel, RectTransform text)
    {
        DOTween.Sequence()
            .Append(panel.DOAnchorPos(new Vector2(0, -60), AnimationDuration))
            .AppendInterval(.1f)
            .Append(text.DOAnchorPos(Vector2.zero, AnimationDuration));
    }

    public static void PulsateButton(RectTransform button)
    {
        var sequence = DOTween.Sequence()
            .Append(button.DOScale(new Vector3(1.1f, 1.1f, 0), AnimationDuration))
            .Append(button.DOScale(Vector3.one, AnimationDuration));

        sequence.SetLoops(-1, LoopType.Restart);
    }

    public static void ZoomInAndPulsateButton(RectTransform button)
    {
        var sequence = DOTween.Sequence().Append(button.DOScale(Vector3.one, AnimationDuration));
        sequence.OnComplete(() =>
        {
            sequence.Pause();
            DOTween.Sequence()
                .Append(button.DOScale(new Vector3(1.1f, 1.1f, 0), PulseDuration))
                .Append(button.DOScale(Vector3.one, PulseDuration))
                .SetLoops(-1, LoopType.Restart);
        });
    }

    public static void ZoomInElement(RectTransform element)
    {
        Tween tween = element.DOScale(new Vector3(1f, 1f, 0), AnimationDuration);
    }
}
