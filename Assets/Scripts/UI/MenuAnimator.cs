using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class MenuAnimator
{
    public const float AnimationDuration = .5f;
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
}
